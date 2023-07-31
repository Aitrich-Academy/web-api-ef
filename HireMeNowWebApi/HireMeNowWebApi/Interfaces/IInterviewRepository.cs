using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
	public interface IInterviewRepository
	{
		Interview shduleInterview(Interview interview);
		Task<PagedList<Interview>> sheduledInterviewList(InterviewParams param);
		void removeInterview(Guid id);
		
	}
}
