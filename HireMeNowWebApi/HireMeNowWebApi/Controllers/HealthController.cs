using Microsoft.AspNetCore.Mvc;

namespace HireMeNowWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class HealthController : ControllerBase
	{
		[HttpGet("/health-check")]
		public IActionResult Index()
		{
			return Ok();
		}
	}
}
