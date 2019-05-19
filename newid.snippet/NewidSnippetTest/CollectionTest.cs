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
            List<SomeId> list = new List<SomeId>();

            list.Add((SomeId)"c");
            list.Add((SomeId)"b");
            list.Add((SomeId)"a");
            list.Add((SomeId)null);

            list.Sort();

            Assert.Null(list[0].Value);
            Assert.Equal("a", list[1].Value);
            Assert.Equal("b", list[2].Value);
            Assert.Equal("c", list[3].Value);
        }

        [Fact]
        public void DictionaryTest()
        {
            Dictionary<SomeId, string> dict = new Dictionary<SomeId, string>();

            dict.Add((SomeId)"a", "1");
            dict.Add((SomeId)"b", "2");
            dict.Add((SomeId)"c", "3");
            dict.Add((SomeId)null, "4");

            Assert.Equal("1", dict[(SomeId)"a"]);
            Assert.Equal("2", dict[(SomeId)"b"]);
            Assert.Equal("3", dict[(SomeId)"c"]);
            Assert.Equal("4", dict[(SomeId)null]);
        }
    }
}
