using Application.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Features.Dummy.Queries
{
    public class GetDummiesQueryHandler : IQueryHandler<GetDummiesQuery, List<DummyDto>>
    {
        public List<DummyDto> Handle(GetDummiesQuery query)
        {
            // Dummy data for demonstration purposes
            return new List<DummyDto>
            {
                new DummyDto("Alice", "Female", DateTime.Now.Year - new DateTime(1990, 1, 1).Year),
                new DummyDto("Bob", "Male", DateTime.Now.Year - new DateTime(1985, 5, 23).Year)
            };
        }
    }
}