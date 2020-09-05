using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Items
{
	public class Item
	{
		private readonly int _hashCode;

		private readonly double _totalMilliseconds;
		private readonly List<Requirement> _totalRequirements;

		protected Item(string name, TimeSpan craftingTime, params Requirement[] requirements)
		{
			Name = name;
			CraftingTime = craftingTime;
			Requirements = requirements;

			_totalMilliseconds = CraftingTime.TotalMilliseconds + Requirements.Sum(r => r.Item._totalMilliseconds);

			_totalRequirements = Requirements.SelectMany(q => q.Item.GetTotalRequirements()).ToList();
			_totalRequirements.AddRange(Requirements);

			unchecked
			{
				_hashCode = 28;

				foreach (var requirement in Requirements)
				{
					_hashCode *= 21;
					_hashCode += requirement.GetHashCode();
				}
			}

			_hashCode = HashCode.Combine(Name, CraftingTime, _hashCode);
		}

		public string Name { get; }
		public TimeSpan CraftingTime { get; }
		public Requirement[] Requirements { get; }

		public IList<Requirement> GetTotalRequirements()
		{
			return _totalRequirements;
		}

		public TimeSpan GetTotalConstructionTime()
		{
			return TimeSpan.FromMilliseconds(_totalMilliseconds);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}
	}
}