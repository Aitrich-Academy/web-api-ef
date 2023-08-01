using AutoMapper;
using HireMeNowWebApi.Data.UnitOfWorks;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Controllers
{
    internal class UserControllerMoqTest
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private readonly Mock<IUnitOfWork> _mockUnitOfWorkRepo;
        public string? Location { get; set; }
        private readonly UserDto userDto = new UserDto { Id = new Guid("9C5F6C05-8543-4BFF-8988-612CB86810D9"),FirstName= "Soudha", LastName="Muneer",Email = "soudhamuneer@gmail.com", Gender = "female", Location = "KKM"};
    }
}
