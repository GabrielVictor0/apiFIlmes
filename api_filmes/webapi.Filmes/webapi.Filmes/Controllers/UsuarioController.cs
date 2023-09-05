using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webapi.Filmes.Domains;
using webapi.Filmes.Repositories;

namespace webapi.Filmes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class UsuarioController : ControllerBase
    {
        public UsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository= new UsuarioRepository();
        }
        

        [HttpPost]
        public IActionResult Login(UsuarioDomain usuario)
        {
            
            try
            {
                UsuarioDomain usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou senha inválidos!");
                }
                //caso encontre o usuário, segue para a criação do token

                // 1 - Definir as informações(claims)que serão fornecidas no token (Payload)
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti,usuarioBuscado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,usuarioBuscado.Email),
                    new Claim(ClaimTypes.Role,usuarioBuscado.Permissao.ToString()),

                    //exixste a possibilidade de criar uma claim personalizada
                    new Claim("Claim Personalizada", "valor da Claim personalizada")

                };

                //2 - definir a chave de acesso do token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

                //3 - Definir as credenciais do token (header)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4 - gerar token
                var token = new JwtSecurityToken
                (
                    //emissor do token
                    issuer: "webapi.Filmes",

                    //destinatário do token
                    audience: "webapi.Filmes",

                    //dados definidos na claims(informações)
                    claims : claims,

                    //tempo de expiração do token
                    expires: DateTime.Now.AddMinutes(5),

                    //credenciais do token
                    signingCredentials: creds
                );

                //5 - retorna o token criado
                return Ok(new
                { 
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
                
            }
            catch (Exception error)
            {

                return BadRequest(error);
            }
            
        }
    }
}
