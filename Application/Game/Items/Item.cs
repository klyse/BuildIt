using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Game.Items
{
	public class Item
	{
		public Item(string name, decimal tick, params Requirement[] requirements) : this(name, tick)
		{
			BuildRequirements = requirements.ToHashSet();
		}

		public Item(string name, decimal tick)
		{
			Name = name;
			Tick = tick;
			Identifier = GetType().FullName;
		}

		public string Name { get; }
		public string Identifier { get; }
		public decimal Tick { get; }
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