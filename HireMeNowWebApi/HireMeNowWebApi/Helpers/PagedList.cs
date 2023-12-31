﻿using HireMeNowWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Helpers
{
	public class PagedList<T> : List<T>
	{
		private Interview interview;
		private object count;
		private int pageNumber;

		public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
		{
			CurrentPage = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			PageSize = pageSize;
			TotalCount = count;
			AddRange(items);
		}

		public PagedList(Interview interview, object count, int pageNumber, int pageSize)
		{
			this.interview = interview;
			this.count = count;
			this.pageNumber = pageNumber;
			PageSize = pageSize;
		}

		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }

		public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber,int pageSize)
		{
			var count = await source.CountAsync();
			var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
			return new PagedList<T>(items, count, pageNumber, pageSize);
		}
	}
}
