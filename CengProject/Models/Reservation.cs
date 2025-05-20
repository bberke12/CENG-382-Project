using System;
using System.ComponentModel.DataAnnotations;

namespace CengProject.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public string InstructorId { get; set; } = string.Empty;

        public ApplicationUser Instructor { get; set; } = null!;

        [Required]
        public int ClassroomId { get; set; }

        public Classroom Classroom { get; set; }

        [Required]
        public int TermId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public string Status { get; set; } = "Pending";

        public bool IsCancelled { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
