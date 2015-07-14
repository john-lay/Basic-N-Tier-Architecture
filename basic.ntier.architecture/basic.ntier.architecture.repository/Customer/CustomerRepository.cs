using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using basic.ntier.architecture.entity.Customer;
using basic.ntier.architecture.repository.Core;

namespace basic.ntier.architecture.repository.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerEntity GetCustomer(long customerId)
        {
            var sqlAccess = new SqlServerAccess("stp_getCustomer");
            sqlAccess.AddParameter("@CustomerId", customerId);

            return sqlAccess.ExecuteReaderFirstRowIfExists(r => new CustomerEntity
            {
                CustomerId = r.To<long>("Id"),
                FirstName = r.To<string>("FirstName"),
                LastName = r.To<string>("LastName")
            });
        }
    }
}
