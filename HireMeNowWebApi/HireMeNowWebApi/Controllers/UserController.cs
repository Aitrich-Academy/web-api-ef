using AutoMapper;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Enums;
using HireMeNowWebApi.Exceptions;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HireMeNowWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService=userService;
            _mapper=mapper;
        }
        //[HttpPost("/account/register")]
        //public IActionResult Register(UserDto userDto)
        //{
        //    var user = _mapper.Map<User>(userDto);
        //    return Ok(_userService.register(userDto));
        //}

        //[HttpPost("/account/login")]
        //public IActionResult Login(LoginDto loginDto)
        //{
        //    //var user = _mapper.Map<User>(userDto);
        //    var user=_userService.login(loginDto.Email, loginDto.Password);
        //    if(user == null)
        //    {
        //        return BadRequest("Login Failed");
        //    }
        //    return Ok(_mapper.Map<UserDto>(user));
        //}
        [HttpGet("/account/profile")]

		[AllowAnonymous]
		public IActionResult GetProfile()
        {
			var currentUser = HttpContext.User;
			var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		
            
            return Ok(currentUser);
        }
		[AllowAnonymous]
		[HttpPut("/account/profile")]
        public async Task<IActionResult> UpdateProfile(UserDto userDto)
        {
            var userToUpdate= _mapper.Map<User>(userDto);
            User user =await _userService.UpdateAsync(userToUpdate);
           
            return Ok(_mapper.Map<UserDto>(user));
        }
		[AllowAnonymous]

		[HttpGet("/account/getAllUsers")]
		public IActionResult getAllUsers()
		{
            List<User> users = _userService.getAllUsers();
			if (users == null)
			{
				return BadRequest("Not Found.");
			
			}
			return Ok(users);
		}
		[AllowAnonymous]
		[HttpGet("/account/getbyId")]
		public IActionResult getbyId(Guid UId)
		{
			User users = _userService.getById(UId);
			if (users == null)
			{
				return BadRequest("Not Found.");

			}
			return Ok(users);
		}
	}
}
