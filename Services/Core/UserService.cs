using AutoMapper;
using Data.DataAccess;
using Data.Entities;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core
{
    public interface IUserService
    {
        Task<ResultModel> Register(UserCreateModel model);
        Task<ResultModel> Login(LoginModel model);

        ResultModel Update(UserUpdateModel model);
        ResultModel GetAll();
    }
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserService(AppDbContext dbContext, IMapper mapper, IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ResultModel GetAll()
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.User;
                var view = _mapper.ProjectTo<UserModel>(data);
                result.Data = view;
                result.Succeed = true;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public async Task<ResultModel> Login(LoginModel model)
        {
            var result = new ResultModel();
            result.Succeed = false;
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var check = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (!check.Succeeded)
                {
                    if (!user.EmailConfirmed)
                    {
                        //await SendMailConfirm(user);
                        result.ErrorMessage = "Unconfirmed Email. Please check email for confirm!";
                    }
                    else
                    {
                        result.ErrorMessage = "Password is wrong!";
                    }
                }
                else
                {
                    var token = GetAccessToken(user);
                    result.Data = token;
                    result.Succeed = true;
                }
            }
            else
            {
                result.ErrorMessage = "Username not found!";
            }
            return result;
        }

        public async Task<ResultModel> Register(UserCreateModel model)
        {
            var result = new ResultModel();
            result.Succeed = false;
            try
            {
                var user = new User
                {
                    UserName = model.UserName.Trim(),
                    Email = model.Email.Trim(),
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    Address = model.Address.Trim(),
                    PhoneNumber = model.PhoneNumber.Trim(),
                    NormalizedEmail = model.Email.Trim(),
                    IsActive = true,
                    Gender = true
                };
                var check = await _userManager.CreateAsync(user, model.Password);
                if (!check.Succeeded)
                {
                    result.ErrorMessage = check.ToString();
                    return result;
                }
                result.Succeed = true;
                result.Data = user.Id;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;

        }

        ResultModel IUserService.Update(UserUpdateModel model)
        {
            throw new NotImplementedException();
        }
        private Token GetAccessToken(User user)
        {
            List<Claim> claims = GetClaims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddHours(int.Parse(_configuration["Jwt:ExpireTimes"])),
              //int.Parse(_configuration["Jwt:ExpireTimes"]) * 3600
              signingCredentials: creds);

            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token
            {
                Access_token = serializedToken,
                Token_type = "Bearer",
                Expires_in = int.Parse(_configuration["Jwt:ExpireTimes"]) * 3600,
                UserId = user.Id.ToString(),
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }
        private List<Claim> GetClaims(User user)
        {
            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim> {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("FullName", user.FirstName),
                new Claim("UserName", user.UserName)
            };
            if (!string.IsNullOrEmpty(user.PhoneNumber)) claims.Add(new Claim("PhoneNumber", user.PhoneNumber));
            return claims;
        }

    }

}
