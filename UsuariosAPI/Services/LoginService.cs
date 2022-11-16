using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        //Gerenciador de Login
        private SignInManager<IdentityUser<int>> _signManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signManager, TokenService tokenService)
        {
            _signManager = signManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            //Efetua um login a partir do nome e senha do user
            var resultadoIdentity = _signManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

            if (resultadoIdentity.Result.Succeeded)
            {
                //Recuperando o user para conseguir gerar o token
                var identityUser = _signManager
                    .UserManager
                    .Users
                    .FirstOrDefault(usuario => 
                    usuario.NormalizedUserName == request.UserName.ToUpper());

                //Gerando token
                Token token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Value);
            }

            return Result.Fail("Falha ao tentar logar!");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            IdentityUser<int> identityUser = _signManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedEmail == request.Email.ToUpper());
            if (identityUser != null)
            {
                string codigoDeRecuperacao = _signManager
                    .UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;
                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }

            return Result.Fail("Falha ao solicitar redefinição!");
        }
    }
}
