using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using CustomerTechEd2014.DataObjects;
using CustomerTechEd2014.Models;

namespace CustomerTechEd2014.Controllers
{
    public class CustomerController : TableController<CustomerDto>
    {


        private readonly OnPremisesDataContexts context = new OnPremisesDataContexts();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            DomainManager = new CustomerDomainManager(context, Request, Services);
        }

        // GET tables/Customer
        public IQueryable<CustomerDto> GetAllCustomers()
        {
            return Query();
        }

        public SingleResult<CustomerDto> GetCustomer(string id)
        {
            return Lookup(id);
        }


        public Task<CustomerDto> PatchCustomer(string id, Delta<CustomerDto> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostCustomer(CustomerDto item)
        {
            var current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteCustomer(string id)
        {
            return DeleteAsync(id);
        }
    }
}