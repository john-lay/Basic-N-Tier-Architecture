using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using basic.ntier.architecture.entity.Customer;

namespace basic.ntier.architecture.repository.Customer
{
    public interface ICustomerRepository
    {
        CustomerEntity GetCustomer(long customerId);
    }
}
