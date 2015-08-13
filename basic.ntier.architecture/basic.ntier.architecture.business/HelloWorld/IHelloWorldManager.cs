using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using basic.ntier.architecture.entity.Customer;

namespace basic.ntier.architecture.business.HelloWorld
{
    public interface IHelloWorldManager
    {
        string SayHello();

        CustomerEntity GetCustomer(CustomerEntity customer);
    }
}
