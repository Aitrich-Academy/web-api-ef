﻿using AutoMapper;
using HireMeNowWebApi.Enums;
using HireMeNowWebApi.Exceptions;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
         static List<User> users = new List<User>();
		private HireMeNowDbContext context;
		private IMapper mapper;

		public UserRepository(HireMeNowDbContext context, IMapper mapper)
		{
			this.context=context;
			this.mapper=mapper;
		}

		//{ new User( "jobprovider", "", "jobprovider@gmail.com", 123, "123", Roles.JobProvider,new Guid("ae32ba86-8e8d-4615-aa47-7387159e705d"),new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")),
		// new User( "Yadhu", "", "yadhu.aitrich@gmail.com", 123, "123", Roles.JobSeeker,null,new Guid("1d8303fb-c1e1-4fa6-a2e1-272472b2beb4")),
		// new User( "rs", "", "sad@gmail.com", 123, "123", Roles.CompanyMember,new Guid("1d8303fb-c1e1-4fa6-a2e1-272472b2beb4")),
		//    new User( "arun", "", "arun@gmail.com", 123, "123", Roles.Admin)};

		public User getById(Guid userId)
        {
           User user= context.Users.Where(e =>e.Id==userId).Include(e=>e.Skills).FirstOrDefault();
            return user;
        }

        public User GetUserByEmail(string email)
        {
			//return users.Where(e=>e.Email==email && e.Password==password).FirstOrDefault();
			return context.Users.FirstOrDefault(e => e.Email==email);
		}

        public async Task<User> registerAsync(User user)
        {
			user.Id = Guid.NewGuid();
			//user.Role = Roles.CompanyMember;

			if (context.Users.Where(e => e.Email == user.Email).Count() <= 0)
			{
				await context.Users.AddAsync(user);
				context.SaveChanges();
				return user;
			}
			else
			{
				throw new UserAlreadyExistException(user.Email);
			}
			
        }

        public async Task<User> Update(User updatedUser)
        {
         var usertoUpdate= await context.Users.FirstOrDefaultAsync(item => item.Id == updatedUser.Id);
            if (usertoUpdate != null)
            {
                // Modify the properties of the item at the found index
                usertoUpdate.About = updatedUser.About ?? usertoUpdate.About;
                //usertoUpdate.Experiences = updatedUser.Experiences ?? usertoUpdate.Experiences;
                //usertoUpdate.Educations = updatedUser.Educations ?? usertoUpdate.Educations;
                usertoUpdate.Image = updatedUser.Image ?? usertoUpdate.Image;
                usertoUpdate.FirstName = updatedUser.FirstName??usertoUpdate.FirstName;
                usertoUpdate.LastName = updatedUser.LastName??usertoUpdate.LastName;
                usertoUpdate.Location = updatedUser.Location??usertoUpdate.Location;
                usertoUpdate.Gender = updatedUser.Gender??usertoUpdate.Gender;
                usertoUpdate.Phone = updatedUser.Phone==null?usertoUpdate.Phone : updatedUser.Phone;
                usertoUpdate.Email = updatedUser.Email == null ? usertoUpdate.Email : updatedUser.Email;
                
               
                context.Users.Update(usertoUpdate);
				await context.SaveChangesAsync();

			}
            else
            {
                throw new NotFoundException("User Not Found");
            }

            return usertoUpdate;
        }
		
		public async Task<User> memberRegister(User user)
        {
            user.Id = Guid.NewGuid();
            //user.Role = Roles.CompanyMember;

            if (context.Users.Where(e => e.Email == user.Email).Count()<=0)
            {
			  await context.Users.AddAsync(user);
                context.SaveChanges();
                return user;
            }
            else
            {
                throw new UserAlreadyExistException(user.Email);
            }
        }

        public List<User> memberListing(Guid companyId) 
        {
            var memberList = context.Users.Where(e=>e.Role==Roles.COMPANY_MEMBER && e.CompanyId==companyId).ToList();
            return memberList;
        }

		public User getuser()
		{
			return context.Users.FirstOrDefault();
		}
	

		public List<User> getAllUsers()
		{
            return context.Users.ToList();
		}

        public void memberDeleteById(Guid id)
        {
            User user = context.Users.Where(e => e.Id==id).FirstOrDefault();
            if (user!=null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

		public bool IsUserExist(string email)
		{
           return context.Users.Where(e => e.Email==email).ToList().Count>0;
		}

		public byte[] ConvertImageToByteArray(IFormFile image)
		{
			using (var memoryStream = new MemoryStream())
			{
				image.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}
	}

    

}
