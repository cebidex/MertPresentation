using System;

namespace MertPresentation.Dtos
{
    public class PeopleGetDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public String Name { get; set; }
    }
}
