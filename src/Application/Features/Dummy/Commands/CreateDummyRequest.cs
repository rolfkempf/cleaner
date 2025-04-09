namespace Application.Features.Dummy.Commands
{
    public class CreateDummyRequest
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }

        public CreateDummyRequest(string name, string gender, DateTime birthday)
        {
            Name = name;
            Gender = gender;
            Birthday = birthday;
        }
    }
}