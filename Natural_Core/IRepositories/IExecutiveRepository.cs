﻿using Natural_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Natural_Core.IRepositories
{
<<<<<<< HEAD
    public  interface IExecutiveRepository :IRepository<Executive>
    {
        Task<IEnumerable<Executive>> GetAllExecutiveAsync();
=======
    public interface IExecutiveRepository:IRepository<Executive>
    {
        Task<List<Executive>> GetAllExectivesAsync();

        Task<Executive> GetWithExectiveByIdAsync(string id);


    }
}
