namespace DonKeyNielas.Entities;

public class Championship
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
    public ICollection<Match> Matches { get; set; } = new List<Match>();

}
