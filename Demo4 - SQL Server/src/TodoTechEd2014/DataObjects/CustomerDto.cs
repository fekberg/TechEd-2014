using Microsoft.WindowsAzure.Mobile.Service;

namespace CustomerTechEd2014.DataObjects
{
    public class CustomerDto : EntityData
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}