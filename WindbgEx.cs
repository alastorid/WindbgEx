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
    class WindbgEx
    {
        enum ARCH
        {
            AMD64,
            WOW64,
            X86
        };
        static void windbg(string dumpfile)
        {
            Console.Write(string.Format("Loading {0}...", dumpfile));
            using (var dbg = new dbgeng())
            {
                var result = dbg.OpenDumpFile(dumpfile);
                if (null != result)
                {
                    Console.WriteLine(string.Format("{0}", result));
                    return;
                }

                ARCH arch;

                var lm = dbg.Execute("lm");
                var vertarget = dbg.Execute("vertarget");

                if (lm.Contains("wow64cpu") && lm.Contains("ntdll_"))
                {
                    arch = ARCH.WOW64;
                }
                else if (vertarget.Contains("Free x64"))
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
                //===== add your super hacky script here ======
                //Console.WriteLine(dbg.Execute("vertarget"));

                // prompt
                var line = "=w=";
                while (line != null && !line.StartsWith("q"))
                {
                    var mre = new ManualResetEvent(false);
                    var mreii = new ManualResetEvent(false);
                    Console.Write("> ");
                    line = Console.ReadLine();

                    var sb = new StringBuilder();
                    var tt = new Thread(() =>
                    {
                        var i = 0;
                        var bb = new[]
                        {
                            "\r-BUSY-",
                            "\r/BUSY/",
                            "\r|BUSY|",
                            "\r\\BUSY\\",
                        };
                        while (false == mreii.WaitOne(33))
                        {
                            Console.Write(bb[i]);
                            i++;
                            if (i == bb.Length)
                            {
                                i = 0;
                            }
                        }
                        Console.Write("\r      \r");
                        var s = string.Empty;
                        while (false == mre.WaitOne(17))
                        {
                            lock (sb)
                            {
                                s = sb.ToString();
                                sb.Clear();
                            }
                            Console.Write(s);
                        }
                        lock (sb)
                        {
                            s = sb.ToString();
                            sb.Clear();
                        }
                        Console.Write(s);
                    })
                    { IsBackground = true };
                    tt.Start();
                    result = dbg.Execute(line, (m, t) =>
                    {
                        mreii.Set();
                        lock (sb)
                        {
                            sb.Append(t);
                        }
                        return 0;
                    });
                    mreii.Set();
                    mre.Set();
                    tt.Join();
                    Console.WriteLine(string.Format("{0}", result));
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
