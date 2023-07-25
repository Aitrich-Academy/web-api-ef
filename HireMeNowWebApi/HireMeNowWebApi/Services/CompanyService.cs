using AutoMapper;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Repositories;

namespace HireMeNowWebApi.Services
{
    public class CompanyService : ICompanyService
    {
        public IUserRepository _UserRepository;
		private AuthHelper _authHelper;
		private IMapper _mapper;


		public ICompanyRepository _CompanyRepository;

        public CompanyService(IUserRepository userRepository, ICompanyRepository companyRepository,AuthHelper authHelper,IMapper mapper)
        {
            _UserRepository = userRepository;
            _CompanyRepository = companyRepository;
            _authHelper = authHelper;
            _mapper = mapper;
        }

        //public Task<User> memberRegister(User user)
        //{
        //    return _UserRepository.memberRegister(user);
        //}
		public async Task memberRegister(CompanyMemberDto companyMemberDto)
		{
			_authHelper.CreatePasswordHash(companyMemberDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
			User user = _mapper.Map<User>(companyMemberDto);
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;
			user.Role = Enums.Roles.COMPANY_MEMBER;

            //await _UserRepository.memberRegister(user);
            await _UserRepository.memberRegister(user);
		}
	

		public List<User> memberListing(Guid companyId)
        {
            return _UserRepository.memberListing(companyId);
        }
        public void memberDeleteById(Guid id)
        {
            _UserRepository.memberDeleteById(id);
        }



        public Company? Register(Company company)
        {
            return _CompanyRepository.Register(company);
        }


        public List<Company> GetAllCompany(string? name=null)
        {
            return _CompanyRepository.getAllCompanies(name);
        }

        public Company? getCompanyById(Guid id)
        {
            return _CompanyRepository.getById(id);
        }

        public Company Update(Company company)
        {
            return _CompanyRepository.Update(company);
        }





    }
}
