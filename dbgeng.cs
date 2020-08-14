using System;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable InterpolatedStringExpressionIsNotIFormattable

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

namespace WindbgEx
{
    #region enum

    [Flags]
    public enum DEBUG_OUTCTL : uint
    {
        THIS_CLIENT = 0,
        ALL_CLIENTS = 1,
        ALL_OTHER_CLIENTS = 2,
        IGNORE = 3,
        LOG_ONLY = 4,
        SEND_MASK = 7,
        NOT_LOGGED = 8,
        OVERRIDE_MASK = 0x10,
        DML = 0x20,
        AMBIENT_DML = 0xfffffffe,
        AMBIENT_TEXT = 0xffffffff
    }

    [Flags]
    public enum DEBUG_EXECUTE : uint
    {
        DEFAULT = 0,
        ECHO = 1,
        NOT_LOGGED = 2,
        NO_REPEAT = 4
    }

    [Flags]
    public enum DEBUG_OUTPUT : uint
    {
        NORMAL = 1,
        ERROR = 2,
        WARNING = 4,
        VERBOSE = 8,
        PROMPT = 0x10,
        PROMPT_REGISTERS = 0x20,
        EXTENSION_WARNING = 0x40,
        DEBUGGEE = 0x80,
        DEBUGGEE_PROMPT = 0x100,
        SYMBOLS = 0x200
    }

    [Flags]
    public enum DEBUG_WAIT : uint
    {
        DEFAULT = 0
    }

    public struct HResult
    {
        public const int S_OK = 0;
        public const int S_FALSE = 1;
        public const int E_FAIL = unchecked((int) 0x80004005);

        public const int E_INVALIDARG = unchecked((int) 0x80070057);
        public const int E_NOTIMPL = unchecked((int) 0x80004001);
        public const int E_NOINTERFACE = unchecked((int) 0x80004002);

        public bool IsOK => Value == S_OK;

        public int Value { get; set; }

        public HResult(int hr)
        {
            Value = hr;
        }

        public static implicit operator HResult(int hr)
        {
            return new HResult(hr);
        }

        public static implicit operator int(HResult hr)
        {
            return hr.Value;
        }
        
        public static implicit operator bool(HResult hr)
        {
            return hr.Value >= 0;
        }

        public override string ToString()
        {
            switch (Value)
            {
                case S_OK: return "S_OK";
                case S_FALSE: return "S_FALSE";
                case E_FAIL: return "E_FAIL";
                case E_INVALIDARG: return "E_INVALIDARG";
                case E_NOTIMPL: return "E_NOTIMPL";
                case E_NOINTERFACE: return "E_NOINTERFACE";
                default: return $"{Value:x8}";
            }
        }
    }

    #endregion

    #region comimport

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("4bf58045-d654-4c40-b0af-683090f356dc")]
    public interface IDebugOutputCallbacks
    {
        [PreserveSig]
        int Output(
            DEBUG_OUTPUT Mask,
            [In] [MarshalAs(UnmanagedType.LPStr)] string Text);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("27fe5639-8407-4f47-8364-ee118fb08ac8")]
    public interface IDebugClient
    {
        /* IDebugClient */

        [PreserveSig]
        int AttachKernel();

        [PreserveSig]
        int GetKernelConnectionOptions();

        [PreserveSig]
        int SetKernelConnectionOptions();

        [PreserveSig]
        int StartProcessServer();

        [PreserveSig]
        int ConnectProcessServer();

        [PreserveSig]
        int DisconnectProcessServer();

        [PreserveSig]
        int GetRunningProcessSystemIds();

        [PreserveSig]
        int GetRunningProcessSystemIdByExecutableName();

        [PreserveSig]
        int GetRunningProcessDescription();

        [PreserveSig]
        int AttachProcess();

        [PreserveSig]
        int CreateProcess();

        [PreserveSig]
        int CreateProcessAndAttach();

        [PreserveSig]
        int GetProcessOptions();

        [PreserveSig]
        int AddProcessOptions();

        [PreserveSig]
        int RemoveProcessOptions();

        [PreserveSig]
        int SetProcessOptions();

        [PreserveSig]
        int OpenDumpFile(
            [In] [MarshalAs(UnmanagedType.LPStr)] string DumpFile);

        [PreserveSig]
        int WriteDumpFile();

        [PreserveSig]
        int ConnectSession();

        [PreserveSig]
        int StartServer();

        [PreserveSig]
        int OutputServer();

        [PreserveSig]
        int TerminateProcesses();

        [PreserveSig]
        int DetachProcesses();

        [PreserveSig]
        int EndSession();

        [PreserveSig]
        int GetExitCode(out uint Code);

        [PreserveSig]
        int DispatchCallbacks();

        [PreserveSig]
        int ExitDispatch();

        [PreserveSig]
        int CreateClient();

        [PreserveSig]
        int GetInputCallbacks();

        [PreserveSig]
        int SetInputCallbacks();

        /* GetOutputCallbacks could a conversion thunk from the debugger engine so we can't specify a specific interface */

        [PreserveSig]
        int GetOutputCallbacks(
            out IDebugOutputCallbacks Callbacks);

        /* We may have to pass a debugger engine conversion thunk back in so we can't specify a specific interface */

        [PreserveSig]
        int SetOutputCallbacks(
            [In] IDebugOutputCallbacks Callbacks);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("5182e668-105e-416e-ad92-24ef800424ba")]
    public interface IDebugControl
    {
        /* IDebugControl */

        [PreserveSig]
        int GetInterrupt();

        [PreserveSig]
        int SetInterrupt();

        [PreserveSig]
        int GetInterruptTimeout(
            out uint Seconds);

        [PreserveSig]
        int SetInterruptTimeout(
            uint Seconds);

        [PreserveSig]
        int GetLogFile();

        [PreserveSig]
        int OpenLogFile();

        [PreserveSig]
        int CloseLogFile();

        [PreserveSig]
        int GetLogMask();

        [PreserveSig]
        int SetLogMask();

        [PreserveSig]
        int Input();

        [PreserveSig]
        int ReturnInput();

        [PreserveSig]
        int Output();

        [PreserveSig]
        int OutputVaList();

        [PreserveSig]
        int ControlledOutput();

        [PreserveSig]
        int ControlledOutputVaList();

        [PreserveSig]
        int OutputPrompt();

        [PreserveSig]
        int OutputPromptVaList();

        [PreserveSig]
        int GetPromptText();

        [PreserveSig]
        int OutputCurrentState();

        [PreserveSig]
        int OutputVersionInformation();

        [PreserveSig]
        int GetNotifyEventHandle();

        [PreserveSig]
        int SetNotifyEventHandle();

        [PreserveSig]
        int Assemble();

        [PreserveSig]
        int Disassemble();

        [PreserveSig]
        int GetDisassembleEffectiveOffset();

        [PreserveSig]
        int OutputDisassembly();

        [PreserveSig]
        int OutputDisassemblyLines();

        [PreserveSig]
        int GetNearInstruction();

        [PreserveSig]
        int GetStackTrace();

        [PreserveSig]
        int GetReturnOffset();

        [PreserveSig]
        int OutputStackTrace();

        [PreserveSig]
        int GetDebuggeeType();

        [PreserveSig]
        int GetActualProcessorType();

        [PreserveSig]
        int GetExecutingProcessorType();

        [PreserveSig]
        int GetNumberPossibleExecutingProcessorTypes();

        [PreserveSig]
        int GetPossibleExecutingProcessorTypes();

        [PreserveSig]
        int GetNumberProcessors();

        [PreserveSig]
        int GetSystemVersion();

        [PreserveSig]
        int GetPageSize();

        [PreserveSig]
        int IsPointer64Bit();

        [PreserveSig]
        int ReadBugCheckData();

        [PreserveSig]
        int GetNumberSupportedProcessorTypes();

        [PreserveSig]
        int GetSupportedProcessorTypes();

        [PreserveSig]
        int GetProcessorTypeNames();

        [PreserveSig]
        int GetEffectiveProcessorType();

        [PreserveSig]
        int SetEffectiveProcessorType();

        [PreserveSig]
        int GetExecutionStatus();

        [PreserveSig]
        int SetExecutionStatus();

        [PreserveSig]
        int GetCodeLevel();

        [PreserveSig]
        int SetCodeLevel();

        [PreserveSig]
        int GetEngineOptions();

        [PreserveSig]
        int AddEngineOptions();

        [PreserveSig]
        int RemoveEngineOptions();

        [PreserveSig]
        int SetEngineOptions();

        [PreserveSig]
        int GetSystemErrorControl();

        [PreserveSig]
        int SetSystemErrorControl();

        [PreserveSig]
        int GetTextMacro();

        [PreserveSig]
        int SetTextMacro();

        [PreserveSig]
        int GetRadix();

        [PreserveSig]
        int SetRadix();

        [PreserveSig]
        int Evaluate();

        [PreserveSig]
        int CoerceValue();

        [PreserveSig]
        int CoerceValues();

        [PreserveSig]
        int Execute(
            DEBUG_OUTCTL OutputControl,
            [In] [MarshalAs(UnmanagedType.LPStr)] string Command,
            DEBUG_EXECUTE Flags);

        [PreserveSig]
        int ExecuteCommandFile();

        [PreserveSig]
        int GetNumberBreakpoints();

        [PreserveSig]
        int GetBreakpointByIndex();

        [PreserveSig]
        int GetBreakpointById();

        [PreserveSig]
        int GetBreakpointParameters();

        [PreserveSig]
        int AddBreakpoint();

        [PreserveSig]
        int RemoveBreakpoint();

        [PreserveSig]
        int AddExtension();

        [PreserveSig]
        int RemoveExtension();

        [PreserveSig]
        int GetExtensionByPath();

        [PreserveSig]
        int CallExtension();

        [PreserveSig]
        int GetExtensionFunction();

        [PreserveSig]
        int GetWindbgExtensionApis32();


        [PreserveSig]
        int GetWindbgExtensionApis64();

        [PreserveSig]
        int GetNumberEventFilters();

        [PreserveSig]
        int GetEventFilterText();

        [PreserveSig]
        int GetEventFilterCommand();

        [PreserveSig]
        int SetEventFilterCommand();

        [PreserveSig]
        int GetSpecificFilterParameters();

        [PreserveSig]
        int SetSpecificFilterParameters();

        [PreserveSig]
        int GetSpecificEventFilterArgument();

        [PreserveSig]
        int SetSpecificEventFilterArgument();

        [PreserveSig]
        int GetExceptionFilterParameters();

        [PreserveSig]
        int SetExceptionFilterParameters();

        [PreserveSig]
        int GetExceptionFilterSecondCommand();

        [PreserveSig]
        int SetExceptionFilterSecondCommand();

        [PreserveSig]
        int WaitForEvent(
            DEBUG_WAIT Flags,
            uint Timeout);
    }

    #endregion

    public delegate int DebugOutputCallback(DEBUG_OUTPUT Mask, string Text);

    public class DebugOutputCallbacks : IDebugOutputCallbacks
    {
        private readonly DebugOutputCallback callback;

        public DebugOutputCallbacks(DebugOutputCallback callback)
        {
            this.callback = callback;
        }

        public int Output(DEBUG_OUTPUT Mask, string Text)
        {
            return callback(Mask, Text);
        }
    }

    public class dbgeng : IDisposable
    {
        private readonly IDebugClient client;
        private readonly IDebugControl control;

        private readonly object executeLock = new object();

        private readonly IntPtr pDebugClient;

        public dbgeng()
        {
            var guid = new Guid("27fe5639-8407-4f47-8364-ee118fb08ac8");
            HResult hr = DebugCreate(guid, out pDebugClient);
            if (!hr.IsOK)
                throw new Exception($"Failed to create DebugClient, hr={hr:x}.");

            client = (IDebugClient) Marshal.GetTypedObjectForIUnknown(pDebugClient, typeof(IDebugClient));
            control = (IDebugControl) Marshal.GetTypedObjectForIUnknown(pDebugClient, typeof(IDebugControl));
        }

        public void Dispose()
        {
            Marshal.Release(pDebugClient);
        }

        [DllImport("dbgeng.dll")]
        public static extern int DebugCreate(Guid InterfaceId, out IntPtr Interface);

        public string OpenDumpFile(string DumpFile)
        {
            HResult hr;
            hr = client.OpenDumpFile(DumpFile);
            if (!hr.IsOK)
                return $"Failed to OpenDumpFile, hr={hr:x}.";

            hr = control.WaitForEvent(DEBUG_WAIT.DEFAULT, 60000);
            if (!hr.IsOK)
                return $"Failed to attach to dump file, hr={hr:x}.";
            return null;
        }

        public string Execute(string cmd, DebugOutputCallback callback = null)
        {
            HResult hr;
            IDebugOutputCallbacks origCallback;
            var sb = new StringBuilder();
            lock (executeLock)
            {
                hr = client.GetOutputCallbacks(out origCallback);
                if (!hr.IsOK)
                    origCallback = null;

                if (null == callback)
                    callback = (m, t) =>
                    {
                        sb.Append(t);
                        return 0;
                    };

                hr = client.SetOutputCallbacks(new DebugOutputCallbacks(callback));
                if (!hr.IsOK)
                {
                    sb.AppendLine($"SetOutputCallbacks failed. HRESULT={hr:x}.");
                }
                else
                {
                    hr = control.Execute(DEBUG_OUTCTL.THIS_CLIENT, cmd, DEBUG_EXECUTE.DEFAULT);
                    if (!hr.IsOK) sb.AppendLine($"Command encountered an error. HRESULT={hr:x}.");
                }

                if (origCallback != null)
                    client.SetOutputCallbacks(origCallback);
            }

            return sb.ToString();
        }
    }
}