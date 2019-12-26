using System;
using System.Collections.Generic;
using Xunit;

namespace NewidSnippetTest
{
    public class CollectionTest
    {
        [Fact]
        public void SortTest()
        {
            List<SomeId> list = new List<SomeId>
            {
                (SomeId)"c",
                (SomeId)"b",
                (SomeId)"a"
            };

            list.Sort();

            Assert.Equal("a", list[0].Id);
            Assert.Equal("b", list[1].Id);
            Assert.Equal("c", list[2].Id);
        }

        [Fact]
        public void DictionaryTest()
        {
            Dictionary<SomeId, string> dict = new Dictionary<SomeId, string>
            {
                { (SomeId)"a", "1" },
                { (SomeId)"b", "2" },
                { (SomeId)"c", "3" }
            };

            Assert.Equal("1", dict[(SomeId)"a"]);
            Assert.Equal("2", dict[(SomeId)"b"]);
            Assert.Equal("3", dict[(SomeId)"c"]);
        }
    }
}
