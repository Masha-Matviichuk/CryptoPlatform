using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using DAL.Entities;
using DAL.UoW;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        

        public AccountService(UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRoleService roleService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleService = roleService;
        }
        
        public async Task<IdentityResult> SignUpAsync(SignUpDto data, string roles)
        {
            IdentityResult result = null;
            var userInfo = _mapper.Map<User>(data);
            result = await _userManager.CreateAsync(userInfo
                    , data.Password);

                if (!result.Succeeded)
                {
                    //todo: catch this exception higher in the processing
                    throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
                }

                var roleList = new AssignUserToRolesDto() {Email = userInfo.Email, Roles = new string[]{roles}};
                await _roleService.AssignUserToRolesAsync(roleList);

                return result;
        }

        public async Task<User> SignInAsync(SignInDto data)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == data.Email);
            //todo: catch this exception higher in the processing
            if (user is null) throw new System.Exception($"User not found: '{data.Email}'.");

            return await _userManager.CheckPasswordAsync(user, data.Password) ? user : null;
        }

        public async Task<UserDto> GetUserInfoAsync(string id)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task SoftDeleteAccountAsync(string id)
        {
            var user = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            var userCreds = _userManager.Users.FirstOrDefault(u => u.Email == user.Email);
                if (userCreds != null)
                {
                    userCreds.IsDeleted = true;
                    await _userManager.UpdateAsync(userCreds);
                }
                await _unitOfWork.SaveChangesAsync();
        }

        public async Task HardDeleteAccountAsync(string id)
        {
            var user = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            var userAccount = _userManager.Users.FirstOrDefault(u => u.Email == user.Email);
                await _userManager.DeleteAsync(userAccount);
        }
    }
}