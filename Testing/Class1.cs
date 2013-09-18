using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Sales.Contracts;

namespace Testing
{
    public class Class1
    {
        public void Foo()
        {
            using (var factory = new ChannelFactory<ISalesService>(new BasicHttpBinding(), "http://localhost:9101/sales"))
            {
                ISalesService service = factory.CreateChannel();

                service.FindBooksAvailableForSale();
            }
        }
    }
}
