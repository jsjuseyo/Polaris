namespace bwaPolaris.Shared
{
    public class T0Form : BaseModel
    {
        [Key]
        [SQLite.PrimaryKeyAttribute]
        public string IdForm { get; set; } = "";
        public string? IdParent { get; set; } = "";
        public string? Form { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public bool? Status { get; set; } = false;
        public bool HasChild { get; set; } = false;
		public bool IsAbleToReadData { get; set; }
		public bool IsAbleToAddData { get; set; }
		public bool IsAbleToEditData { get; set; }
		public bool IsAbleToDeleteData { get; set; }
	}
}
