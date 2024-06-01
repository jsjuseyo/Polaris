namespace bwaPolaris.Shared;

public class BaseModel
{
    public string? Keterangan { get; set; }
    public DateTime? WaktuInsert { get; set; }
    public DateTime? WaktuProses { get; set; }
    public string? Creator { get; set; }
    public string? Operator { get; set; }
    public string? Synchronise { get; set; }
}
