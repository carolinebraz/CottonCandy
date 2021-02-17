using CottonCandy.Application.AppUsuario.Output;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CottonCandy.API.Comum
{
    public class TokenService
    {
        public static string GenerateToken(UsuarioViewModel usuario, string secrets)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secrets);

            var tokenDescriptor = new SecurityTokenDescriptor // metodo de criação do token
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome.ToString()), 
                    new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10), //tempo de validade do token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  // O SecurityAlgorithms pega os dados definidos e gera um token criptografado no JWT
            };

            var token = tokenHandler
                            .CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); // retorno do token
        }
    }
}