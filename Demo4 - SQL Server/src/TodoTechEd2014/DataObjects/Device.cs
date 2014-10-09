using System;

namespace CustomerTechEd2014.DataObjects
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid CustomerId { get; set; }
    }
}