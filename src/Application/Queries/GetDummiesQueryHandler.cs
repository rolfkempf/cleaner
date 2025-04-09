using Domain.Entities;
using Application.DTOs;

namespace Application.Queries
{
    public class GetDummiesQueryHandler
    {
        public List<DummyDto> Handle(GetDummiesQuery query)
        {
            // Dummy data for demonstration purposes
            var dummies = new List<Dummy>
            {
                new Dummy { Name = "Alice", Birthday = new DateTime(1990, 1, 1), Gender = "Female" },
                new Dummy { Name = "Bob", Birthday = new DateTime(1985, 5, 23), Gender = "Male" }
            };

            // Map domain entities to DTOs
            return dummies.Select(d => new DummyDto
            {
                Name = d.Name,
                Gender = d.Gender,
                Age = DateTime.Now.Year - d.Birthday.Year
            }).ToList();
        }
    }
}