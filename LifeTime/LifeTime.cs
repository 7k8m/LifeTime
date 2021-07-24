using System;

namespace LifeTime
{
    /// <summary>
    /// Module to handle instantiating an object and story of code over it
    /// </summary>
    /// <typeparam name="T">The type of object</typeparam>
    public class LifeTime<T> where T : IDisposable
    {
        /// <summary>
        /// Delegate of factory method for type T objecct
        /// </summary>
        /// <returns>An instance of T </returns>
        public delegate T FactoryMethod();
        private FactoryMethod _createInstance;

        /// <summary>
        /// Delegate of method through over life time of type T object
        /// </summary>
        /// <param name="obj">Object of T</param>
        public delegate void Story(T obj);

        /// <summary>
        /// Delegate of method through over life time of type T object with result
        /// </summary>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="obj">Object of T</param>
        /// <returns>result value</returns>
        public delegate R Story<R>(T obj);

        /// <summary>
        /// Constructor of LifeTime
        /// </summary>
        /// <param name="createInstance">A delegate of factory method for T</param>
        public LifeTime(FactoryMethod createInstance)
        {
            _createInstance = createInstance;
        }

        /// <summary>
        /// Execute whole story of type T object and dispose it.
        /// </summary>
        /// <param name="s">Delegate of story</param>
        public void Complete(Story s)
        {
            using (T obj = _createInstance())
            {
                s(obj);
            }
        }

        /// <summary>
        /// Execute whole story of type T object and dispose it with result.
        /// </summary>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="s">Delegate of story</param>
        /// <returns>result value</returns>
        public R Complete<R>(Story<R> s)
        {
            using (T obj = _createInstance())
            {
                return s(obj);
            }
        }
    }
}
