using System;

namespace LifeTime
{
    public abstract class LifeTime<T> where T:IDisposable
    {
        public delegate T FactoryMethod();
        private FactoryMethod _createInstance;

        public delegate void Story(T obj);

        public LifeTime(FactoryMethod createInstance)
        {
            _createInstance = createInstance;
        }

        public void GoThrough(Story s)
        {
            using(T obj = _createInstance())
            {
                s(obj);
            }
        } 
    }
}
