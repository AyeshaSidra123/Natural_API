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
        Task<Retailor> GetRetailorDetailsById(string distributorId);
        Task<ResultResponse> CreateRetailorWithAssociationsAsync(Retailor distributor);
        Task<ResultResponse> UpdateRetailors(Retailor existingRetailor, Retailor retailor);

        Task<ResultResponse> DeleteRetailor(string retailorId);
        Task<Retailor> GetRetailorsById(string retailorId);
        Task<IEnumerable<Retailor>> SearcRetailors(SearchModel search);




    }
}
