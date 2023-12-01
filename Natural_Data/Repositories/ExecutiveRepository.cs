﻿using Microsoft.EntityFrameworkCore;
using Natural_Core.IRepositories;
using Natural_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Natural_Data.Repositories
{
    public class ExecutiveRepository : Repository<Executive>, IExecutiveRepository
    {
        public ExecutiveRepository(NaturalsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Executive>> GetAllExecutiveAsync()
        {
            var executive = await(from executives in NaturalDbContext.Executives
                                  join area in NaturalDbContext.Areas on executives.Area equals area.Id
                                  join city in NaturalDbContext.Cities on area.CityId equals city.Id
                                  join state in NaturalDbContext.States on city.StateId equals state.Id
                                  select new
                                  {
                                      executive = executives,
                                      Area = area,
                                      City = city,
                                      State = state
                                  }).ToListAsync();
            var result = executive.Select(c => new Executive
            {

                FirstName = c.executive.FirstName,
                LastName = c.executive.LastName,
                MobileNumber = c.executive.MobileNumber,
                Address = c.executive.Address,
                Area = c.Area.AreaName,
                Email = c.executive.Email,
                City = c.City.CityName,
                State = c.State.StateName,
            });

            return result;
        }

        private NaturalsContext NaturalDbContext
        {
            get { return Context as NaturalsContext; }
        }
    }
}
