using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Entities
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Country { get; set; }
        public int CustomerId { get; set; }
        public IList<Customer> Customers { get; set; }
    }
}
