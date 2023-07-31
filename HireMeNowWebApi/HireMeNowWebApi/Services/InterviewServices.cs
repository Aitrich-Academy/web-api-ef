using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Repositories;

namespace HireMeNowWebApi.Services
{
	public class InterviewServices : IInterviewServices
	{
		public IInterviewRepository interviewRepository;

		public InterviewServices(IInterviewRepository interviewRepository)
		{
			this.interviewRepository = interviewRepository;
		}

		public void removeInterview(Guid id)
		{
			interviewRepository.removeInterview(id);
		}

		public async Task<PagedList<Interview>> sheduledInterviewList(InterviewParams param)
		{
			return await interviewRepository.sheduledInterviewList(param);
		}

		public Interview sheduleinterview(Interview interview)
		{
			return interviewRepository.shduleInterview(interview);
		}
	
	}
}
