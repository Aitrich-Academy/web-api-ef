using AutoMapper;
using HireMeNowWebApi.Data.UnitOfWorks;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Extensions;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Repositories;
using HireMeNowWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace HireMeNowWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
	public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
		private readonly ICompanyService _companyService;
        public readonly IUnitOfWork _unitofWork;

        public IMapper Mapper { get; }
        public IUnitOfWork Object { get; }

        public CompanyController(IUnitOfWork unitOfWork, IMapper mapper,IUserRepository userRepository,ICompanyService companyService, IUnitOfWork @object)
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
			
             _companyService.memberRegister(companyMemberDto);
            return Ok();
         
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
        public async Task<IActionResult> RegisterCompany([FromForm]CompanyRegDto companyRegDto)
        {
            if (_unitOfWork.CompanyRepository.IsUserExist(companyRegDto.Email))
            {
                return BadRequest("User Already Exist");
            }
            else
            {
                Company company = _mapper.Map<Company>(companyRegDto);
                _unitOfWork.CompanyRepository.Register(company);
                await _unitOfWork.Complete();
                return Created("", companyRegDto);
               
            }
       
           
        }

        [HttpGet("/company/list")]
        //[ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> GetAllCompany([FromQuery] CompanyListParams param)
        {

            var companylist = await _unitOfWork.CompanyRepository.GetAllByFilter(param);
            //Response.AddPaginationHeader(companylist.CurrentPage, companylist.PageSize, companylist.TotalCount, companylist.TotalPages);
            List<CompanyDto> company = _mapper.Map<List<CompanyDto>>(companylist);
            return Ok(company);
           
        }

        [HttpPut("/company/profile")]
        public IActionResult UpdateProfile([FromForm] CompanyDto companyDto)
        {
            if (companyDto.Id==null)
            {
                return BadRequest("Id is required ");
            }
            Company company = _mapper.Map<Company>(companyDto);
            byte[] byteArray = _unitOfWork.CompanyRepository.ConvertImageToByteArray(companyDto.ImageFile);
            company.Logo = byteArray;
            Company updatedCompany = _unitOfWork.CompanyRepository.Update(company);

            return Ok(_mapper.Map<Company>(updatedCompany));
        }


		[HttpPut("/company/logo")]
		public IActionResult UpdateLogo( IFormFile? logo)
		{
			if (logo == null || logo.Length == 0)
				return BadRequest("Image file is required.");
             
			//byte[] byteArray = _unitOfWork.CompanyRepository.ConvertImageToByteArray(logo);

			// Now you can use the byteArray as needed (e.g., save it to the database, manipulate, etc.).

			return Ok("Image successfully uploaded.");
		}

		

	
	}
}
