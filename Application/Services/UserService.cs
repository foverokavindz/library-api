using library_api.Application.DTO.User;
using library_api.Application.Interfaces;
using library_api.Domain.Entities;
using library_api.Domain.Interfaces;
using library_api.Insfastructure.Repositories;
using BCrypt.Net;

namespace library_api.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> CreateAsync(CreateUserDto dto) {


            // Check if user with email already exists
            var existingUsers = await _userRepository.GetAllAsync();
            if (existingUsers.Any(u => u != null && u.Email == dto.Email))
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), // Hash the password
                Role = dto.Role ?? "User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
                IsLocked = false,
                IsDeleted = false
            };
            
            await _userRepository.AddAsync(user);

            return MapToResponseDto(user);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync() // stydy about linQs
        {
            var users = await _userRepository.GetAllAsync();
            return users
                .Where(u => u != null && !u.IsDeleted)
                .Select(u => MapToResponseDto(u!));
        }

        public async Task<UserResponseDto?> GetByIdAsync(Guid id)
        {
            // Note: There's a type mismatch here - User.Id is int but interface expects Guid
            // This will need to be resolved in the repository layer or interface design
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null || user.IsDeleted)
            {
                return null;
            }

            return MapToResponseDto(user);
        }

        public async Task UpdateAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null || user.IsDeleted)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (!string.IsNullOrEmpty(dto.Email))
            {
                // Check if email is already taken by another user
                var existingUsers = await _userRepository.GetAllAsync();
                if (existingUsers.Any(u => u != null && u.Email == dto.Email && u.Id != user.Id))
                {
                    throw new InvalidOperationException("Email is already taken by another user.");
                }
                user.Email = dto.Email;
            }

            user.Name = dto.Name ?? user.Name;
            user.Role = dto.Role ?? user.Role;
            user.IsActive = dto.IsActive ?? user.IsActive;
            user.IsLocked = dto.IsLocked ?? user.IsLocked;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);

        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null || user.IsDeleted)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Soft delete
            user.IsDeleted = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
        }

        public async Task LockUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null || user.IsDeleted)
            {
                throw new InvalidOperationException("User not found.");
            }

            user.IsLocked = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(Guid userId, ChangePasswordDto dto)
        {
            // Convert int to Guid for repository call - this needs to be addressed in architecture
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u != null && u.Id == userId && !u.IsDeleted);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
            {
                throw new InvalidOperationException("Current password is incorrect.");
            }

            if (dto.NewPassword != dto.ConfirmPassword)
            {
                throw new InvalidOperationException("New password and confirmation do not match.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
        }

        public async Task UnlockUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null || user.IsDeleted)
            {
                throw new InvalidOperationException("User not found.");
            }

            user.IsLocked = false;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
        }

        private static UserResponseDto MapToResponseDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsActive = user.IsActive,
                IsLocked = user.IsLocked
            };
        }
    }
}
