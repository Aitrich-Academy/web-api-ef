using AutoMapper;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Data.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
		private readonly IMapper _mapper;
		private HireMeNowDbContext _context;
		public ApplicationRepository(HireMeNowDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
        public List<Application> GetAllByCompany(Guid companyId)
        {
            return _context.Applications.Where(e => e.CompanyId == companyId).ToList();
        }
        public void AddApplication(Application application)
        {
			_context.Applications.Add(application);
			_context.SaveChanges();
			//_applications.Add(new Application(job, user, "Pending"));
		}
        public List<Application> GetAll(Guid userId)
        {
            return _context.Applications.Where(e => e.User.Id == userId).Include(x=>x.Job).ToList();



        }
    }
}
