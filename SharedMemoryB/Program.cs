using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace SharedMemoryB
{
    class Program
    {
        private const int MAX_VARIABLE = 65000;


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct My_Variable_Struct
        {
            UInt32 variable_number;
            UInt32 prev_value;
            UInt32 value;
            byte variable_changed;
            byte log_variable;
            short variable_delta;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct Variable_Struct
        {
            public Variable_Struct(int count)
                : this()
            {
                tab_str = new My_Variable_Struct[count];
            }

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VARIABLE)]
            My_Variable_Struct[] tab_str;
        }



        static void Main(string[] args)
        {

            Variable_Struct my_var_tab = new Variable_Struct(MAX_VARIABLE);

            List<int> a = new List<int>();

            while (true)
            {
                using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("14"))
                {

              

                    Stopwatch stopwatch = Stopwatch.StartNew();



                    SharedMemoryVariable sharedMemoryVariable = new SharedMemoryVariable();
                    My_Variable_Struct variable = new My_Variable_Struct();
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                    {

                        BinaryReader reader = new BinaryReader(stream);
                        //sharedMemoryVariable = (SharedMemoryVariable)binFormatter.Deserialize();
                        var dupaaa = reader.ReadBytes(65200 * System.Runtime.InteropServices.Marshal.SizeOf(typeof(My_Variable_Struct)));

                        int raw_size;

                        raw_size = Marshal.SizeOf(my_var_tab);

                        IntPtr ptr = Marshal.AllocHGlobal(raw_size);

                        Marshal.Copy(dupaaa, 0, ptr, raw_size);

                        my_var_tab = (Variable_Struct)Marshal.PtrToStructure(ptr, my_var_tab.GetType());

                        Marshal.FreeHGlobal(ptr);


                        /*
                        var handle = GCHandle.Alloc(dupaaa, GCHandleType.Pinned);
                        try
                        {
                            My_Variable_Struct s = (My_Variable_Struct)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(My_Variable_Struct));
                        }
                        finally
                        {
                            handle.Free();
                        }
                        //int bb = BitConverter.ToInt32(dupaaa, 0);
                        //var aa = reader.ReadInt32();

                        using (var mStream = new MemoryStream(dupaaa))
                        {
                            //var binFormatter = new BinaryFormatter();
                            //My_Variable_Struct myObject = (My_Variable_Struct)binFormatter.Deserialize(mStream);
                        };

                        Thread.Sleep(20);

                        //reader.ReadBytes(0);
                        //mStream.Write(reader.ReadBytes(65200 * System.Runtime.InteropServices.Marshal.SizeOf(typeof(MyVariable))), 0, reader.ReadBytes(65200* System.Runtime.InteropServices.Marshal.SizeOf(typeof(MyVariable))).Length);
                        //mStream.Position = 0;
                        //MyVariable myObject = (MyVariable)binFormatter.Deserialize(mStream);

    */

                        //Console.WriteLine(reader.ReadInt32());
                    }

                    stopwatch.Stop();
                }
                //Console.ReadKey();
            }

        }
    }
}
