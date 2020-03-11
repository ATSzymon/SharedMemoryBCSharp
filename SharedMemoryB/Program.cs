using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharedMemoryB
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> a = new List<int>();
            

            using(MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("14"))
            {
                var mStream = new MemoryStream();
                var binFormatter = new BinaryFormatter();

                // Where 'objectBytes' is your byte array.
                //mStream.Write(objectBytes, 0, objectBytes.Length);


                //var myObject = binFormatter.Deserialize(mStream) as YourObjectType;

                Stopwatch stopwatch = Stopwatch.StartNew();

                
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                    {

                        BinaryReader reader = new BinaryReader(stream);
                    var dupaaa = reader.ReadBytes(4);
                    int bb = BitConverter.ToInt32(dupaaa, 0);
                    //var aa = reader.ReadInt32();




                    //reader.ReadBytes(0);
                    //mStream.Write(reader.ReadBytes(221), 0, reader.ReadBytes(221).Length);
                    //mStream.Position = 0;
                    //var myObject = binFormatter.Deserialize(mStream) as List<int>;



                    //Console.WriteLine(reader.ReadInt32());
                }
                
                stopwatch.Stop();
                Console.ReadKey();
            }

            
        }
    }
}
