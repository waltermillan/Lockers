using API.DTOs;
using Core.Interfaces;

namespace API.Services;

public class UserDTOService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UserDTOService(IUserRepository userRepository,
                          IRoleRepository roleRepository,
                          ILocationRepository locationRepository,
                          IRentRepository rentRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<UserDTO> GetByIdAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var role = await _roleRepository.GetByIdAsync(user.IdPerfil);

        if (user is null || role is null)
            return null;

        var userDTO = new UserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            IdRole = user.IdPerfil,
            Role = role.Description
        };

        return userDTO;
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        if (users is null || !users.Any())
            return Enumerable.Empty<UserDTO>();

        var usersDTO = new List<UserDTO>();

        foreach (var user in users)
        {
            var role = await _roleRepository.GetByIdAsync(user.IdPerfil);

            if (role is null)
                continue;

            var userDTO = new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                IdRole = user.IdPerfil,
                Role = role.Description
            };

            usersDTO.Add(userDTO);
        }

        return usersDTO;
    }
}
