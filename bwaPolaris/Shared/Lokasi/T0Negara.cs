global using SQLite;
global using System.ComponentModel.DataAnnotations;
global using MaxLengthAttribute = System.ComponentModel.DataAnnotations.MaxLengthAttribute;

namespace bwaPolaris.Shared;

public class T0Negara : BaseModel
{
    [Key]
    [PrimaryKey]
    public long IdNegara { get; set; }
    public long? IdNegara_Induk { get; set; }
    public long? IdSektorNegara { get; set; }
    public int? KodeAngka { get; set; }
    [MaxLength(10)]
    public string? KodeTelepon { get; set; }
    [MaxLength(2)]
    public string? KodeISO2 { get; set; }
    [MaxLength(3)]
    public string? KodeISO3 { get; set; }
    [MaxLength(50)]
    public string? Negara { get; set; }
    [MaxLength(50)]
    public string? Country { get; set; }
    public string? Bendera { get; set; }
    [MaxLength(50)]
    public string? Region { get; set; }
    [MaxLength(50)]
    public string? Continent { get; set; }
}
