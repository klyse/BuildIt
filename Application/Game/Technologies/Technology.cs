using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Application.Game.Technologies
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
			Identifier = GetType().FullName;
		}

		public string Name { get; }
		public string Identifier { get; }
		public decimal Tick { get; }
		public ICollection<Requirement> BuildRequirements { get; } = new HashSet<Requirement>();
	}
}