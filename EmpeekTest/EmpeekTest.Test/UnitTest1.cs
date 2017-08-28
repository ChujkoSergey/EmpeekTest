using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmpeekTest.Model.Contexts;
using System.Diagnostics;

namespace EmpeekTest.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var temp = MainContext.Instance.Items.GetAll();
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}, Type: {item.TypeId}");
            }
        }
    }
}
