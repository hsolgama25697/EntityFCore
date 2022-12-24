namespace EFCore.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int RollNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
