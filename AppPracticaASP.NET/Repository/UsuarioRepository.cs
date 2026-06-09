using AppPracticaASP.NET.DTO;
using AppPracticaASP.NET.Models;
using AppPracticaASP.NET.Repository.Interfaces;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;

namespace AppPracticaASP.NET.Repository
{
    public class UsuarioRepository : IUserRepository
    {
        private readonly BddEcomerceContext _context;
        private readonly ILogger<UsuarioRepository> _logger;
        public UsuarioRepository(BddEcomerceContext bddEcomerceContext, ILogger<UsuarioRepository> logger)
        {
            _context = bddEcomerceContext;
            _logger = logger;
        }
        public async Task<User>? ActualizarUsuario(string guid, UserCreateDTO userCreateDTO)
        {
            try
            {
                var user = await this.GetByGUID(guid);
                if (user == null) return null;
                _context.Entry(user).CurrentValues.SetValues(userCreateDTO);
                int rowsUpdates = await _context.SaveChangesAsync();
                if (rowsUpdates > 0) return user;
                else return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en actualizar usuario: {ex.Message}");
                _logger.LogError(ex, "Error en actualizar el usuario con el {guid}", guid);
                return null;
            }
        }

        public async Task<User>? CrearUsuario(UserCreateDTO userCreateDTO)
        {
            
            try
            {
                User user = new User
                {
                    Nombre = userCreateDTO.Nombre,
                    Email = userCreateDTO.Email,
                    PasswordHash = userCreateDTO.PasswordHash
                };
                _context.Users.Add(user);
                int rowsUpdate = await _context.SaveChangesAsync();
                if (rowsUpdate > 0) return user;
                else return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en crear usuario: {ex.Message}");
                _logger.LogError(ex, "Error en crear el usuario");
                return null;
            }

        }

        public async Task<bool> EliminarUsuario(string guid)
        {
            
            try
            {
                User user = await this.GetByGUID(guid);
                if (user == null) return false;
                _context.Remove(user);
                int rowsAffect = await _context.SaveChangesAsync();
                if (rowsAffect > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en eliminar el usuario: {ex.Message}");
                _logger.LogError(ex, "Error en eliminarl el usuario con el {guid}", guid);
                return false;
            }

        }

        public async Task<IEnumerable<User>> GetALL()
        {
            
            try
            {
                return await _context.Users
                .Include(u => u.Reviews)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en al recopilar los usuarios: {ex.Message}");
                _logger.LogError(ex, "Error al recopilar los usuarios");
                return Enumerable.Empty<User>();
            }

        }

        public async Task<User>? GetByGUID(string guid)
        {
            try
            {
                return await _context.Users
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                .Include(u => u.Reviews)
                .FirstOrDefaultAsync(item => item.Id == Guid.Parse(guid));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recopilar usuario: {ex.Message}");
                _logger.LogError(ex, "Error al recopilar el usuario con el {guid}", guid);
                return null;
            }
        }

        public async Task<User>? Login(LoginDTO data)
        {
            try
            {
                User user = await _context.Users.Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                .Include(u => u.Reviews)
                .Where(item => item.Email == data.Email)
                .FirstOrDefaultAsync();
                if (user == null) return null;

                bool isValid = BCrypt.Net.BCrypt.Verify(data.Password, user.PasswordHash);
                return isValid ? user : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar sesion: {ex.Message}");
                _logger.LogError(ex, "Error iniciar sesion");
                return null;
            }
        }
    }
}
