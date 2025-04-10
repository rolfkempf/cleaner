namespace Domain.Entities
{
    public class Dummy
    {
        public int Id { get; set; } // Assuming you have an Id property in your domain entity
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }

        public Dummy(string name, DateTime birthday, string gender)
        {
            Name = name;
            Birthday = birthday;
            Gender = gender;
        }
    }
}