using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace basic.ntier.architecture.api.Models
{
    public class CustomerModel
    {
        public long CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}