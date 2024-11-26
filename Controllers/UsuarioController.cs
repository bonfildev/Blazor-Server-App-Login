using AutoMapper;
using Blazor_Server_App_Login.Login;
using Blazor_Server_App_Login.Interfaces;
using Blazor_Server_App_Login.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Server_App_Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<UserDTO>> _ResponseDTO = new ResponseDTO<List<UserDTO>>();

            try
            {
                List<UserDTO> ListaUsuarios = new List<UserDTO>();
                IQueryable<UsersModel> query = await _usuarioRepositorio.Consultar();
                query = query.Include(r => r.IdRolUsuarioNavigation);

                ListaUsuarios = _mapper.Map<List<UserDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<UserDTO>>() { status = true, msg = "ok", value = ListaUsuarios };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<UserDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            ResponseDTO<UserDTO> _ResponseDTO = new ResponseDTO<UserDTO>();
            try
            {
                UserDTO _usuario = new UserDTO();
                IQueryable<UsersModel> query = await _usuarioRepositorio.Consultar(u => u.Correo == correo && u.Clave == clave);
                query = query.Include(r => r.IdRolUsuarioNavigation);

                _usuario = _mapper.Map<UserDTO>(query.FirstOrDefault());

                if (_usuario != null)
                    _ResponseDTO = new ResponseDTO<UserDTO>() { status = true, msg = "ok", value = _usuario };
                else
                    _ResponseDTO = new ResponseDTO<UserDTO>() { status = false, msg = "no encontrado", value = null };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<UserDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] UserDTO request)
        {
            ResponseDTO<UserDTO> _ResponseDTO = new ResponseDTO<UserDTO>();
            try
            {
                UsersModel _usuario = _mapper.Map<UsersModel>(request);

                UsersModel _usuarioCreado = await _usuarioRepositorio.Crear(_usuario);

                if (_usuarioCreado.IdUsuario != 0)
                    _ResponseDTO = new ResponseDTO<UserDTO>() { status = true, msg = "ok", value = _mapper.Map<UserDTO>(_usuarioCreado) };
                else
                    _ResponseDTO = new ResponseDTO<UserDTO>() { status = false, msg = "No se pudo crear el usuario" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<UserDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] UserDTO request)
        {
            ResponseDTO<UserDTO> _ResponseDTO = new ResponseDTO<UserDTO>();
            try
            {
                UsersModel _usuario = _mapper.Map<UsersModel>(request);
                UsersModel _usuarioParaEditar = await _usuarioRepositorio.Obtener(u => u.IdUsuario == _usuario.IdUsuario);

                if (_usuarioParaEditar != null)
                {

                    _usuarioParaEditar.NombreCompleto = _usuario.NombreCompleto;
                    _usuarioParaEditar.Correo = _usuario.Correo;
                    _usuarioParaEditar.IdRolUsuario = _usuario.IdRolUsuario;
                    _usuarioParaEditar.Clave = _usuario.Clave;

                    bool respuesta = await _usuarioRepositorio.Editar(_usuarioParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<UserDTO>() { status = true, msg = "ok", value = _mapper.Map<UserDTO>(_usuarioParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<UserDTO>() { status = false, msg = "No se pudo editar el usuario" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<UserDTO>() { status = false, msg = "No se encontró el usuario" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<UserDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }



        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            ResponseDTO<string> _ResponseDTO = new ResponseDTO<string>();
            try
            {
                UsersModel _usuarioEliminar = await _usuarioRepositorio.Obtener(u => u.IdUsuario == id);

                if (_usuarioEliminar != null)
                {

                    bool respuesta = await _usuarioRepositorio.Eliminar(_usuarioEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el usuario", value = "" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<string>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }
    }
}
