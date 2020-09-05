using MongoDB.Bson;

namespace Domain
{
	public class User
	{
		public ObjectId Id { get; set; }
		public string UserId { get; set; }
	}
}