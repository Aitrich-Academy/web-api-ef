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
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Controllers
{
	public class InterviewControllerMoqTests

	{
		private readonly Mock<IInterviewServices> _interviewService;
		private readonly IMapper _mapper;
		private readonly InterviewController _controller;

	//	private readonly InterviewDto interviewdto = new InterviewDto { Id = new Guid("ed3d8914-a950-4f51-0275-08db82d180bf"), JobId = new Guid("02eeda07-ba04-4a92-052c-08db88e79a77"), CompanyId = new Guid("2c8303fb-c1e1-4fa6-a2e1-272472b4beb5"), Date = new DateTime(2023, 07, 28, 12, 30, 0), Location ="Padinjarangadi",Time = "10:11:00" };

	//	private readonly List<Interview> interviews = new Interview { Id = new Guid("ed3d8914-a950-4f51-0275-08db82d180bf"), JobId = new Guid("02eeda07-ba04-4a92-052c-08db88e79a77"), JobseekerId = new Guid("2c8303fv-c1e1-4fa6-a2e1-272472b4beb5"), CompanyId = new Guid("2c8303fb-c1e1-4fa6-a2e1-272472b4beb5"), Date = new DateTime(2023, 07, 28, 12, 30, 0), Location = "Padinjarangadi", Time = new TimeSpan(10, 11, 0) };

	//	//public InterviewControllerMoqTests()
	//	//{

	//	//	//_mockRepo = new Mock<IJobRepository>();


	//	//	_interviewService = new Mock<IInterviewServices>();
	//	//	var configurationProvider = new MapperConfiguration(cfg =>
	//	//	{
	//	//		cfg.AddProfile<AutoMapperProfiles>();
	//	//	});
	//	//	_mapper = configurationProvider.CreateMapper();
	//	//	_controller = new InterviewController(_mapper,_interviewService);

	//	//}
	//	public InterviewControllerMoqTests()
	//	{



	//		_interviewService = new Mock<IInterviewServices>();
	//		var configurationProvider = new MapperConfiguration(cfg =>
	//		{
	//			cfg.AddProfile<AutoMapperProfiles>();
	//		});
	//		_mapper = configurationProvider.CreateMapper();
	//		_controller = new InterviewController(_mapper, _interviewService.Object);

	//	}



	//	[Fact]
	//	public async Task GET_InterView_Results_Success_Count_3()
	//	{
	//		//Arrange  
	//		InterviewParams param = new InterviewParams();
	//		////param.JobTitle = "Developer";
	//		var listdata = new PagedList<Interview >(interviews, interviews.Count, param.PageNumber, param.PageSize);
	//		//_mockUnitOfWorkRepo.Setup(repo => repo.JobRepository.GetAllByFilter(param)).ReturnsAsync(listdata);

	//		//Act
	//		var result = await _controller.InterviewSheduledList(param);
	//		Assert.NotNull(result);
	//		OkObjectResult response = (OkObjectResult)result;

	//		//Assert
	//		var res = (List<InterviewDto>)response.Value;
	//		Assert.Equal(200, response.StatusCode);
	//		Assert.Equal(res.Count, listdata.Count);

	//	}

	}
}
