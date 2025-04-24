using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class UserTestService
{
    private readonly IUserRepository _userRepository;

    public UserTestService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            throw new KeyNotFoundException("User not found");

        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAllAsync();
    }

    public void AddUser(User user)
    {
        _userRepository.Add(user);
    }

    public void AddUsers(IEnumerable<User> users)
    {
        foreach (var user in users)
            _userRepository.Add(user);
    }

    public void UpdateUser(User user)
    {
        var exists = _userRepository.GetByIdAsync(user.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("User to update not found");

        _userRepository.Update(user);
    }

    public void DeleteUser(User user)
    {
        var exists = _userRepository.GetByIdAsync(user.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("User not found");

        _userRepository.Remove(user);
    }
}
