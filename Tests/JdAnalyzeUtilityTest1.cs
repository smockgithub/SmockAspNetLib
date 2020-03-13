using SmockAspNetLib.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class JdAnalyzeUtilityTest1
    {
        [Theory]
        [InlineData("0000000000011000")]
        //[InlineData("abc", Skip = "跳过测试")]
        public void Test_JdAnalyzeUtility(string value)
        {
            var TestResult = JdUnionUtility.AnalyzeUnionTag(value);

            //int a = 1;
            //a.AA();

            //DateTime dt = new DateTime();
            //dt.AA1();

            //df().AAA();

            Assert.True(TestResult.IsFirstPurchase);

            //Assert.Equal(true, TestResult);
            //Assert.True(TestResult == new DateTime(2019, 3, 1));
            //Assert.ex
        }
    }
}
