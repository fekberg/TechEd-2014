using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using CustomerTechEd2014.DataObjects;
using CustomerTechEd2014.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace CustomerTechEd2014
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;


            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>()
                    .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id.ToString()))
                    .ForMember(dst => dst.Firstname, map => map.MapFrom(src => src.Firstname))
                    .ForMember(dst => dst.Lastname, map => map.MapFrom(src => src.Lastname));

                cfg.CreateMap<CustomerDto, Customer>()
                    .ForMember(dst => dst.Id, map => map.MapFrom(src => Guid.Parse(src.Id)))
                    .ForMember(dst => dst.Firstname, map => map.MapFrom(src => src.Firstname))
                    .ForMember(dst => dst.Lastname, map => map.MapFrom(src => src.Lastname));
            });

        }
    }
}

