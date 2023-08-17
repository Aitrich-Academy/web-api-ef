using AutoMapper;
using HireMeNowWebApi.Data.UnitOfWorks;
using HireMeNowWebApi.Enums;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireMeNowWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "JOB_PROVIDER")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProfileController(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userService=userService;
            _mapper=mapper;
            _unitOfWork = unitOfWork;

        }

        [HttpPost]
        [Route("skill")]
        public async Task<IActionResult> AddSkillAsync(string skill)
        {
            Skill skill2 = new();
            skill2.UserId=new Guid( _userService.GetUserId());
            skill2.Title=skill;
            _unitOfWork.UserRepository.AddSkill(skill2);
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpGet]
        [Route("skills")]
        public async Task<IActionResult> GetSkills()
        {
            var userId= new Guid(_userService.GetUserId());
            List<Skill> skills = await _unitOfWork.UserRepository.getSkills(userId);
        
            return Ok(skills);
        }

        [HttpPost]
        [Route("Experience")]
        public async Task<IActionResult> AddExperience(Experience experience)
        {

            experience.UserId=new Guid(_userService.GetUserId());
            
            await _unitOfWork.UserRepository.AddExperience(experience);
           await _unitOfWork.Complete();
            return Ok();
        }

        [HttpPost]
        [Route("Qualification")]
        public async Task<IActionResult> AddQualification(Qualification qualification)
        {

            qualification.UserId=new Guid(_userService.GetUserId());

            await _unitOfWork.UserRepository.AddQualification(qualification);
            await _unitOfWork.Complete();
            return Ok();
        }
    }
}
