﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Natural_Core.IRepositories;
using Natural_Core.Models;

namespace Natural_Data.Repositories
{
	public class DsrdetailRepository: Repository<Dsrdetail>, IDsrdetailRepository
    {
		public DsrdetailRepository(NaturalsContext context) : base(context)
        {
		}



        public async Task<IEnumerable<DsrProduct>> GetDsrDetailsByDsrIdAsync(string dsrId)
        {
            var productDetails = await NaturalDbContext.Dsrdetails
                 .Include(c => c.ProductNavigation)
                 
                 .ThenInclude(a => a.CategoryNavigation)
                .Where(d => d.Dsr == dsrId && d.IsDeleted == false)
                .Select(d => new DsrProduct
                {
                    Dsr = d.Dsr,
                    
                    Product = d.ProductNavigation.ProductName,
                    Quantity = d.Quantity,
                    Price = d.Price,
                    Id = d.Id,
                    Category = d.ProductNavigation.CategoryNavigation.CategoryName

                })
                .ToListAsync();

            return productDetails;
        }

        public async Task<IEnumerable<Dsrdetail>> GetDetailTableByDsrIdAsync(string dsrId)
        {
            var result = NaturalDbContext.Dsrdetails.Where(v => v.Dsr == dsrId && v.IsDeleted == false).ToList();

            return result;
        }


        public async Task<IEnumerable<GetProduct>> GetDetailTableAsync(string dsrId)
        {
           


            var productDetails = await NaturalDbContext.Dsrdetails
                .Include(c => c.ProductNavigation)
                .ThenInclude(a => a.CategoryNavigation)
               .Where(d => d.Dsr == dsrId && d.IsDeleted == false)
               .Select(d => new GetProduct
               {
                   //Dsr = d.Dsr,
                   ProductName = d.ProductNavigation.ProductName,
                   Quantity = d.Quantity,
                   Price = d.Price,
                   Id = d.Product,
                   Category = d.ProductNavigation.CategoryNavigation.CategoryName

               })
               .ToListAsync();

            return productDetails;

        }

        private NaturalsContext NaturalDbContext
        {
            get
            {
                return Context as NaturalsContext;
            }
        }

    }
}

