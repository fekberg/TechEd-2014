namespace TodoTechEd2014.Client
{
    public class Customer
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Fullname
        {
            get { return string.Format("{0} {1}", Firstname, Lastname); }
        }
    }
}