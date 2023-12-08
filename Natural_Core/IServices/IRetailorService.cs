﻿using Natural_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Natural_Core.IServices
{
    public interface IRetailorService
    {
        Task<IEnumerable<Retailor>> GetAllRetailors();
        Task<Retailor> GetRetailorById(string distributorId);
        Task<RetailorResponce> CreateRetailorWithAssociationsAsync(Retailor distributor);

        Task<RetailorResponce> DeleteRetailor(string retailorId);
        Task<Retailor> GetRetailorsById(string retailorId);




    }
}
