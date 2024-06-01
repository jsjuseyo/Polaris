using MassTransit;
using System.ComponentModel.DataAnnotations.Schema;

namespace bwaPolaris.Shared
{
    public class T9User : BaseModel
    {
        [Key]
        [PrimaryKey]
        public Guid IdUser { get; set; } = NewId.NextGuid();
        public string? Nama { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public byte[]? Password { get; set; }
        public string? Role { get; set; } = "";

        public ICollection<T9Privileges>? DaftarPrivileges { get; set; }

    }
}
