using AutoMapper;
using HireMeNowWebApi.Controllers;
using HireMeNowWebApi.Data.UnitOfWorks;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Controllers
{
    public class CompanyControllerMoqTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWorkRepo;
        private readonly CompanyController _controller;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CompanyDto companyDto = new CompanyDto { Id = new Guid("9C5F6C05-8543-4BFF-8988-612CB86810D9"), Name = "Aitrich", Email = "aitrich@gmail.com", Website = "aitrich@gmail.com", Phone = "908897547", About = "Good Company", Vision = "Good", Mission = "Mission", Location = "delhi", Address = "Delhi" };
        private readonly CompanyRegDto companyRegDto = new CompanyRegDto { Id = new Guid("9C5F6C05-8543-4BFF-8988-612CB86810D9"), Name = "Aitrich", Email = "aitrich@gmail.com", Website = "aitrich@gmail.com", Phone = "908897547", About = "Good Company", Vision = "Good", Mission = "Mission", Location = "delhi", Address = "Delhi" };

        private readonly List<Company> companies = new List<Company>
        {
                new Company{Id = new Guid("9C5F6C05-8543-4BFF-8988-612CB86810D9"), Name = "Aitrich", Email = "Wipro@gmail.com", Website = "Wipro@gmail.com", Phone = "908897547", About = "Good Company", Vision = "Good",Mission="Mission",Location="delhi",Address="Delhi" },
                new Company{Id = new Guid("2419d0c8-2a76-4eff-864b-0f091588ac03"), Name = "Wipro", Email = "aitrich@gmail.com", Website = "aitrich@gmail.com", Phone = "9087867564", About = "string", Vision = "Be passionate about clients' success;",Mission="Be global and responsible; Treat each person with respect"}

        };
        public CompanyControllerMoqTest()
        {
            //_mockRepo = new Mock<IJobRepository>();
            _mockUnitOfWorkRepo = new Mock<IUnitOfWork>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfiles>();
            });
            _mapper = configurationProvider.CreateMapper();
            _controller = new CompanyController(_mockUnitOfWorkRepo.Object, _mapper, _userRepository, _companyService,_mockUnitOfWorkRepo.Object);

        }
        [Fact]
        public async Task GET_Companies_Results_Success_Count_2()
        {
            //Arrange  
            CompanyListParams param = new CompanyListParams();
            var listdata = new PagedList<Company>(companies, companies.Count, param.PageNumber, param.PageSize);
            _mockUnitOfWorkRepo.Setup(repo => repo.CompanyRepository.GetAllByFilter(param)).ReturnsAsync(listdata);


            //Act
            var result = await _controller.GetAllCompany(param);
            
            OkObjectResult response = (OkObjectResult)result;

            //Assert
            Assert.NotNull(result);
            var res = (List<CompanyRegDto>)response.Value;
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(res.Count, listdata.Count);

        }
        [Fact]
        public async Task POST_Company_Results_Success()
        {
            //Arrange  
            Company companyToCreate = new Company
            {
                Id = new Guid("7163744e-eb8d-45a4-82a8-2c7816f4526d"),
                Email = "Wipro@gmail.com",
                Website = "Wipro@gmail.com",
                Phone = "908897547",
                About = "Good Company",
                Vision = "Good",
                Mission = "Mission",
                Location = "delhi",
                Address = "Delhi"
            };


            _mockUnitOfWorkRepo.Setup(repo => repo.CompanyRepository.Register(companyToCreate));

            //Act
            var result = await _controller.RegisterCompany(companyRegDto);

            //Assert
            Assert.NotNull(result);
            CreatedResult response = (CreatedResult)result;
            Assert.Equal(201, response.StatusCode);

        }



    }
}
