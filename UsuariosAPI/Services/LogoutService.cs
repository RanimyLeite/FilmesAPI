using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosAPI.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signManager;
        private TokenService _tokenService;

        public LogoutService(SignInManager<IdentityUser<int>> signManager, TokenService tokenService)
        {
            _signManager = signManager;
            _tokenService = tokenService;
        }

        public Result DeslogaUsuario()
        {
            var resultadoIdentity = _signManager.SignOutAsync();
            if (resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Logout falhou!");
        }
    }
}
