# WindbgEx
[WindbgExcuseme] Play dump with Windbg in C# now!

Copy dlls from windbg folder or just put the compiled binary inside windbg folder. And run
````
windbgex [dump_file_path]
````

sample console
````
Microsoft Windows [Version 10.0.18363.959]
(c) 2019 Microsoft Corporation. All rights reserved.

C:\dump>windbgex windbg.DMP

C:\dump>"C:\Program Files\Windows Kits\10\Debuggers\x64\WindbgEx.exe" windbg.DMP
Loading windbg.DMP...> k
Child-SP          RetAddr           Call Site
000000f5`9807da28 00007ff7`78342abf win32u!NtUserWaitMessage+0x14
000000f5`9807da30 00007ff7`7834a7cd windbg!wmain+0x533
000000f5`9807fad0 00007ff9`74ec7bd4 windbg!__wmainCRTStartup+0x14d
000000f5`9807fb10 00007ff9`76e0ce51 kernel32!BaseThreadInitThunk+0x14
000000f5`9807fb40 00000000`00000000 ntdll!RtlUserThreadStart+0x21

> ~
.  0  Id: 112c.5558 Suspend: 0 Teb: 000000f5`97f46000 Unfrozen
   1  Id: 112c.7668 Suspend: 0 Teb: 000000f5`97f54000 Unfrozen
   2  Id: 112c.5168 Suspend: 0 Teb: 000000f5`97f56000 Unfrozen
   3  Id: 112c.4d54 Suspend: 0 Teb: 000000f5`97f58000 Unfrozen
   4  Id: 112c.260c Suspend: 0 Teb: 000000f5`97f5a000 Unfrozen
   5  Id: 112c.71d0 Suspend: 0 Teb: 000000f5`97f5c000 Unfrozen
   6  Id: 112c.7620 Suspend: 0 Teb: 000000f5`97f5e000 Unfrozen
   7  Id: 112c.3c44 Suspend: 0 Teb: 000000f5`97f60000 Unfrozen
   8  Id: 112c.6d18 Suspend: 0 Teb: 000000f5`97f62000 Unfrozen
   9  Id: 112c.6670 Suspend: 0 Teb: 000000f5`97f64000 Unfrozen
  10  Id: 112c.71b8 Suspend: 0 Teb: 000000f5`97f66000 Unfrozen
  11  Id: 112c.4194 Suspend: 0 Teb: 000000f5`97f68000 Unfrozen

> !peb
PEB at 000000f597f45000
    InheritedAddressSpace:    No
    ReadImageFileExecOptions: No
    BeingDebugged:            No
    ImageBaseAddress:         00007ff778300000
    NtGlobalFlag:             0
    NtGlobalFlag2:            0
    Ldr                       00007ff976f053c0
    Ldr.Initialized:          Yes
    Ldr.InInitializationOrderModuleList: 0000027518422ed0 . 0000027d26feedf0
    Ldr.InLoadOrderModuleList:           0000027518423040 . 0000027d26feedd0
    Ldr.InMemoryOrderModuleList:         0000027518423050 . 0000027d26feede0
                    Base TimeStamp                     Module
            7ff778300000 4fed8795 Jun 29 18:46:45 2012 C:\Program Files\Windows Kits\10\Debuggers\x64\windbg.exe
            7ff976da0000 b29ecf52 Dec 17 23:10:10 2064 C:\WINDOWS\SYSTEM32\ntdll.dll
            7ff974eb0000 ce6bbd73 Sep 29 06:55:47 2079 C:\WINDOWS\System32\KERNEL32.DLL
            7ff9740c0000 7b90c1b5 Sep 11 08:45:41 2035 C:\WINDOWS\System32\KERNELBASE.dll
            7ff975490000 502aaa11 Aug 15 03:42:09 2012 C:\WINDOWS\System32\ADVAPI32.dll
            7ff9767c0000 f5bdefd7 Aug 25 16:27:03 2100 C:\WINDOWS\System32\msvcrt.dll
            7ff976cb0000 f222d51c Sep 24 11:50:52 2098 C:\WINDOWS\System32\sechost.dll
            7ff976860000 20fafd3a Jul 15 11:55:38 1987 C:\WINDOWS\System32\RPCRT4.dll
            7ff975d40000 90b22122 Dec 05 11:23:14 2046 C:\WINDOWS\System32\GDI32.dll
            7ff973d20000 5343f4fb Apr 08 21:09:15 2014 C:\WINDOWS\System32\win32u.dll
            7ff974cb0000 42d46341 Jul 13 08:41:37 2005 C:\WINDOWS\System32\gdi32full.dll
            7ff973fa0000 2085286c Apr 17 02:52:28 1987 C:\WINDOWS\System32\msvcp_win.dll
            7ff973ea0000 32a6df9a Dec 05 22:43:38 1996 C:\WINDOWS\System32\ucrtbase.dll
            7ff976980000 ee4ef0d0 Sep 11 05:27:44 2096 C:\WINDOWS\System32\USER32.dll
            7ff976b20000 00331c0f Feb 09 02:25:19 1970 C:\WINDOWS\System32\ole32.dll
            7ff975540000 90957831 Nov 13 17:38:57 2046 C:\WINDOWS\System32\combase.dll
            7ff974040000 bf9ade76 Nov 13 07:49:10 2071 C:\WINDOWS\System32\bcryptPrimitives.dll
            7ff9760d0000 046e5800 May 10 18:05:52 1972 C:\WINDOWS\System32\SHELL32.dll
            7ff9743d0000 afaaabaa May 24 10:04:26 2063 C:\WINDOWS\System32\cfgmgr32.dll
            7ff974f70000 bd8b0e7d Oct 08 23:17:17 2070 C:\WINDOWS\System32\shcore.dll
            7ff9648f0000 4e57c836 Aug 27 00:22:14 2011 C:\Program Files\Windows Kits\10\Debuggers\x64\dbghelp.dll
            7ff964ae0000 fb4dabf0 Aug 10 14:45:04 2103 C:\Program Files\Windows Kits\10\Debuggers\x64\dbgeng.dll
            7ff974450000 b71508e8 May 03 08:44:24 2067 C:\WINDOWS\System32\windows.storage.dll
            7ff974420000 b59ada57 Jul 20 12:08:55 2066 C:\WINDOWS\System32\bcrypt.dll
            7ff973ca0000 ca538c5d Jul 26 05:10:53 2077 C:\WINDOWS\System32\profapi.dll
            7ff975df0000 6ac977e7 Oct 10 07:25:27 2026 C:\WINDOWS\System32\OLEAUT32.dll
            7ff973cd0000 fdc4588a Nov 30 23:49:30 2104 C:\WINDOWS\System32\powrprof.dll
            7ff973c50000 a2ccd413 Jul 20 23:30:27 2056 C:\WINDOWS\System32\UMPDC.dll
            7ff974e50000 f8807ba1 Feb 12 14:43:45 2102 C:\WINDOWS\System32\shlwapi.dll
            7ff965780000 74245cb1 Sep 30 21:08:01 2031 C:\Program Files\Windows Kits\10\Debuggers\x64\dbgmodel.dll
            7ff973c80000 05bef372 Jan 21 01:50:42 1973 C:\WINDOWS\System32\kernel.appcore.dll
            7ff974be0000 a51023f1 Oct 03 09:33:37 2057 C:\WINDOWS\System32\cryptsp.dll
            7ff96cbb0000 2449c853 Apr 17 19:19:47 1989 C:\WINDOWS\SYSTEM32\XmlLite.dll
            7ff975c70000 e92039b7 Dec 09 21:20:55 2093 C:\WINDOWS\System32\COMDLG32.dll
            7ff976d50000 3ebc2f6d May 10 06:45:01 2003 C:\WINDOWS\System32\PSAPI.DLL
......
````
have fun!
