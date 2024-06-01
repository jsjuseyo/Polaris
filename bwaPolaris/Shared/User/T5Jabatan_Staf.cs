using MassTransit;
using System.ComponentModel.DataAnnotations.Schema;

namespace bwaPolaris.Shared
{
    public class T5Jabatan_Staf
    {
        [Key]
        [PrimaryKey]
        public Guid IdJabatanStaf { get; set; } = NewId.NextGuid();
        public Guid? IdStaf { get; set; }
        public Guid? IdJabatan { get; set; }
        public string? Jabatan { get; set; }

        [ForeignKey(nameof(T5Jabatan_Staf.IdJabatan))]
        public T0Jabatan? T0Jabatan { get; set; }

        [ForeignKey(nameof(T5Jabatan_Staf.IdStaf))]
        public T1Staf? T1Staf { get; set; }
    }
}
