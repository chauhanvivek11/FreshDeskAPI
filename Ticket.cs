namespace FreshDeskAPI;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; }   // Shikayat ka title
    public string Description { get; set; } // Poori baat
    public string Status { get; set; }  // Open ya Closed
}