using Application.Game.Items;

namespace Application.Game.Jobs
{
	public class MakeJob : Job
	{
		public MakeJob(Item tec) : base($"Make {tec.Name}")
		{
			Tec = tec;
		}

		public Item Tec { get; }
	}
}