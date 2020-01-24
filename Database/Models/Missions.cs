using System;
using System.ComponentModel.DataAnnotations;

namespace MertPresentation.Models
{
    public class Missions
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Mission { get; set; }
    }
}
