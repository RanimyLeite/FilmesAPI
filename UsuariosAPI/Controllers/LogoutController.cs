using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public IActionResult DeslogarUsuario()
        {
            Result resultado = _logoutService.DeslogaUsuario();
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            //Não retorna nada, apenas um http 200 e um array vazio!
            return Ok(resultado.Successes);
        }
    }
}
