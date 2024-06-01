using System.ComponentModel.DataAnnotations;

namespace bwaPolaris.Shared;

public class T4Kelurahan : BaseModel
{
    [Key]
    [PrimaryKey]
    public long IdKelurahan { get; set; }
    public long? IdKecamatan { get; set; }
    [MaxLength(50)]
    public string? Kelurahan { get; set; }
    [MaxLength(50)]
    public string? KodePos { get; set; }
}
