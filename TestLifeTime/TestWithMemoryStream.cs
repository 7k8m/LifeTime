using NUnit.Framework;
using LifeTime;
using System.IO;
using System.Threading.Tasks;

namespace TestLifeTime
{
    ///Test using MemoryStream as stub object
    public class TestWithMemoryStream
    {
        /// <summary>
        /// Test using MemoryStream as stub object
        /// </summary>
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

        /// <summary>
        /// Test using MemoryStream as stub object with result
        /// </summary>
        [Test]
        public void TestWithResult()
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

        /// <summary>
        /// Test using MemoryStream as stub object with async
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Test using MemoryStream as stub object with async and result
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestWithResultAsync()
        {
            var lifeTimeAsync =
                new LifeTimeAsync<MemoryStream>(async () =>
                {
                    await Task.Delay(1);//dummy step for testing async code
                    return new MemoryStream();
                });
            var result =
                await lifeTimeAsync.Complete<int>(async (ms) =>
                {
                    await Task.Delay(1);//dummy step for testing async code
                    Assert.IsInstanceOf(typeof(MemoryStream), ms);
                    return 0;
                });

            Assert.AreEqual(0, result);
        }
    }
}