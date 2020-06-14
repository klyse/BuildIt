using Application.Game.Technologies;

namespace Application.Game.Jobs
{
	public class Job
	{
		public string Name { get; }

		public Job(string Name)
		{
			this.Name = Name;
		}
	}

	public class MakeJob : Job
	{
		public Technology Tec { get; }

		public MakeJob(Technology tec) : base($"Make {tec.Name}")
		{
			Tec = tec;
		}
	}
}