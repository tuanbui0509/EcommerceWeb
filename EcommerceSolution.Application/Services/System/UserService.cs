using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.ViewModels.Catalog.Orders;
using EcommerceSolution.ViewModels.Common;
using EcommerceSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EcommerceSolution.InterfaceService;
using EcommerceSolution.InterfaceRepository.Interface;

namespace EcommerceSolution.Application.Services.System
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IConfiguration config, IUnitOfWork unitOfWork)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResult<string>> AuthencateAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Account not exits");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Login error");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Token:Issuer"],
                _config["Token:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token), roles, "Login successful");
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.IsDeleted = !user.IsDeleted;
            var result = _unitOfWork.CompleteAsync();
            if (result.IsCompletedSuccessfully)
            {
                return new ApiSuccessResult<bool>("Deleted user successful");
            }
            return new ApiErrorResult<bool>("Deleted user unsuccessful");
        }


        public async Task<ApiResult<UserViewModel>> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserViewModel>("User not exits");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var UserViewModel = new UserViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Dob = user.Dob,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                Roles = roles
            };
            return new ApiSuccessResult<UserViewModel>(UserViewModel, (string[])roles);
        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            var query = _userManager.Users;
            var data = await query
                .Select(x => new UserViewModel()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName,
                    Avatar = x.Avatar,
                    Dob = x.Dob
                }).ToListAsync();

            foreach (var item in data)
            {
                var user = await _userManager.FindByIdAsync(item.Id.ToString());
                var roles = await _userManager.GetRolesAsync(user);
                item.Roles = roles;
            }
            return data;
        }

        public async Task<ApiResult<bool>> RegisterAsync(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("User not exits");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email is exits");
            }

            user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                IsDeleted = false,
                CreatedBy = user.FirstName + " " + user.LastName,
                CreatedDate = DateTime.Now,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Register successful");
        }



        public async Task<ApiResult<bool>> UpdateUserAsync(UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != request.Id))
            {
                return new ApiErrorResult<bool>("Email is exits");
            }
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            user.Dob = request.Dob;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Avatar = null;
            user.UpdatedBy = user.FirstName + " " + user.LastName;
            user.UpdatedDate = DateTime.Now;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>("Updated user successful");
            }
            return new ApiErrorResult<bool>("Updated user unsuccessful");
        }

        public Task<List<OrderViewModel>> GetOrdersByUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
