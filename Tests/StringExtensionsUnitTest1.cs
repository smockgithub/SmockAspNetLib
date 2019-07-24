using SmockAspNetLib.Infrastructure.Extensions;
using System;
using Xunit;

namespace Tests
{
    public class StringExtensionsUnitTest1
    {
        //[Fact]
        //[Theory(DisplayName = "Í¨¹ý²âÊÔ")]
        [Theory]
        [InlineData("123")]
        [InlineData("abc", Skip = "Ìø¹ý²âÊÔ")]
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

        [Theory]
        [InlineData("2019-07-07 18:00:00")]
        public void Test_AsUtcDateTimeByBeiJingTimeZone(string value)
        {
            var TestResult = value.AsUtcDateTimeByBeiJingTimeZone(DateTime.Now);

            //int a = 1;
            //a.AA();

            //DateTime dt = new DateTime();
            //dt.AA1();

            //df().AAA();

            //Assert.Equal(true, TestResult);
            Assert.True(TestResult == new DateTime(2019,07,07,10,00,00,DateTimeKind.Utc));
        }

        //private int df()
        //{
        //    return 1;
        //}
    }
}
