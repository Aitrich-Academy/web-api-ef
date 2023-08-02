using AutoMapper;
using HireMeNowWebApi.Controllers;
using HireMeNowWebApi.Data.Repositories;
using HireMeNowWebApi.Data.UnitOfWorks;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Controllers
{
    public class UserControllerMoqTest
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserController _controller;
        private readonly Mock<IUnitOfWork> _mockUnitOfWorkRepo;
        public string? Location { get; set; }
        private readonly UserDto userDto = new UserDto { Id = new Guid("9C5F6C05-8543-4BFF-8988-612CB86810D9"), FirstName = "Soudha", LastName = "Muneer", Email = "soudhamuneer@gmail.com", Gender = "female", Location = "KKM", Phone = 9087853532 };
        private readonly List<User> users = new List<User> {
                new User{Id=new Guid("7163744e-eb8d-45a4-82a8-2c7816f4526d"),FirstName= "Soudha", LastName="Muneer",Email = "soudhamuneer@gmail.com", Gender = "female", Location = "KKM",Phone="9087853532"},
                new User{Id=new Guid("8e0095ce-f90c-4f03-003f-08db844f473f"),FirstName= "Soudha", LastName="rasiya",Email = "rasiya@gmail.com", Gender = "female", Location = "GVR",Phone="9087853530"},
                new User{Id=new Guid("e86a5bb8-3c03-4591-b214-8087dd605da5"),FirstName= "vishal", LastName="Minaza",Email = "visahalminza@gmail.com", Gender = "Male", Location = "KKM",Phone="9087853532" }
};

        public UserControllerMoqTest()
        {
            _mockUnitOfWorkRepo = new Mock<IUnitOfWork>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfiles>();
            });
            _mapper = configurationProvider.CreateMapper();
            _controller = new UserController(_userService,_mapper,_mockUnitOfWorkRepo.Object);

        }
        [Fact]
        public async Task GET_User_Results_Success_Count_3()
        {
            //Arrange

            UserListParams param = new UserListParams();

            var listdata = new PagedList<User>(users, users.Count, param.PageNumber, param.PageSize);
            _mockUnitOfWorkRepo.Setup(repo => repo.UserRepository.GetAllByFilter(param)).ReturnsAsync(listdata);
            //Act
            var result = await _controller.GetUserAsync(param);

            OkObjectResult response = (OkObjectResult)result;
            //Assert
            Assert.NotNull(result);
            var res = (List<UserDto>)response.Value;
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(res.Count, listdata.Count);

        }
        [Fact]
        public async Task GET_User_By_Id_Results_Success()
        {
            //Arrange
            Guid userId = new Guid("19e2a06e-5a4b-481b-8b06-13f361828668");
            User userToReturn = new User
            {
                Id = new Guid("7163744e-eb8d-45a4-82a8-2c7816f4526d"),
                FirstName ="Asha",
                LastName = "Sarath",
                Email = "ashasarath@gmail.com",
                Gender= "Female",
                Location="KKM",
                Phone = "789798978"
                
            };

            _mockUnitOfWorkRepo.Setup(repo => repo.UserRepository.getById(userId)).Returns(userToReturn);

            //Act
            var result = _controller.getbyId(userId);
            Assert.NotNull(result);
            OkObjectResult response = (OkObjectResult)result;

            //Assert
            Assert.Equal(200, response.StatusCode);
            var res = (UserDto)response.Value;

            Assert.Equal(res.Id, userToReturn.Id);
            Assert.Equal(res.FirstName, userToReturn.FirstName);
            Assert.Equal(res.LastName, userToReturn.LastName);
            Assert.Equal(res.Gender, userToReturn.Gender);
            Assert.Equal(res.Email, userToReturn.Email);
            Assert.Equal(res.Location, userToReturn.Location);
          


        }

    }
}