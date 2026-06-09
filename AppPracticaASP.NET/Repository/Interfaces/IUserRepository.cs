using AppPracticaASP.NET.DTO;
using AppPracticaASP.NET.Models;

namespace AppPracticaASP.NET.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetALL();
        Task<User>? GetByGUID(string guid);
        Task<User> CrearUsuario(UserCreateDTO userCreateDTO);

        Task<User>? ActualizarUsuario(string guid, UserCreateDTO userCreateDTO);

        Task<bool> EliminarUsuario(string guid);

        Task<User>? Login(LoginDTO data);

    }
}
