
using System.ComponentModel.DataAnnotations;

namespace CengProject.Models
{
    public class Classroom
    {
        public int ClassroomId { get; set; }

        [Required]
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public int Id { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}

