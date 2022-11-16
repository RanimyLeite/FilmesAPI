using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class TokenService
    {
        //Classe responsavel pela criação do Token

        public Token CreateToken(IdentityUser<int> usuario) 
        {
            //Direitos que o user terá apartir de seu username e id
            Claim[] direitosUsuario = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString())
            };

            //Gerando chave de criptografia do token
            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
                );
            //Gerando credencial a partir da chave e do Algoritmo de criptografia 
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            //Geração do token
            var token = new JwtSecurityToken(
                claims: direitosUsuario,
                signingCredentials: credenciais,
                expires: DateTime.UtcNow.AddHours(1) //Expiração depois de 1 hora
                );

            //Transformando o token pra string
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(tokenString);
        }
    }
}
