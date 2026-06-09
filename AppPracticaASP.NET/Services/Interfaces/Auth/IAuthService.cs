using AppPracticaASP.NET.DTO;

namespace AppPracticaASP.NET.Services.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<(UserResponseDTO, string)?> Login(LoginDTO data);
        Task<(UserResponseDTO, string)?> Registro(UserCreateDTO userCreateDTO);

    }
}
