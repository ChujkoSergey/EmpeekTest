namespace EmpeekTest.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EmpeekTest.Model.Contexts;
    using System.Diagnostics;
    using System.Collections.Generic;

    [TestClass]
    public class ItemsTest
    {
        [TestMethod]
        public void Select()
        {
            var temp = MainContext.Instance.Items.GetAll();
            Assert.IsNotNull(temp);
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}, Type: {item.TypeId}");
            }
        }

        [TestMethod]
        public void Insert()
        {
            Assert.IsTrue(MainContext.Instance.Items.Insert(new Model.Models.Items() { Name = "Test item from unit test", TypeId = 1 }));
        }

        [TestMethod]
        public void SelectWhere()
        {
            var temp = MainContext.Instance.Items.GetBy(x => x.Name == "Test item from unit test");
            Assert.IsNotNull(temp);
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}, Type: {item.TypeId}");
            }
        }

        [TestMethod]
        public void Update()
        {
            var temp = (List<Model.Models.Items>)MainContext.Instance.Items.GetBy(x => x.Name == "Test item from unit test");
            Debug.WriteLine("Before update");
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}, Type: {item.TypeId}");
            }
            Assert.IsTrue(MainContext.Instance.Items.Update(new Model.Models.Items() { Name = "Updated test item from unit test", TypeId = temp[0].TypeId }, 
                                                            x => x.Name == "Test item from unit test"));
            temp = (List<Model.Models.Items>)MainContext.Instance.Items.GetBy(x => x.Name == "Updated test item from unit test");
            Debug.WriteLine("Updated items");
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}, Type: {item.TypeId}");
            }
        }

        [TestMethod]
        public void Delete()
        {
            Assert.IsTrue(MainContext.Instance.Items.Delete(x => x.Name == "Updated test item from unit test"));
            var temp = MainContext.Instance.Items.GetAll();
            foreach (var item in temp)
            {
                Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}, Type: {item.TypeId}");
            }
        }
    }
}
