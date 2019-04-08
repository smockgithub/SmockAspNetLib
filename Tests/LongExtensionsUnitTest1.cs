using SmockAspNetLib.Infrastructure.Extensions;
using System;
using Xunit;

namespace Tests
{
    public class LongExtensionsUnitTest1
    {
        //[Fact]
        //[Theory(DisplayName = "Í¨¹ý²âÊÔ")]
        [Theory]
        [InlineData(1551369600000)]
        //[InlineData("abc", Skip = "Ìø¹ý²âÊÔ")]
        public void Test_ToDateTime(long value)
        {
            var TestResult = value.ToDateTime(true);

            //int a = 1;
            //a.AA();

            //DateTime dt = new DateTime();
            //dt.AA1();

            //df().AAA();

            //Assert.Equal(true, TestResult);
            Assert.True(TestResult == new DateTime(2019,3,1));
        }

        //private int df()
        //{
        //    return 1;
        //}
    }
}
