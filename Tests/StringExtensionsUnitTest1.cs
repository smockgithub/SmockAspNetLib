using SmockAspNetLib.Infrastructure.Extensions;
using System;
using Xunit;

namespace Tests
{
    public class StringExtensionsUnitTest1
    {
        //[Fact]
        //[Theory(DisplayName = "ͨ������")]
        [Theory]
        [InlineData("123")]
        [InlineData("abc", Skip = "��������")]
        public void Test_IsInt(string value)
        {
            var TestResult = value.IsInt();

            //int a = 1;
            //a.AA();

            //DateTime dt = new DateTime();
            //dt.AA1();

            //df().AAA();

            //Assert.Equal(true, TestResult);
            Assert.True(TestResult);
        }

        //private int df()
        //{
        //    return 1;
        //}
    }
}
