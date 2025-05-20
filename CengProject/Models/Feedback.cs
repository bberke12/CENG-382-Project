using System.ComponentModel.DataAnnotations;

namespace CengProject.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        [Required]
        public string InstructorId { get; set; } = null!;

        public ApplicationUser Instructor { get; set; } = null!; // Eklemen gereken navigation property

        [Required]
        public int ClassroomId { get; set; }

        public Classroom Classroom { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime Date { get; set; }
    }
}
