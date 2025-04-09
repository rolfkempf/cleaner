namespace Application.Features.Dummy.Queries
{
    public class DummyDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; } // Calculated from Birthday

        public DummyDto(string name, string gender, int age)
        {
            Name = name;
            Gender = gender;
            Age = age;
        }
    }
}
