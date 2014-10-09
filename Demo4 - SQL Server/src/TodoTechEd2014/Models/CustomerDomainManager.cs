using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using CustomerTechEd2014.DataObjects;

namespace CustomerTechEd2014.Models
{
   
    public class CustomerDomainManager : 
        MappedEntityDomainManager<CustomerDto, Customer>
    {
        public CustomerDomainManager(DbContext context, HttpRequestMessage request, ApiServices services) : base(context, request, services)
        {
        }

        public override IQueryable<CustomerDto> Query()
        {
            return base.Query().ToList().AsQueryable();
        }

        public override SingleResult<CustomerDto> Lookup(string id)
        {
            return LookupEntity(x => x.Id.ToString() == id);
        }

        public override Task<CustomerDto> UpdateAsync(string id, Delta<CustomerDto> patch)
        {
            return UpdateEntityAsync(patch, Guid.Parse(id));
        }

        public override Task<bool> DeleteAsync(string id)
        {
            return DeleteItemAsync(Guid.Parse(id));
        }
    }
}