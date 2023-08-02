using AutoMapper;
using HireMeNowWebApi.Data.UnitOfWorks;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Enums;
using HireMeNowWebApi.Exceptions;
using HireMeNowWebApi.Extensions;
using HireMeNowWebApi.Helpers;
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
    [Authorize(Roles = "JOB_PROVIDER")]
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

		
		public IActionResult GetProfile()
        {
			//var currentUser = HttpContext.User;
			//var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		
            
            return Ok(_userService.GetCurrentUser());
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
	var user =await _userService.UpdateAsync(userToUpdate);
           
            return Ok(_mapper.Map<UserDto>(user));
        }
	

		[HttpGet("/account/getAllUsers")]
        public async Task<IActionResult> GetUserAsync([FromQuery] UserListParams  param)
        {
           var userslist =await _unitOfWork.UserRepository.GetAllByFilter(param);
            //Response.AddPaginationHeader(userslist.CurrentPage,
            //                             userslist.PageSize,
            //                             userslist.TotalCount,
            //                             jobslist.TotalPages);
            List<UserDto> users = _mapper.Map<List<UserDto>>(userslist);
            return Ok(users);

           
		}
	
		[HttpGet("/account/getbyId")]
		public IActionResult getbyId(Guid UId)
		{
			User users = _userService.getById(UId);
            UserDto user1 = _mapper.Map<UserDto>(users);
			if (users == null)
			{
				return BadRequest("Not Found.");

			}
			return Ok(user1);
		}

        public static implicit operator UserController(JobController v)
        {
            throw new NotImplementedException();
        }
    }
}
