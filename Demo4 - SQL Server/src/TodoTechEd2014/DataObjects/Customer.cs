using System;
using System.Collections.Generic;

namespace CustomerTechEd2014.DataObjects
{

    public class Customer
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<Device> Devices { get;set; }
    }
}