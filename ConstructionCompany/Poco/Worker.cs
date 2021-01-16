namespace ConstructionCompany
{ 
    public class Worker
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public long Salary { get; set; }
        public long RoleId { get; set; }
        public long SiteId { get; set; }

        public override string ToString()
        {
            return $"{Id}: Name - {Name}, Phone - {Phone}, Salary - {Salary}";
        }
    }
}