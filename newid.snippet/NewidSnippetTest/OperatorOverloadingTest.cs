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

        [Fact]
        public void Test3()
        {
            SomeId? a = (SomeId?)"a";
            SomeId b = (SomeId)"a";

            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Fact]
        public void Test4()
        {
            SomeId a = (SomeId)"a";
            SomeId? b = (SomeId?)"a";

            Assert.True(a == b);
            Assert.False(a != b);
        }


        [Fact]
        public void Test5()
        {
            SomeId? a = (SomeId?)"a";
            SomeId b = (SomeId)"b";

            Assert.False(a == b);
            Assert.True(a != b);
        }

        [Fact]
        public void Test6()
        {
            SomeId a = (SomeId)"a";
            SomeId? b = (SomeId?)"b";

            Assert.False(a == b);
            Assert.True(a != b);
        }

        [Fact]
        public void Test7()
        {
            SomeId? a = null;
            SomeId b = (SomeId)"a";

            Assert.False(a == b);
            Assert.True(a != b);
        }

        [Fact]
        public void Test8()
        {
            SomeId a = (SomeId)"a";
            SomeId? b = null;

            Assert.False(a == b);
            Assert.True(a != b);
        }

        [Fact]
        public void Test9()
        {
            SomeId? a = null;
            SomeId? b = null;

            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Fact]
        public void Test10()
        {
            SomeId? a = new SomeId(null);
            SomeId? b = null;

            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Fact]
        public void Test11()
        {
            SomeId? a = null;
            SomeId? b = new SomeId(null);

            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Fact]
        public void Test12()
        {
            SomeId? a = new SomeId(null);
            SomeId? b = new SomeId(null);

            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Fact]
        public void Test13()
        {
            SomeId? a = new SomeId(null);
            SomeId? b = (SomeId)"b";

            Assert.True(a != b);
            Assert.False(a == b);
        }

        [Fact]
        public void Test14()
        {
            SomeId? a = (SomeId)"a";
            SomeId? b = new SomeId(null);

            Assert.True(a != b);
            Assert.False(a == b);
        }

        [Fact]
        public void Test15()
        {
            SomeId? a = (SomeId?)"a";
            SomeId? b = (SomeId?)"a";

            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Fact]
        public void Test16()
        {
            SomeId? a = (SomeId?)"a";
            SomeId? b = (SomeId?)"b";

            Assert.True(a != b);
            Assert.False(a == b);
        }
    }
}
