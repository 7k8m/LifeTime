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
    }
}