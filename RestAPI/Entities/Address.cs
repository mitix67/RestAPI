namespace RestAPI.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
