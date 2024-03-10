using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubsApi.Data;
using SubsApi.DTOs;
using SubsApi.Interfaces;
using SubsApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace SubsApi.Controllers
{
    public class AccountsController: BaseAPIController
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;   
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountsController> _logger;
        public AccountsController(ILogger<AccountsController> logger , UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("register")] // Post: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDto)
        {
            _logger.LogInformation("Register EndPoint Reached");
            try
            {
                if (await UserExists(registerDto.UserName))
                {
                    _logger.LogInformation("UserName already exist: "+ registerDto.UserName);
                    return BadRequest("UserName is already taken");
                }
                _logger.LogInformation("Creating new UserName: " + registerDto.UserName);
                var user = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    DateOfBirth = DateOnly.FromDateTime(registerDto.DateOfBirth),
                    PhoneNumber = registerDto.PhoneNumber,

                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded)
                {
                    _logger.LogInformation("Failed to create new UserName: " + registerDto.UserName);
                    return BadRequest(result.Errors);
                }
                _logger.LogInformation("Adding roles to UserName: " + registerDto.UserName);

                await _userManager.AddToRolesAsync(user, registerDto.Roles);

                _logger.LogInformation("Creating new JWT token for UserName: " + registerDto.UserName);

                string token = await _tokenService.CreateToken(user);
                            
                var userToReturn = new UserDto
                {
                    UserName = user.UserName,
                    Token = token
                };

                _logger.LogInformation("User created successfully: " + registerDto.UserName);

                return Ok(userToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create user: " + registerDto.UserName + "-Error :" + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            _logger.LogInformation("Login EndPoint Reached");
            try
            {
                var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

                if (user == null)
                {
                    _logger.LogInformation("Login- Invalid UserName:" +loginDto.UserName);

                    return Unauthorized("Invalid UserName or Password");
                }

                var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (!result)
                {
                    _logger.LogInformation("Login- Invalid UserName or Password:" + loginDto.UserName);
                    return Unauthorized("Invalid UserName or Password");
                }

                _logger.LogInformation("Creating new JWT token for UserName: " + loginDto.UserName);
                string token = await _tokenService.CreateToken(user);

                var userToReturn = new UserDto
                {
                    UserName = user.UserName,
                    Token = token
                };

                _logger.LogInformation("User LoggedIn successfully: " + loginDto.UserName);
                return Ok(userToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Login new user: " + loginDto.UserName + "-Error :" + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        private async Task<bool> UserExists(string userName)
        {
            return await _userManager.Users.AnyAsync(x=> x.UserName == userName);
        } 
    }
}
