using System;
using System.Threading.Tasks;

namespace LifeTime
{
    /// <summary>
    /// Module to handle instantiating an object and story of code over it asynchronously
    /// </summary>
    /// <typeparam name="T">The type of object</typeparam>
    public class LifeTimeAsync<T> where T : IDisposable
    {
        /// <summary>
        /// Delegate of factory method for task to instantiate type T objecct
        /// </summary>
        /// <returns>An instance of T </returns>
        public delegate Task<T> FactoryMethod();

        /// <summary>
        /// The factory method for task to instantiate T of this LifeTime object
        /// </summary>
        private FactoryMethod _instantiateTaskFactory;

        /// <summary>
        /// Delegate of method to create a task through over life time of type T object
        /// </summary>
        /// <param name="obj">Object of T</param>
        public delegate Task Story(T obj);

        /// <summary>
        /// Delegate of method to create a task through over life time of type T object with result
        /// </summary>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="obj">Object of T</param>
        /// <returns>result value</returns>
        public delegate Task<R> Story<R>(T obj);

        /// <summary>
        /// Constructor of LifeTime
        /// </summary>
        /// <param name="createInstance">A delegate of factory method for task to instantiate T</param>
        public LifeTimeAsync(FactoryMethod instantiateTaskFactory)
        {
            _instantiateTaskFactory = instantiateTaskFactory;
        }

        /// <summary>
        /// Execute whole story of type T object and dispose it.
        /// </summary>
        /// <param name="s">Delegate of story</param>
        public async Task Complete(Story s)
        {
            using (T obj = await _instantiateTaskFactory())
            {
                await s(obj);
            }
        }

        /// <summary>
        /// Execute whole story of type T object and dispose it with result.
        /// </summary>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="s">Delegate of story</param>
        /// <returns>result value</returns>
        public async Task<R> Complete<R>(Story<R> s)
        {
            using (T obj = await _instantiateTaskFactory())
            {
                return await s(obj);
            }
        }
    }
}
