
namespace CengProject.Models
{
public class Log
{
    public int LogId { get; set; }
    public string UserEmail { get; set; }
    public string Action { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}

}
