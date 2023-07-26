using HireMeNowWebApi.Exceptions;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private List<Company> companies = new List<Company>();
        HireMeNowDbContext context;

		public CompanyRepository(HireMeNowDbContext _context, AutoMapper.IMapper _mapper)
        {
            context= _context;
        }

		public byte[] ConvertImageToByteArray(IFormFile image)
		{
	
				using (var memoryStream = new MemoryStream())
				{
					image.CopyTo(memoryStream);
					return memoryStream.ToArray();
				}
			
		}

		public List<Company> getAllCompanies(string? name)
        {
            if(name == null)    
            return context.Companies.ToList();
            else return context.Companies.Where(e=>e.Name==name).ToList();
        }

        public Company? getById(Guid id)
        {
           return context.Companies.Where(c=>c.Id == id).FirstOrDefault();
        }

        public Company? Register(Company company)
        {
            company.Id=Guid.NewGuid();
           context.Companies.Add(company);
            context.SaveChanges();
            return company;
        }

        public Company Update(Company company)
        {
            var indexToUpdate =context.Companies.Where(item => item.Id == company.Id).FirstOrDefault();
            if (indexToUpdate != null)
            {
				// Modify the properties of the item at the found index
				indexToUpdate.Name = company.Name ?? indexToUpdate.Name;

                indexToUpdate.Email = company.Email ?? indexToUpdate.Email;

				indexToUpdate.Website = company.Website ?? indexToUpdate.Website;
				indexToUpdate.Vision = company.Vision ?? indexToUpdate.Vision;
				indexToUpdate.Mission = company.Mission ?? indexToUpdate.Mission;
				indexToUpdate.Location = company.Location ?? indexToUpdate.Location;
				indexToUpdate.Address = company.Address ?? indexToUpdate.Address;
				indexToUpdate.Logo = company.Logo ?? indexToUpdate.Logo;
				indexToUpdate.Phone = company.Phone==null ? indexToUpdate.Phone : company.Phone;
				context.Companies.Update(indexToUpdate);
				 context.SaveChanges();

			}
            else
            {
                throw new NotFoundException("Company Not Found");
            }

            return indexToUpdate;
        }
    }
}
