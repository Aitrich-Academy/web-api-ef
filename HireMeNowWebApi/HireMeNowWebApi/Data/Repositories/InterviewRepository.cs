using AutoMapper;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Repositories
{
	
	public class InterviewRepository : IInterviewRepository
	{
		List<Interview> interviews = new ();
		private readonly IMapper _mapper;
		private HireMeNowDbContext _context;
        public InterviewRepository(HireMeNowDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		
		//{ new Interview(new Guid(), "TCS", "Developer", "10/02/2023", "Mumbai", "10.00"), new Interview(new Guid(), "Wipro", "Developer", "11/02/2023", "EKm", "12.00"), new Interview(new Guid(), "anglo", "Accountant", "24/02/2023", "Tcr", "12.00") };
		public Interview shduleInterview(Interview interview)
		{
			_context.Interviews.Add(interview);
			_context.SaveChanges();
			return interview;
			
		}
		public List<Interview> sheduledInterviewList()
		{
			return _context.Interviews.ToList();
		

		}
		public void removeInterview(Guid id)
		{
			Interview interview = _context.Interviews.FirstOrDefault(e => e.Id == id);
			if(interview!=null)
			{
				interviews.Remove(interview);
				_context.SaveChanges();
			}
			
		}

	}
}
