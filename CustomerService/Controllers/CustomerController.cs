using CustomerService.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;



namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        List<Store> stores = new List<Store>(){
                new Store() { StoreId=1, StoreName="NewStore", Country = "USA", CustomerId=1},
                new Store() { StoreId=1, StoreName="OldStore", Country = "India", CustomerId=2},
                new Store() { StoreId=1, StoreName="OldStore", Country = "India", CustomerId=3},
                new Store() { StoreId=2, StoreName="OldStore", Country = "India", CustomerId=3},
            };
        List<Customer> customers = new List<Customer>(){
                new Customer() { CustomerId=1, CustomerName="Ashwani"},
                new Customer() { CustomerId=2, CustomerName="Ravi"},
                new Customer() { CustomerId=3, CustomerName="Cust2"},
            };

        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            //var allstore = stores
            //    .Join(customers,
            //    sto => sto.CustomerId,
            //    cust => cust.CustomerId,
            //    (sto, cust) => new { Store = sto, Customer = cust });
            string header = HttpContext.Request.Headers["x-test-country-code"];
            var allStores = from st in stores
                            where st.Country==header
                            join cust in customers on st.CustomerId equals cust.CustomerId
                            select new
                            {
                                StoreName = st.StoreName,
                                CustomerName = cust.CustomerName
                            };
            
            return Ok(allStores);
        }
        [HttpGet("{storeId}/{includeCustomers}")]
        public async Task<IActionResult> GetStore(int storeId, bool includeCustomers=false)
        {
            var allStores = from st in stores
                            where st.StoreId==storeId
                            join cust in customers on st.CustomerId equals cust.CustomerId
                            select new
                            {
                                StoreName = st.StoreName,
                                CustomerName = cust.CustomerName
                            };


            return Ok(allStores);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            customers.Add(new Customer() { CustomerId = customer.CustomerId, CustomerName = customer.CustomerName });

            return Ok();
        }
    }
}
