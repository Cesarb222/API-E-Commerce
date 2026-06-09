using AppPracticaASP.NET.DTO;
using AppPracticaASP.NET.Models;
using AppPracticaASP.NET.Repository.Interfaces;
using AppPracticaASP.NET.Services.Interfaces;

namespace AppPracticaASP.NET.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUserRepository _userRepository;
        public UsuarioService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDTO>? ActualizarUsuario(string guid, UserCreateDTO userCreateDTO)
        {

            if (userCreateDTO.PasswordHash != null)
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(userCreateDTO.PasswordHash);
                userCreateDTO.PasswordHash = hash;
            }
            User? user = await _userRepository.ActualizarUsuario(guid, userCreateDTO);
            if (user == null) return null;
            else return MapUser(user);

        }


        public async Task<IEnumerable<UserResponseDTO>> GetALL()

        {
            var  usuarios = await _userRepository.GetALL();
            var lista = usuarios.Select(user => MapUser(user)).ToList();
            return lista;
        }

        public async Task<UserResponseDTO>? GetByGUID(string guid)
        {
            var user = await _userRepository.GetByGUID(guid);
            if (user == null) return null;
            else return MapUser(user);
        }


        public static UserResponseDTO MapUser(User user)
        {
            return new UserResponseDTO
            {
                Email = user.Email,
                Id = user.Id,
                Nombre = user.Nombre,
                Rol = user.Rol,
                FechaRegistro = user.FechaRegistro,
                Orders = user.Orders,
                Reviews = user.Reviews,
            };
        }
    }
}
