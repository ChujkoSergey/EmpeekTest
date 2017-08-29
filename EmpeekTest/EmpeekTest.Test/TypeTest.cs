
namespace EmpeekTest.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EmpeekTest.Model.Contexts;
    using System.Diagnostics;
    using System.Collections.Generic;

    [TestClass]
    public class TypeTest
    {
        [TestMethod]
        public void Select()
        {
            var temp = MainContext.Instance.Type.GetAll();
            Assert.IsNotNull(temp);
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}");
            }
        }

        [TestMethod]
        public void Insert()
        {
            Assert.IsTrue(MainContext.Instance.Type.Insert(new Model.Models.Type() { Name = "Test type from unit test"}));
        }

        [TestMethod]
        public void SelectWhere()
        {
            var temp = MainContext.Instance.Type.GetBy(x => x.Name == "Test type from unit test");
            Assert.IsNotNull(temp);
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}");
            }
        }

        [TestMethod]
        public void Update()
        {
            var temp = (List<Model.Models.Type>)MainContext.Instance.Type.GetBy(x => x.Name == "Test type from unit test");
            Debug.WriteLine("Before update");
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}");
            }
            Assert.IsTrue(MainContext.Instance.Type.Update(new Model.Models.Type() { Name = "Updated test type from unit test"},
                                                            x => x.Name == "Test type from unit test"));
            temp = (List<Model.Models.Type>)MainContext.Instance.Type.GetBy(x => x.Name == "Updated test type from unit test");
            Debug.WriteLine("Updated types");
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}");
            }
        }

        [TestMethod]
        public void Delete()
        {
            Assert.IsTrue(MainContext.Instance.Type.Delete(x => x.Name == "Updated test type from unit test"));
            var temp = MainContext.Instance.Type.GetAll();
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}");
            }
        }
    }
}
