using AutoMapper;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Repositories;
using HireMeNowWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireMeNowWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "JOBSEEKER")]
	public class JobSeekerController : ControllerBase
	{

		IJobService _jobService;
		IUserService _userService;
		IUserRepository _userRepository;
		IApplicationService _applicationService;
		IMapper _mapper;
		public JobSeekerController(IJobService jobService, IUserService userService, IUserRepository userRepository, IApplicationService applicationService, IMapper mapper)
		{
			_jobService = jobService;
			_userService = userService;
			_userRepository = userRepository;
			_applicationService = applicationService;
			_mapper = mapper;
		}
		[HttpPost]
        [Route("applyjob")]
        public IActionResult ApplyJob(Guid jobId)
		{
			if (jobId != null)
			{

				//bool res = _applicationService.ApplyJob(new Guid(jobId), new Guid(uid));
				var UserId = _userService.GetUserId();


				if (UserId == null)
				{
					return Unauthorized();
				}
				_applicationService.AddApplication(jobId, new Guid(UserId));

				

			}
			else
			{
				return BadRequest("Id is necessary");
			}
			return NoContent();
		}
		[HttpGet]
        [Route("AllAppliedJobs")]
       		public  IActionResult AllAppliedJobs()
		{
            var UserId = _userService.GetUserId();
			List<Application> appliedJobs = _applicationService.GetAll(new Guid(UserId));
			var applicationsDto = _mapper.Map<List<ApplicationDto>>(appliedJobs);
   //     appliedJobs.ForEach((e) =>
   //    {
   //            e.Job = _jobService.getJobById(e.JobId.Value);
			//	e.User = _userService.getById(e.UserId.Value);
			
   //});
            


			return Ok(applicationsDto);
		}

	}

}
