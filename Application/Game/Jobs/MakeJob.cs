using Application.Game.Technologies;

namespace Application.Game.Jobs
{
	public class MakeJob : Job
	{
		public MakeJob(Technology tec) : base($"Make {tec.Name}")
		{
			Tec = tec;
		}

		public Technology Tec { get; }
	}
}