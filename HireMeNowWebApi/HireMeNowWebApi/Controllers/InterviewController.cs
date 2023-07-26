﻿using AutoMapper;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Enums;
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

		public InterviewController(IInterviewServices interviewService, IMapper mapper)
		{
			_interviewService = interviewService;
			_mapper = mapper;
		}
		[HttpPost("/interview/interviewShedule")]
		public IActionResult InterviewShedule(InterviewDto interviews)
		{

			Interview interview = _mapper.Map<Interview>(interviews);
			return Ok(_interviewService.sheduleinterview(interview));
		}
		[HttpGet("/interviewSheduledlist")]
		public IActionResult InterviewSheduledList()
		{
			List<Interview> interviews = _interviewService.sheduledInterviewList();
			return Ok(interviews);

		}
		[HttpDelete("{id}")]
		public IActionResult Removeinterview(Guid id)
		{
			_interviewService.removeInterview(id);
			return NoContent();
		}

	}
}
