using System.Linq;
using Application.Game.Storage;
using Application.Game.Technologies;
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
			_storageHandler.Add(TechnologyTree.Stone, 3);


			Assert.AreEqual(3, _storageHandler.GetDictionary()[TechnologyTree.Stone]);
		}

		[Test]
		public void Add_Twice()
		{
			_storageHandler.Add(TechnologyTree.Stone, 3);
			_storageHandler.Add(TechnologyTree.Stone, 3);


			Assert.AreEqual(6, _storageHandler.GetDictionary()[TechnologyTree.Stone]);
		}

		[Test]
		public void Take()
		{
			_storageHandler.Add(TechnologyTree.Stone, 9);

			var res = _storageHandler.Take(TechnologyTree.Stone, 3);

			Assert.IsTrue(res);
			Assert.AreEqual(6, _storageHandler.GetDictionary()[TechnologyTree.Stone]);
		}

		[Test]
		public void Take_Twice()
		{
			_storageHandler.Add(TechnologyTree.Stone, 9);

			var res1 = _storageHandler.Take(TechnologyTree.Stone, 3);
			var res2 = _storageHandler.Take(TechnologyTree.Stone, 3);

			Assert.IsTrue(res1);
			Assert.IsTrue(res2);
			Assert.AreEqual(3, _storageHandler.GetDictionary()[TechnologyTree.Stone]);
		}

		[Test]
		public void Take_MoreThanAvailable()
		{
			_storageHandler.Add(TechnologyTree.Stone, 9);

			var res1 = _storageHandler.Take(TechnologyTree.Stone, 3);
			var res2 = _storageHandler.Take(TechnologyTree.Stone, 12);

			Assert.IsTrue(res1);
			Assert.IsFalse(res2);
			Assert.AreEqual(6, _storageHandler.GetDictionary()[TechnologyTree.Stone]);
		}

		[Test]
		public void CanMake_NoDependencies()
		{
			Assert.IsTrue(_storageHandler.CanMake(TechnologyTree.Stone));
		}

		[Test]
		public void CanMake()
		{
			foreach (var requirement in TechnologyTree.StoneAxe.BuildRequirements) _storageHandler.Add(requirement.Technology, requirement.Quantity);

			Assert.IsTrue(_storageHandler.CanMake(TechnologyTree.StoneAxe));
		}

		[Test]
		public void CanMake_NotEnoughResources()
		{
			foreach (var requirement in TechnologyTree.StoneAxe.BuildRequirements) _storageHandler.Add(requirement.Technology, requirement.Quantity);

			var tec = TechnologyTree.StoneAxe.BuildRequirements.First().Technology;
			// remove one element
			var res = _storageHandler.Take(tec);

			Assert.IsTrue(res);
			Assert.IsFalse(_storageHandler.CanMake(TechnologyTree.StoneAxe));
		}

		[Test]
		public void Make()
		{
			var res = _storageHandler.Make(TechnologyTree.Stone, out _);

			Assert.IsTrue(res);
		}

		[Test]
		public void Make_NoResources()
		{
			var res = _storageHandler.Make(TechnologyTree.StoneAxe, out _);

			Assert.IsFalse(res);
		}

		[Test]
		public void Make_AddResourcesFirst()
		{
			foreach (var requirement in TechnologyTree.StoneAxe.BuildRequirements) _storageHandler.Add(requirement.Technology, requirement.Quantity);

			var res = _storageHandler.Make(TechnologyTree.StoneAxe, out _);

			Assert.IsTrue(res);
		}

		[Test]
		public void Make_NotEnoughResources()
		{
			foreach (var requirement in TechnologyTree.StoneAxe.BuildRequirements) _storageHandler.Add(requirement.Technology, requirement.Quantity);

			var tec = TechnologyTree.StoneAxe.BuildRequirements.First().Technology;
			// remove one element
			var res1 = _storageHandler.Take(tec);

			var res2 = _storageHandler.Make(TechnologyTree.StoneAxe, out _);

			Assert.IsTrue(res1);
			Assert.IsFalse(res2);
		}

		[Test]
		public void Make_Two_NotEnoughResourcesForSecond()
		{
			foreach (var requirement in TechnologyTree.StoneAxe.BuildRequirements) _storageHandler.Add(requirement.Technology, requirement.Quantity * 2);

			var tec = TechnologyTree.StoneAxe.BuildRequirements.First().Technology;
			// remove one element
			var res1 = _storageHandler.Take(tec);

			var res2 = _storageHandler.Make(TechnologyTree.StoneAxe, out _);
			var res3 = _storageHandler.Make(TechnologyTree.StoneAxe, out _);

			Assert.IsTrue(res1);
			Assert.IsTrue(res2);
			Assert.IsFalse(res3);
		}

		[Test]
		public void TakeMax_Limit()
		{
			_storageHandler.Add(TechnologyTree.Stone, 10);

			var cnt = _storageHandler.TakeMax(TechnologyTree.Stone, 13);

			Assert.AreEqual(10, cnt);
			Assert.AreEqual(0, _storageHandler.GetDictionary()[TechnologyTree.Stone]);
		}

		[Test]
		public void TakeMax()
		{
			_storageHandler.Add(TechnologyTree.Stone, 10);

			var cnt = _storageHandler.TakeMax(TechnologyTree.Stone, 5);

			Assert.AreEqual(5, cnt);
			Assert.AreEqual(5, _storageHandler.GetDictionary()[TechnologyTree.Stone]);
		}
	}
}