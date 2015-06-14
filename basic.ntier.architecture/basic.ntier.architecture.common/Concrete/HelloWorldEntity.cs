using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using basic.ntier.architecture.common.Abstract;

namespace basic.ntier.architecture.common.Concrete
{
    public class HelloWorldEntity : IHelloWorldEntity
    {
        public string SayHello()
        {
            return "Hello World";
        }
    }
}
