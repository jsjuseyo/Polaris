using MassTransit;

namespace bwaPolaris.Shared
{
	public class T9DataOption : BaseModel
	{
		[Key]
		[PrimaryKey]
		public long  IdDataOption{ get; set; }
		public string?  Entity{ get; set; }
		public string?  DataOption{ get; set; }
	}
}
