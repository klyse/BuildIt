using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Game.Technologies
{
	public class Technology
	{
		public Technology(string name, decimal tick, params Requirement[] requirements) : this(name, tick)
		{
			BuildRequirements = requirements.ToHashSet();
		}

		private IEnumerable<(Technology, decimal)> GetTotalRequirementsTuple()
		{
			foreach (var r in BuildRequirements)
			{
				yield return new ValueTuple<Technology, decimal>(r.Technology, r.Quantity);

				foreach (var valueTuple in r.Technology.GetTotalRequirementsTuple())
				{
					yield return valueTuple;
				}
			}
		}

		public IDictionary<Technology, decimal> GetTotalRequirements()
		{
			var totalRequirements = GetTotalRequirementsTuple().GroupBy(c => c.Item1)
				.ToDictionary(c => c.Key, c => c.Sum(r => r.Item2));

			return totalRequirements;
		}

		public Technology(string name, decimal tick)
		{
			Name = name;
			Tick = tick;
			Identifier = GetType().FullName;
		}

		public string Name { get; }
		public string Identifier { get; }
		public decimal Tick { get; }
		public ICollection<Requirement> BuildRequirements { get; } = new HashSet<Requirement>();
	}
}