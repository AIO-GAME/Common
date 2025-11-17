using System;

namespace AIO
{
    class MyClass : IPoolable
    {
        public int _data;

        public void Dispose() => this.Free();

        public void Initialize()
        {
            _data = 0;
            Console.WriteLine($"MyClass Initialized ");
        }

        public void Reset()
        {
            _data = 0;
            Console.WriteLine($"MyClass Reset");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var s = Pool.Alloc<MyClass>())
            {
                s._data = 42;
                Console.WriteLine($"MyClass Data: {s._data}");
            }

            using (var s = Pool.Alloc<MyClass>())
            {
                Console.WriteLine($"MyClass Data: {s._data}");
            }

            Console.Read();
        }
    }
}