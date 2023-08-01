using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
	public interface IInterviewServices
	{
		Interview sheduleinterview(Interview interview);
		Task<PagedList<Interview>> sheduledInterviewList(InterviewParams param);
		void removeInterview(Guid id);

	}
}
