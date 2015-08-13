using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using basic.ntier.architecture.api.Models;
using basic.ntier.architecture.entity.Customer;

namespace basic.ntier.architecture.api.Mappings.Customer
{
    public class CustomerMap: Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CustomerEntity, CustomerModel>();
            Mapper.CreateMap<CustomerModel, CustomerEntity>();
        }
    }
}