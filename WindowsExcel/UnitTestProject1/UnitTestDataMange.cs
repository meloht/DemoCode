using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsExcel;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestDataMange
    {
        [TestMethod]
        public void TestGetData()
        {
            DataMange mange = new DataMange();
            var data = mange.GetData("Sheet1", @"D:\testData2018-7.xlsx");

            Assert.IsNotNull(data);
        }
    }
}
