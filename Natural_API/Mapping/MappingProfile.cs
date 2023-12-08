﻿using AutoMapper;
using Natural_API.Resources;
using Natural_Core;
using Natural_Core.Models;

#nullable disable
namespace Natural_API.Mapping
{
    public class MappingProfile : Profile
    {
      
            public MappingProfile()
            {
                // DOMAIN TO RESOURCE 


                CreateMap<Login, LoginResource>();
                CreateMap<Distributor, DistributorGetResource>();
                CreateMap<Distributor, DistributorInsertUpdateResource>();
                CreateMap<State, StateResource>();
                CreateMap<Area, AreaResource>();
                CreateMap<City, CityResource>();
                CreateMap<Category, CategoryResource>();
                CreateMap<Retailor , RetailorResource>();
                CreateMap <Executive, ExecutiveResource>();
              CreateMap <Executive, ExecutiveGetResource>();
            CreateMap<Executive, ExecutiveInsertUpdateResource>();


            
                //We can map like this also
                //CreateMap<City, CityResource>()
                //.ForMember(domain => domain.Id, opt => opt.MapFrom(source => source.Id))
                //.ForMember(domain => domain.CityName, opt => opt.MapFrom(source => source.CityName));



            //// RESOURCE TO DOMAIN

            CreateMap<LoginResource, Login>();
                CreateMap<DistributorResource, Distributor>();
                CreateMap<LoginResource, Login>();
                CreateMap<DistributorGetResource, Distributor>();
                CreateMap<DistributorInsertUpdateResource, Distributor>();
                CreateMap<StateResource, State>();
                CreateMap<AreaResource, Area>();
                CreateMap<CityResource, City>();
                CreateMap<CategoryResource,Category>();
                CreateMap<RetailorResource, Retailor>();
            CreateMap<ExecutiveGetResource, Executive>();

            CreateMap<ExecutiveInsertUpdateResource, Executive>();

           


                CreateMap<ExecutiveResource, Executive>();
                CreateMap<SaveExecutiveResource, Executive>();
                CreateMap<ExecutiveResource, Executive>();



                //// We can map like this also 
                // CreateMap<CityResource, City>()
                //.ForMember(source => source.Id, opt => opt.MapFrom(domain => domain.Id))
                //.ForMember(source => source.CityName, opt => opt.MapFrom(domain => domain.CityName));


            }
        }
}
