namespace apihiro.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address  { get; set; }
        public string? MiddleName { get; set; }
        public int manualColumn { get; set; }
    }
}
