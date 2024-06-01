using MassTransit;
using System.ComponentModel.DataAnnotations.Schema;

namespace bwaPolaris.Shared;

public class T9Privileges : BaseModel
{
    [Key]
    [PrimaryKey]
    public Guid IdKonfigurasiAkses { get; set; } = NewId.NextGuid();
    public Guid? IdUser { get; set; }
    public string? IdForm { get; set; }
    public bool IsAbleToReadData { get; set; }
    public bool IsAbleToAddData { get; set; }
    public bool IsAbleToEditData { get; set; }
    public bool IsAbleToDeleteData { get; set; }


    [ForeignKey(nameof(T9Privileges.IdUser))]
    public T9User? T9User { get; set; }

}
