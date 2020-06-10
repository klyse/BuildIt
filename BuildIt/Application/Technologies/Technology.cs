using System.Collections.Generic;
using System.Linq;

namespace BuildIt.Application.Technologies
{
	public class Technology
	{
		public Technology(string name, decimal tick, params Requirement[] requirements) : this(name, tick)
		{
			BuildRequirements = requirements.ToHashSet();
		}

		public Technology(string name, decimal tick)
		{
			Name = name;
			Tick = tick;
		}

		public string Name { get; }

		public decimal Tick { get; }
		public ICollection<Requirement> BuildRequirements { get; } = new HashSet<Requirement>();
	}
}