namespace Application.DTOs
{
    public class DummyDto
    {
        public int Id { get; set; } // Assuming you have an Id property in your domain entity
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; } // Calculated from Birthday
    }
}