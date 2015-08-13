using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using basic.ntier.architecture.api.Mappings.Customer;

namespace basic.ntier.architecture.api.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<CustomerMap>();
            });
        }
    }
}