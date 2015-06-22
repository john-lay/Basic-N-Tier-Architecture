using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using basic.ntier.architecture.entity.Customer;
using basic.ntier.architecture.repository.Customer;

namespace basic.ntier.architecture.business.HelloWorld
{
    public class HelloWorldManager : IHelloWorldManager
    {
        private readonly ICustomerRepository customerRepository;

        public HelloWorldManager(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public string SayHello()
        {
            return "Hello World";
        }

        public CustomerEntity GetCustomer(long customerId)
        {
            return this.customerRepository.GetCustomer(customerId);
        }
    }
}
