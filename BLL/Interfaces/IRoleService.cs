using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    public interface IRoleService
    {
        Task AssignUserToRolesAsync(AssignUserToRolesDto assignUserToRoles);
        Task CreateRoleAsync(string roleName);
        Task<IEnumerable<string>> GetRolesAsync(User user);
        IEnumerable<IdentityRole> GetRoles();
    }
}