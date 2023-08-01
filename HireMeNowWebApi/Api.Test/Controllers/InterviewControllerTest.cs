using Api.Test.Fixtures;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Controllers
{
	public class InterviewControllerTest
	{
		protected readonly HttpClient _httpClient;

		public InterviewControllerTest()
		{
			ApiWebApplicationFactory _factory = new ApiWebApplicationFactory();
			_httpClient = _factory.CreateClient();
		}
		[Fact]
		public async Task POST_interview_Shedule_Success()
		{
		
			InterviewDto interviewdto = new  InterviewDto { Id = new Guid("ed3d8914-a950-4f51-0275-08db82d180bf"), JobId = new Guid("02eeda07-ba04-4a92-052c-08db88e79a77"), CompanyId = new Guid("2c8303fb-c1e1-4fa6-a2e1-272472b4beb5"), Date = new DateTime(2023, 07, 28, 12, 30, 0), Location = "Padinjarangadi", Time = "10:11:00" };
			HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(interviewdto), Encoding.UTF8);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			//Act
			var response = await _httpClient.PostAsync("interview/interviewShedule", httpContent);
			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);


		}
		[Fact]
		public async Task POST_interview_Shedule_without_CompanyName_BadRequest()
		{
			//InterviewDto interviewdto = new InterviewDto(Guid.NewGuid(),null, "Senior dotnet developer", "10/12/2023", "TCR", "10.00");
			//HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(interviewdto), Encoding.UTF8);
			//httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			////Act
			//var response = await _httpClient.PostAsync("interview/interviewShedule", httpContent);
			////Assert
			//Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}






	} 
}
