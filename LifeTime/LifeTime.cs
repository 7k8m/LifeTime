using System;

namespace LifeTime
{
    public class LifeTime<T> where T : IDisposable
    {
        public delegate T FactoryMethod();
        private FactoryMethod _createInstance;

        public delegate void Story(T obj);
        public delegate R Story<R>(T obj);

        public LifeTime(FactoryMethod createInstance)
        {
            _createInstance = createInstance;
        }

        public void Complete(Story s)
        {
            using (T obj = _createInstance())
            {
                s(obj);
            }
        }

        public R Complete<R>(Story<R> s)
        {
            using (T obj = _createInstance())
            {
                return s(obj);
            }
        }
    }
}
