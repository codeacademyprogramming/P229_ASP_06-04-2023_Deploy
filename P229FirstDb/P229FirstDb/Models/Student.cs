namespace P229FirstDb.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}
