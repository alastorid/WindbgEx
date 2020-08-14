using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable StringLiteralTypo

namespace WindbgEx
{
    enum ARCH
    {
        AMD64,
        WOW64,
        X86
    };
    class WindbgEx
    {
        static void windbg(string dumpfile)
        {
            Console.Write($"Loading {dumpfile}...");
            using (var dbg = new dbgeng())
            {
                var result = dbg.OpenDumpFile(dumpfile);
                if (null != result)
                {
                    Console.WriteLine($"{result}");
                    return;
                }

                ARCH arch;

                var lm = dbg.Execute("lm");
                var vertarget = dbg.Execute("vertarget");

                if (lm.Contains("wow64cpu") && lm.Contains("ntdll_"))
                {
                    arch = ARCH.WOW64;
                }
                else if(vertarget.Contains(".amd64"))
                {
                    arch = ARCH.AMD64;
                }
                else
                {
                    arch = ARCH.X86;
                }
                
                if (arch == ARCH.WOW64)
                {
                    Console.WriteLine(dbg.Execute(@"!wow64exts.sw;"));
                }
                //===================================
                
                var line = "=w=";
                while (false == line?.StartsWith("q"))
                {
                    var mre = new ManualResetEvent(false);
                    Console.Write("> ");
                    line = Console.ReadLine();
                    
                    var tt = new Thread(() =>
                    {
                        var bb = new[]
                        {
                            "\r-BUSY-",
                            "\r/BUSY/",
                            "\r-BUSY-",
                            "\r\\BUSY\\",
                        };
                        var i = 0;
                        while (false == mre.WaitOne(333))
                        {
                            Console.Write(bb[i]);
                            i++;
                            if (i == bb.Length)
                            {
                                i = 0;
                            }
                        }
                        Console.Write("\r      \r");
                    }) { IsBackground = true};
                    tt.Start();
                    result = dbg.Execute(line, (m, t) =>
                    {
                        mre.Set();
                        tt.Join();
                        Console.Write(t);
                        return 0;
                    });
                    
                    Console.WriteLine($"{result}");
                }
                //===================================
            }
            Console.WriteLine("bye");
        }
        static void Main(string[] args)
        {
            byte[] inputBuffer = new byte[65535];
            Stream inputStream = Console.OpenStandardInput(inputBuffer.Length);
            Console.SetIn(new StreamReader(inputStream, Console.InputEncoding, false, inputBuffer.Length));

            if (args.Length == 1)
            {
                windbg(args[0]);
            }
        }
    }
}
