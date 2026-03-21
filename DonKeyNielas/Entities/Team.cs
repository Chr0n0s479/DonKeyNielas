namespace DonKeyNielas.Entities;
public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageFile { get; set; } = string.Empty;
    public bool Available { get; set; }
}
