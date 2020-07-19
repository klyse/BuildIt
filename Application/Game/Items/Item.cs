using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Application.Game.Items
{
	public class Item
	{
		public Item(string name, decimal createTick, decimal researchTick, params Requirement[] requirements) : this(name, createTick, researchTick, 1, requirements)
		{
		}

		public Item(string name, decimal createTick, decimal researchTick, decimal durabilityCount, params Requirement[] requirements) : this(name, createTick, researchTick, durabilityCount)
		{
			BuildRequirements = requirements.ToHashSet();
		}

		public Item(string name, decimal createTick, decimal researchTick = 0, decimal durabilityCount = 1)
		{
			Name = name;
			CreateTick = createTick;
			ResearchTick = researchTick;
			DurabilityCount = durabilityCount;
			Identifier = GetType().FullName;
		}

		public string Name { get; }
		public string Identifier { get; }
		public decimal CreateTick { get; }
		public decimal ResearchTick { get; }
		public decimal DurabilityCount { get; }
		public ICollection<Requirement> BuildRequirements { get; } = new HashSet<Requirement>();

		private IEnumerable<(Item, decimal)> GetTotalRequirementsTuple()
		{
			foreach (var r in BuildRequirements)
			{
				yield return new ValueTuple<Item, decimal>(r.Item, r.Quantity);

				foreach (var valueTuple in r.Item.GetTotalRequirementsTuple()) yield return valueTuple;
			}
		}

		public IDictionary<Item, decimal> GetTotalRequirements()
		{
			var totalRequirements = GetTotalRequirementsTuple().GroupBy(c => c.Item1)
				.ToDictionary(c => c.Key, c => c.Sum(r => r.Item2));

			return totalRequirements;
		}
	}
}