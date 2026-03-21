namespace DonKeyNielas.Entities;
public class InviteCode
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int QtyUsed { get; set; }
    public int MaxUses { get; set; }
}
