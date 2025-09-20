using library_api.Application.DTO.User;

namespace library_api.Application.Interfaces
{
    public interface IUserService : IBaseService<UserResponseDto, CreateUserDto, UpdateUserDto>
    {
        // User account management
        Task LockUserAsync(Guid id);
        Task UnlockUserAsync(Guid id);

        // Password management
        Task ChangePasswordAsync(Guid userId, ChangePasswordDto dto);

    }
}
