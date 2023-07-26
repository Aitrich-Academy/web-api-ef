using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
	public interface IApplicationService
	{
		void AddApplication(Guid JobId, Guid UserId);
		List<Application> GetAll(Guid userId);

    }
}
