using Core.Entities;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Tests;

public class RoleTestService
{
    private readonly IRoleRepository _roleRepository;

    public RoleTestService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> GetRoleById(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);

        if (role is null)
            throw new KeyNotFoundException("Role not found");

        return role;
    }

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        return await _roleRepository.GetAllAsync();
    }

    public void AddRole(Role role)
    {
        _roleRepository.Add(role);
    }

    public void AddRoles(IEnumerable<Role> roles)
    {
        foreach (var role in roles)
            _roleRepository.Add(role);
    }

    public void UpdateRole(Role role)
    {
        var exists = _roleRepository.GetByIdAsync(role.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Role to update not found");

        _roleRepository.Update(role);
    }

    public void DeleteRole(Role role)
    {
        var exists = _roleRepository.GetByIdAsync(role.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("Role not found");

        _roleRepository.Remove(role);
    }
}
