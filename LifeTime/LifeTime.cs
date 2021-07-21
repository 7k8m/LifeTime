using System;

namespace LifeTime
{
    public abstract class LifeTime<T> where T:IDisposable
    {
        public delegate T FactoryMethod();
        private FactoryMethod _createInstance;

        public delegate void Destiny(T obj);

        public LifeTime(FactoryMethod createInstance)
        {
            _createInstance = createInstance;
        }

        public void LiveThrough(Destiny d)
        {
            using(T obj = _createInstance())
            {
                d(obj);
            }
        } 
    }
}
