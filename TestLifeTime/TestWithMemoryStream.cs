using NUnit.Framework;
using LifeTime;
using System.IO;
using System.Threading.Tasks;

namespace TestLifeTime
{
    public class Tests
    {
        [Test]
        public void Test()
        {
            var lifeTime =
                new LifeTime<MemoryStream>(() => new MemoryStream());
            lifeTime.Complete((ms) =>
            {
                Assert.IsInstanceOf(typeof(MemoryStream), ms);
            });
        }

        [Test]
        public void TestWithR()
        {
            var lifeTime =
                new LifeTime<MemoryStream>(() => new MemoryStream());
            var result =
                lifeTime.Complete<int>((ms) =>
                {
                    Assert.IsInstanceOf(typeof(MemoryStream), ms);
                    return 0;
                });

            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task TestAsync()
        {
            var lifeTimeAsync =
                new LifeTimeAsync<MemoryStream>(async () => 
                {
                    await Task.Delay(1);
                    return new MemoryStream();
                });
            await lifeTimeAsync.Complete(async (ms) =>
            {
                await Task.Delay(1);
                Assert.IsInstanceOf(typeof(MemoryStream), ms);
            });
        }

        [Test]
        public async Task TestWithRAsync()
        {
            var lifeTimeAsync =
                new LifeTimeAsync<MemoryStream>(async () => 
                {
                    await Task.Delay(1);
                    return new MemoryStream();
                });
            var result =
                await lifeTimeAsync.Complete<int>(async (ms) =>
                {
                    await Task.Delay(1);
                    Assert.IsInstanceOf(typeof(MemoryStream), ms);
                    return 0;
                });

            Assert.AreEqual(0, result);
        }
    }
}