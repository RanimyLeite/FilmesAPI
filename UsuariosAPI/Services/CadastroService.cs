using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosAPI.Data;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private UserDbContext _context;
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public CadastroService(UserDbContext context, IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto); //Converte de um CreateUsuarioDto --> Usuario
            //Converte de um Usuario para um IdentityUser pois essa é a forma que ele será gravado no banco
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password); //Cria o user no banco
            if (resultadoIdentity.Result.Succeeded) 
            {
                //Gera o código de ativação
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                var encodedCode = HttpUtility.UrlEncode(code);
                _emailService.EnviarEmail(new[] { usuarioIdentity.Email }, 
                    "Link de Ativação", 
                    usuarioIdentity.Id, encodedCode);
                return Result.Ok().WithSuccess(encodedCode);
            }

            return Result.Fail("Falha ao cadastrar usuário!");
        }

        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(usuario => usuario.Id == request.UsuarioId);

            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;

            if (identityResult.Succeeded) return Result.Ok();

            return Result.Fail("Falha ao ativar conta do usuario!");
        }
    }
}
