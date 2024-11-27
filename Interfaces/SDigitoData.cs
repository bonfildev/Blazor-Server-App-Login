using Blazor_Server_App_Login.Data;
using Blazor_Server_App_Login.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blazor_Server_App_Login.Interfaces
{
    public class SDigitoData : ISDigito
    {
        public ApplicationDbContext _ctx;
        public SDigitoData(ApplicationDbContext db)
        {
            _ctx = db;
        }

        public List<sDigito> Options => GetSDigitoData();

        public  List<sDigito> GetSDigitoData()
        {
            List<sDigito> lista = new List<sDigito>();
            lista = _ctx.SDigito.ToList();
            return lista;
        }
    }
}
