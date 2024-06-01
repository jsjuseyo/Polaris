using System.ComponentModel.DataAnnotations;

namespace bwaPolaris.Shared;

public class T1Provinsi : BaseModel
{
    [Key]
    [PrimaryKey]
    public long IdProvinsi { get; set; }
    public long? IdNegara { get; set; }
    [MaxLength(10)]
    public string? KodeBPS { get; set; }
    [MaxLength(10)]
    public string? KodeISO { get; set; }
    public string? Lambang { get; set; }
    [MaxLength(200)]
    public string? Provinsi { get; set; }
    [MaxLength(200)]
    public string? IbuKota { get; set; }
    public int? Populasi { get; set; }
    public decimal? Luas { get; set; }
    [MaxLength(50)]
    public string? StatusKhusus { get; set; }
    [MaxLength(200)]
    public string? Pulau { get; set; }
    [MaxLength(50)]
    public string? MayoritasAgama { get; set; }
    [MaxLength(50)]
    public string? Inisial { get; set; }
}
