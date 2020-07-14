using System.Linq;
using Application.Game.Items;
using Application.Game.Storage;
using NUnit.Framework;

namespace Application.Tests.Game.Storage
{
	public class StorageHandlerTests
	{
		private StorageHandler _storageHandler;

		[SetUp]
		public void Setup()
		{
			_storageHandler = new StorageHandler();
		}

		[Test]
		public void Add()
		{
			_storageHandler.Add(Items.Stone, 3);


			Assert.AreEqual(3, _storageHandler.GetDictionary()[Items.Stone]);
		}

		[Test]
		public void Add_Twice()
		{
			_storageHandler.Add(Items.Stone, 3);
			_storageHandler.Add(Items.Stone, 3);


			Assert.AreEqual(6, _storageHandler.GetDictionary()[Items.Stone]);
		}

		[Test]
		public void Take()
		{
			_storageHandler.Add(Items.Stone, 9);

			var res = _storageHandler.Take(Items.Stone, 3);

			Assert.IsTrue(res);
			Assert.AreEqual(6, _storageHandler.GetDictionary()[Items.Stone]);
		}

		[Test]
		public void Take_Twice()
		{
			_storageHandler.Add(Items.Stone, 9);

			var res1 = _storageHandler.Take(Items.Stone, 3);
			var res2 = _storageHandler.Take(Items.Stone, 3);

			Assert.IsTrue(res1);
			Assert.IsTrue(res2);
			Assert.AreEqual(3, _storageHandler.GetDictionary()[Items.Stone]);
		}

		[Test]
		public void Take_MoreThanAvailable()
		{
			_storageHandler.Add(Items.Stone, 9);

			var res1 = _storageHandler.Take(Items.Stone, 3);
			var res2 = _storageHandler.Take(Items.Stone, 12);

			Assert.IsTrue(res1);
			Assert.IsFalse(res2);
			Assert.AreEqual(6, _storageHandler.GetDictionary()[Items.Stone]);
		}

		[Test]
		public void CanMake_NoDependencies()
		{
			Assert.IsTrue(_storageHandler.CanMake(Items.Stone));
		}

		[Test]
		public void CanMake()
		{
			foreach (var requirement in Items.StoneAxe.BuildRequirements) _storageHandler.Add(requirement.Item, requirement.Quantity);

			Assert.IsTrue(_storageHandler.CanMake(Items.StoneAxe));
		}

		[Test]
		public void CanMake_NotEnoughResources()
		{
			foreach (var requirement in Items.StoneAxe.BuildRequirements) _storageHandler.Add(requirement.Item, requirement.Quantity);

			var tec = Items.StoneAxe.BuildRequirements.First().Item;
			// remove one element
			var res = _storageHandler.Take(tec);

			Assert.IsTrue(res);
			Assert.IsFalse(_storageHandler.CanMake(Items.StoneAxe));
		}

		[Test]
		public void Make()
		{
			var res = _storageHandler.Make(Items.Stone, out _);

			Assert.IsTrue(res);
		}

		[Test]
		public void Make_NoResources()
		{
			var res = _storageHandler.Make(Items.StoneAxe, out _);

			Assert.IsFalse(res);
		}

		[Test]
		public void Make_AddResourcesFirst()
		{
			foreach (var requirement in Items.StoneAxe.BuildRequirements) _storageHandler.Add(requirement.Item, requirement.Quantity);

			var res = _storageHandler.Make(Items.StoneAxe, out _);

			Assert.IsTrue(res);
		}

		[Test]
		public void Make_NotEnoughResources()
		{
			foreach (var requirement in Items.StoneAxe.BuildRequirements) _storageHandler.Add(requirement.Item, requirement.Quantity);

			var tec = Items.StoneAxe.BuildRequirements.First().Item;
			// remove one element
			var res1 = _storageHandler.Take(tec);

			var res2 = _storageHandler.Make(Items.StoneAxe, out _);

			Assert.IsTrue(res1);
			Assert.IsFalse(res2);
		}

		[Test]
		public void Make_Two_NotEnoughResourcesForSecond()
		{
			foreach (var requirement in Items.StoneAxe.BuildRequirements) _storageHandler.Add(requirement.Item, requirement.Quantity * 2);

			var tec = Items.StoneAxe.BuildRequirements.First().Item;
			// remove one element
			var res1 = _storageHandler.Take(tec);

			var res2 = _storageHandler.Make(Items.StoneAxe, out _);
			var res3 = _storageHandler.Make(Items.StoneAxe, out _);

			Assert.IsTrue(res1);
			Assert.IsTrue(res2);
			Assert.IsFalse(res3);
		}

		[Test]
		public void TakeMax_Limit()
		{
			_storageHandler.Add(Items.Stone, 10);

			var cnt = _storageHandler.TakeMax(Items.Stone, 13);

			Assert.AreEqual(10, cnt);
			Assert.AreEqual(0, _storageHandler.GetDictionary()[Items.Stone]);
		}

		[Test]
		public void TakeMax()
		{
			_storageHandler.Add(Items.Stone, 10);

			var cnt = _storageHandler.TakeMax(Items.Stone, 5);

			Assert.AreEqual(5, cnt);
			Assert.AreEqual(5, _storageHandler.GetDictionary()[Items.Stone]);
		}
	}
}