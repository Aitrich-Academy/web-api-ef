using AutoMapper;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Enums;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireMeNowWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ApiVersion("1.0")]
	//[Route("api/v{version:apiVersion}/[controller]")]
	
	//[Authorize(Roles = "JOB_PROVIDER")]
	public class InterviewController : ControllerBase
	{

		private readonly IInterviewServices _interviewService;
		private readonly IMapper _mapper;
		private IInterviewServices @object;

		public InterviewController(IMapper mapper,InterviewServices interviewService)
		{
			_interviewService = interviewService;
			_mapper = mapper;
		}

		public InterviewController(IMapper mapper, IInterviewServices @object)
		{
			_mapper = mapper;
			this.@object = @object;
		}

		


		[HttpPost("/interview/interviewShedule")]
		public IActionResult InterviewShedule(InterviewDto interviews)
		{

			Interview interview = _mapper.Map<Interview>(interviews);
			return Ok(_interviewService.sheduleinterview(interview));
		}
		[HttpGet("/interviewSheduledlist")]
		public  async Task<IActionResult> InterviewSheduledList([FromQuery] InterviewParams param)
		{
			var interviews = await _interviewService.sheduledInterviewList(param);
			return Ok(_mapper.Map<List<InterviewDto>>(interviews));

		}
		[HttpDelete("{id}")]
		public IActionResult Removeinterview(Guid id)
		{
			_interviewService.removeInterview(id);
			return NoContent();
		}

	}
}
