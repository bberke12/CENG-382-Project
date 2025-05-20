
namespace CengProject.Models
{
    public class Term
    {
        public int TermId { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }

}
