using System;
using Xunit;

namespace NewidSnippetTest
{
    public class OperatorOverloadingTest
    {
        [Fact]
        public void Test1()
        {
            SomeId a = (SomeId)"a";
            SomeId b = (SomeId)"a";

            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Fact]
        public void Test2()
        {
            SomeId a = (SomeId)"a";
            SomeId b = (SomeId)"b";

            Assert.True(a != b);
            Assert.False(a == b);
        }
    }
}
