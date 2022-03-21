using Vesuv.TestLibrary;

using Xunit;

namespace Vesuv.Core.Collections
{
    public class MRUTest
    {

        [Fact(DisplayName = "MRU constructor tests")]
        public void MRUConstructorTests()
        {
            ConstructorTests<MRU<int>>
                .For(typeof(int))
                .Fail(new object[] { -1 }, typeof(ArgumentOutOfRangeException), "Capacity is negative")
                .Fail(new object[] { 0 }, typeof(ArgumentOutOfRangeException), "Capacity is 0")
                .Succeed(new object[] { 1 }, "Minimum capacity should be 1")
                .Succeed(new object[] { 10 }, "A positive capacity should be possible")
                .Assert();

            ConstructorTests<MRU<int>>
                .For(typeof(ICollection<int>))
                .Fail(new object[] { new List<int>() }, typeof(ArgumentException), "Empty collection")
                .Succeed(new object[] { new List<int> { 1, 2, 3 } }, "Filled collection with distinct values should be possible")
                .Succeed(new object[] { new List<int> { 1, 1, 3 } }, "Filled collection without distinct values should be possible")
                .Assert();

            ConstructorTests<MRU<int>>
                .For(typeof(int), typeof(ICollection<int>))
                .Fail(new object[] { -1, new List<int> { 1, 2, 3 } }, typeof(ArgumentOutOfRangeException), "Capacity is negative")
                .Fail(new object[] { 0, new List<int> { 1, 2, 3 } }, typeof(ArgumentOutOfRangeException), "Capacity is 0")
                .Succeed(new object[] { 1, new List<int>() }, "Empty collection with positive capacity should be possible")
                .Succeed(new object[] { 1, new List<int>() { 1 } }, "Filled collection with same amount of items as the capacity should be possible")
                .Succeed(new object[] { 1, new List<int>() { 1, 2, 3 } }, "Filled collection with same more items than the capacity should be possible")
                .Assert();
        }

        [Fact(DisplayName = "Capacity tests")]
        public void CapacityTests()
        {
            // Prepare actual test objects
            var mru1 = new MRU<int>();
            var mru2 = new MRU<int>(3);
            var mru3 = new MRU<int>(3, new List<int>());
            var mru4 = new MRU<int>(3, new List<int> { 1 });
            var mru5 = new MRU<int>(3, new List<int> { 1, 2, 3, 4 });
            var mru6 = new MRU<int>(3, new List<int> { 1, 1, 2, 2 });
            var mru7 = new MRU<int>(4, new List<int> { 1, 2, 1, 4 });
            var mru8 = new MRU<int>(new List<int> { 1, 2, 3, 4 });
            var mru9 = new MRU<int>(new List<int> { 1, 2, 1, 4 });

            // Prepare expected properties
            var capacity1 = 10;
            var capacity2 = 3;
            var capacity3 = 3;
            var capacity4 = 3;
            var capacity5 = 3;
            var capacity6 = 3;
            var capacity7 = 4;
            var capacity8 = 4;
            var capacity9 = 4;

            // Do the tests
            Assert.Equal(capacity1, mru1.Capacity);
            Assert.Equal(capacity2, mru2.Capacity);
            Assert.Equal(capacity3, mru3.Capacity);
            Assert.Equal(capacity4, mru4.Capacity);
            Assert.Equal(capacity5, mru5.Capacity);
            Assert.Equal(capacity6, mru6.Capacity);
            Assert.Equal(capacity7, mru7.Capacity);
            Assert.Equal(capacity8, mru8.Capacity);
            Assert.Equal(capacity9, mru9.Capacity);
        }

        [Fact(DisplayName = "Items tests")]
        public void ItemsTests()
        {
            // Prepare actual test objects
            var mru1 = new MRU<int>(new List<int> { 1, 2, 3, 4 });
            var mru2 = new MRU<int>(new List<int> { 1, 2, 1, 4 });
            var mru3 = new MRU<int>(3, new List<int> { 1, 2, 3, 4 });
            var mru4 = new MRU<int>(3, new List<int> { 1, 2, 1, 4 });

            // Prepare expected properties
            var list1 = new List<int> { 1, 2, 3, 4 };
            var list2 = new List<int> { 1, 2, 4 };
            var list3 = new List<int> { 1, 2, 3 };
            var list4 = new List<int> { 1, 2, 4 };

            // Do the tests
            Assert.Equal(list1, mru1.ToList());
            Assert.Equal(list2, mru2.ToList());
            Assert.Equal(list3, mru3.ToList());
            Assert.Equal(list4, mru4.ToList());
        }

        [Fact(DisplayName = "Add items test")]
        public void AddItemsTest()
        {
            // Prepare actual test objects
            var mru = new MRU<int>(3);

            // Prepare expected properties
            var capacity = 3;
            var count1 = 0;
            var count2 = 1;
            var count3 = 2;
            var count4 = 2;
            var count5 = 3;
            var count6 = 3;
            var list1 = new List<int>();
            var list2 = new List<int> { 1 };
            var list3 = new List<int> { 2, 1 };
            var list4 = new List<int> { 1, 2 };
            var list5 = new List<int> { 4, 1, 2 };
            var list6 = new List<int> { 5, 4, 1 };

            // Do the tests
            Assert.Equal(capacity, mru.Capacity);
            Assert.Equal(count1, mru.Count);
            Assert.Equal(list1, mru.ToList());

            mru.Add(1);
            Assert.Equal(capacity, mru.Capacity);
            Assert.Equal(count2, mru.Count);
            Assert.Equal(list2, mru.ToList());

            mru.Add(2);
            Assert.Equal(capacity, mru.Capacity);
            Assert.Equal(count3, mru.Count);
            Assert.Equal(list3, mru.ToList());

            mru.Add(1);
            Assert.Equal(capacity, mru.Capacity);
            Assert.Equal(count4, mru.Count);
            Assert.Equal(list4, mru.ToList());

            mru.Add(4);
            Assert.Equal(capacity, mru.Capacity);
            Assert.Equal(count5, mru.Count);
            Assert.Equal(list5, mru.ToList());

            mru.Add(5);
            Assert.Equal(capacity, mru.Capacity);
            Assert.Equal(count6, mru.Count);
            Assert.Equal(list6, mru.ToList());
        }
    }
}
