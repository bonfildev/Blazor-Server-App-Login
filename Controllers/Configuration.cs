using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blazor_Server_App_Login.api
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        public ConfigurationController()
        {
        }

        [HttpGet("[action]")]
        public string Test()
        {
            // Route: /Configuration/Test
            return "Test a controller in Razor";
        }
    }
}
