﻿using Natural_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Natural_Core.IRepositories
{
    public interface IExecutiveRepository:IRepository<Executive>
    {
        Task<List<Executive>> GetAllExectivesAsync();

        Task<List<Executive>> GetAll();
        Task<Executive> GetWithExectiveByIdAsync(string id);

        //Task<Executive> GetByIdAsync(string id);

    }
}