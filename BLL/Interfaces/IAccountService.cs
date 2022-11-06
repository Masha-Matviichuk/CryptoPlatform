using System.Threading.Tasks;
using BLL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUpAsync(SignUpDto data, string roles);
        Task<User> SignInAsync(SignInDto data);
        Task<UserDto> GetUserInfoAsync(string id);
        Task SoftDeleteAccountAsync(string id);
        Task HardDeleteAccountAsync(string id);
    }
}