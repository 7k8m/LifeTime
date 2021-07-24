using NUnit.Framework;
using LifeTime;
using System.IO;

namespace TestLifeTime
{
    public class Tests
    {
        [Test]
        public void Test()
        {
            var lifeTime = 
                new LifeTime<MemoryStream>(() => new MemoryStream());
            lifeTime.Complete((ms) => {
                Assert.IsInstanceOf(typeof(MemoryStream), ms);
            });
        }

        [Test]
        public void TestWithR()
        {
            var lifeTime = 
                new LifeTime<MemoryStream>(() => new MemoryStream());
            var result = 
                lifeTime.Complete<int>((ms) => {
                    Assert.IsInstanceOf(typeof(MemoryStream), ms);
                    return 0;
                });

            Assert.AreEqual(0,result);
        }
    }
}