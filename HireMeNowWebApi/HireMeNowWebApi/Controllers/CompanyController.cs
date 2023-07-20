﻿using AutoMapper;
using HireMeNowWebApi.Data.UnitOfWorks;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Repositories;
using HireMeNowWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireMeNowWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
		private readonly ICompanyService _companyService;



		public CompanyController(IUnitOfWork unitOfWork, IMapper mapper,IUserRepository userRepository,ICompanyService companyService)
        {
			_unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _companyService = companyService;

		}

        [HttpPost("/company/memberRegister")]
		public IActionResult memberRegister(CompanyMemberDto companyMemberDto)
        { 
			if (_userRepository.IsUserExist(companyMemberDto.Email))
            {
				return BadRequest("User Already Exist");
			}
			
            return Ok(_companyService.memberRegister(companyMemberDto));
         
        }
	
		[HttpGet("/company/memberListing")]
        public IActionResult memberListing(Guid companyId) 
        {
            if (companyId == null)
            {
                return BadRequest();
            }
            var companyMembers = _unitOfWork.UserRepository.memberListing(companyId);

            return Ok(_mapper.Map<List<UserDto>>(companyMembers));
            
        }
        [HttpDelete("/company/RemoveMember")]
        public IActionResult memberDelete(Guid id)
        {
            _unitOfWork.UserRepository.memberDeleteById(id);
            return NoContent(); 
        }

        [HttpPost("/company/register")]
        public IActionResult RegisterCompany(CompanyDto companyDto)
        {
            if (companyDto.Name == null)
                return BadRequest("Company Name Is Required ");
            Company company = _mapper.Map<Company>(companyDto);
            return Ok(_unitOfWork.CompanyRepository.Register(company));
        }

        [HttpGet("/company/list")]
        public IActionResult GetAllCompany(Guid? id = null,string? name=null)
        {
            if (id == null)
            {
                List<Company> companies = _unitOfWork.CompanyRepository.getAllCompanies(name);
                return Ok(companies);
            }
            else
            {
                return Ok(_unitOfWork.CompanyRepository.getById(id.Value));
            }
        }

        [HttpPut("/company/profile")]
        public IActionResult UpdateProfile(CompanyDto companyDto)
        {
            if (companyDto.Id==null)
            {
                return BadRequest("Id is required ");
            }
            Company company = _mapper.Map<Company>(companyDto);

            Company updatedCompany = _unitOfWork.CompanyRepository.Update(company);

            return Ok(_mapper.Map<CompanyDto>(updatedCompany));
        }
    }
}
