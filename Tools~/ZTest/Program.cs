using System;
using AIO;

namespace ZTest
{
    public class TestData : IBinData
    {
        public long Name;

        public TestData()
        {
            Name = Utils.Random.NextLong(100, 200);
        }

        public TestData(in int min, in int max)
        {
            Name = Utils.Random.NextLong(min, max);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public void Deserialize(IReadData buffer)
        {
            if (buffer.Count == 0) return;
            Name = buffer.ReadInt64();
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public void Serialize(IWriteData buffer)
        {
            buffer.WriteInt64(Name);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name.ToString();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            Name = Utils.Random.NextLong(100, 200);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
        }
    }

    public class TestStorage : StorageFile
    {
        public readonly BinList<TestData> List;
        public readonly BinDictionary<TestData, TestData> Dic;

        /// <summary>
        /// 数据存储
        /// </summary>
        /// <param name="path">存储读取路径</param>
        public TestStorage(in string path) : base(in path)
        {
            List = new BinList<TestData>();
            Dic = new BinDictionary<TestData, TestData>();
            AddBin(List);
            AddBin(Dic);
        }

        protected override void OnDeserialize(IReadData buffer)
        {
            if (buffer.Count == 0) return;
        }

        protected override void OnSerialize(IWriteData buffer)
        {
        }

        protected override void OnReset()
        {
            List.Clear();
            Dic.Clear();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------");
            using (var data = new TestStorage(@"F:\Unity\AIO2021\Packages\com.self.package\Tools~\TableData.json"))
            {
               data.Deserialize();
               // data.List.Clear();
               // data.Dic.Clear();
               // for (var i = 0; i < 20; i++)
               // {
               //     data.List.Add(new TestData());
               //     data.Dic.Add(new TestData(100, 5000), new TestData());
               // }

                Console.WriteLine("------------------");
                CPrint.Log(data.Dic);
                Console.WriteLine("------------------");
                CPrint.Log(data.List);
                Console.WriteLine("------------------");
            }

            Console.WriteLine("------------------");
            Console.Read();
        }
    }
}