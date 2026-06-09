using AppPracticaASP.NET.DTO;
using AppPracticaASP.NET.Models;

namespace AppPracticaASP.NET.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UserResponseDTO>> GetALL();
        Task<UserResponseDTO>? GetByGUID(string guid);

        Task<UserResponseDTO>? ActualizarUsuario(string guid, UserCreateDTO userCreateDTO);


    }
}
