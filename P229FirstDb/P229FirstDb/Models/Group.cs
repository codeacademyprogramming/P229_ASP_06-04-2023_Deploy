namespace P229FirstDb.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int StudentCount { get; set; }

        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<GroupTeacher> GroupTeachers { get; set; }
    }
}
