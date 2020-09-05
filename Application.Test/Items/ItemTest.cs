using System;
using System.Linq;
using Application.Items;
using NUnit.Framework;

namespace Application.Test.Items
{
	public class ItemTest
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Item_SelfMethod()
		{
			var allItems = typeof(Item)
				.Assembly
				.GetTypes()
				.Where(r => r.BaseType == typeof(Item))
				.ToList();

			var itemsWithoutSelf = allItems
				.Select(r => new
				{
					Type = r,
					Method = r.GetMethod("get_Self")
				})
				.Where(r => r.Method is null ||
				            !r.Method.IsStatic ||
				            r.Method.ReturnType != r.Type ||
				            r.Type.GetConstructors().Any(q => !q.IsPrivate))
				.ToList();

			foreach (var i in itemsWithoutSelf) Console.WriteLine($"Issue found with {i.Type.FullName}");

			Assert.IsEmpty(itemsWithoutSelf);
		}
	}
}