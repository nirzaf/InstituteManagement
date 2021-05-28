namespace LeLeInstitute.Models
{
    public class CourseAssignment
    {
        public int Id { get; set; }
        public Instructor Instructor { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}