﻿using Natural_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Natural_Core.IServices
{
    public interface IDsrService
    {
        Task<IEnumerable<Dsr>> GetAllDsr();
        Task<RetailorResponce> CreateDsrWithAssociationsAsync(Dsr dsr);
        
    }
}