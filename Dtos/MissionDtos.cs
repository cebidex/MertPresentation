using System;
using System.ComponentModel.DataAnnotations;

namespace MertPresentation.Dtos
{
    public class MissionAddDto
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Mission { get; set; }
    }
    public class MissionUpdateDto : MissionAddDto
    {
        [Required]
        public Guid? Id { get; set; }
    }
    public class MissionGetDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public String Name { get; set; }
        public String Mission { get; set; }
    }
}