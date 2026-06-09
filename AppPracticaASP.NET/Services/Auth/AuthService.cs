using AppPracticaASP.NET.DTO;
using AppPracticaASP.NET.Models;
using AppPracticaASP.NET.Repository.Interfaces;
using AppPracticaASP.NET.Services.Interfaces;
using AppPracticaASP.NET.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppPracticaASP.NET.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<(UserResponseDTO,string)?> Login(LoginDTO data)
        {
            var user = await _userRepository.Login(data);
            if (user == null) return null;
            var tupla = (UsuarioService.MapUser(user), CreateJWT_Token(user));
            return tupla;
        }

        public async Task<(UserResponseDTO, string)?> Registro([FromBody] UserCreateDTO userCreateDTO)
        {
            if (userCreateDTO.PasswordHash != null)
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(userCreateDTO.PasswordHash);
                userCreateDTO.PasswordHash = hash;
            }
            User? user = await _userRepository.CrearUsuario(userCreateDTO);
            if (user == null) return null;

            return (UsuarioService.MapUser(user), CreateJWT_Token(user));
        }

        private string CreateJWT_Token(User usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim("Email",usuario.Email)
                };

            foreach (var rol in usuario.Rol.Split(','))
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }


            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDes);
            return tokenHandler.WriteToken(token);
        }
    }
}
