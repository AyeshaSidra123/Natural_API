﻿using Natural_Core;
using Natural_Core.IRepositories;
using Natural_Core.Models;
using Natural_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Natural_Data.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NaturalsContext _context;

        private ILoginRepository _loginRepository;

        private IDistributorRepository _distributorRepository;
        private ICityRepository _cityRepository;
        private IAreaRepository _areaRepository;
        private IStateRepository _stateRepository;
        private ICategoryRepository _categoryRepository;
        private IRetailorRepository _retailorRepository;
        private IExecutiveRepository _executiveRepository;

        private IDsrRepository _dsrRepository;
        private IAssignDistributorToExecutiveRepository _dstributorToExecutiveRepository;
        private IRetailor_To_Distributor_Repository _retailor_To_Distributor_Repository;

      



        public UnitOfWork(NaturalsContext context)
        {
            _context = context;
        }

        public ILoginRepository Login => _loginRepository = _loginRepository ?? new LoginRepository(_context);
        public IDistributorRepository DistributorRepo => _distributorRepository = _distributorRepository ?? new DistributorRepository(_context);
        public IExecutiveRepository ExecutiveRepo => _executiveRepository = _executiveRepository ?? new ExecutiveRepository(_context);
        public ICityRepository CityRepo => _cityRepository = _cityRepository ?? new CityRepository(_context);
        public IStateRepository StateRepo => _stateRepository = _stateRepository ?? new StateRepository(_context);

        public IAreaRepository AreaRepo => _areaRepository= _areaRepository ?? new AreaRepository(_context);

        public ICategoryRepository CategoryRepo => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public IRetailorRepository RetailorRepo  => _retailorRepository = _retailorRepository ?? new RetailorRepository(_context);


        public IDsrRepository dSRRepo => _dsrRepository = _dsrRepository = _dsrRepository ?? new DsrRepository(_context);

        public IAssignDistributorToExecutiveRepository distributorToExecutiveRepo => _dstributorToExecutiveRepository = _dstributorToExecutiveRepository ?? new AssignDistributorToExecutiveRepository(_context);
        public IRetailor_To_Distributor_Repository Retailor_To_Distributor_RepositoryRepo => _retailor_To_Distributor_Repository ?? new Retailor_To_Distributor_Repository(_context);



        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}


