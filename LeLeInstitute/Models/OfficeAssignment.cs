namespace LeLeInstitute.Models
{
    public class OfficeAssignment
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public Instructor Instructor { get; set; }
    }
}