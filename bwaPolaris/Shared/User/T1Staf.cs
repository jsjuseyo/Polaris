using MassTransit;

namespace bwaPolaris.Shared;

public class T1Staf : BaseModel
{
    [Key]
    [PrimaryKey]
    public Guid IdStaf { get; set; } = NewId.NextGuid();
    [Required]
    public string? NamaLengkap { get; set; }
    [Required]
    public string? NamaPanggilan { get; set; }
    [Required]
    public string? Alamat { get; set; }
    public string? Kode { get; set; }
    [Required]
    public string? Email { get; set; }
    public byte[]? Password { get; set; }

}
