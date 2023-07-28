using AutoMapper;
using HireMeNowWebApi.Data.UnitOfWorks;
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
		private readonly IUnitOfWork _unitOfWork;
		public UserController(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userService=userService;
            _mapper=mapper;
			_unitOfWork = unitOfWork;

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
        public async Task<IActionResult> UpdateProfile([FromForm] UserDto userDto)
        {
			if (userDto.Id == null)
			{
				return BadRequest("Id is required ");
			}
			var userToUpdate= _mapper.Map<User>(userDto);

            byte[] byteArray = _unitOfWork.UserRepository.ConvertImageToByteArray(userDto.ImageFile);
			userToUpdate.Image = byteArray;
;			User user =await _userService.UpdateAsync(userToUpdate);
           
            return Ok(_mapper.Map<UserDto>(User));
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
