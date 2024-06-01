namespace bwaPolaris.Shared
{
    public class T1AtributForm
    {
        [Key]
        [SQLite.PrimaryKey]
        public long IdAtributForm { get; set; }
        public string? IdForm { get; set; }
        public string? Field { get; set; }
        public bool TampilRekapitulasi { get; set; } = false;
        public bool TampilDetil { get; set; } = false;
        public string? CaptionRekapitulasi { get; set; }
        public string? CaptionDetil { get; set; }
        public bool TampilMobile { get; set; } = false;
    }
}
