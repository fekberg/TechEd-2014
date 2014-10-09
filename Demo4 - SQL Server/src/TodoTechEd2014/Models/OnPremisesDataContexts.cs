using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;
using CustomerTechEd2014.DataObjects;

namespace CustomerTechEd2014.Models
{
    public partial class OnPremisesDataContexts : DbContext
    {
        public OnPremisesDataContexts()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasMany(x => x.Devices);
        }
    }
}