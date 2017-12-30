
namespace uSharpBrowser
{
    public class DispatchConstants
    {
        public const int LOCALE_SYSTEM_DEFAULT = 0x800; //From WinNT.h == 2048 == 2 << 10
    }

    /// <summary></summary>
    /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms221242(v=vs.85).aspx</remarks>
    public class DISPIDConstants
    {
        public const int DISPID_COLLECT = -8;
        public const int DISPID_DESTRUCTOR = -7;
        public const int DISPID_CONSTRUCTOR = -6;
        public const int DISPID_EVALUATE = -5;
        public const int DISPID_NEWENUM = -4;
        public const int DISPID_PROPERTYPUT = -3;
        public const int DISPID_UNKNOWN = -1;
        public const int DISPID_VALUE = 0;
    }

    public class HResults //From WinError.h
    {
        public const uint S_OK = 0x00000000;
        public const uint E_ABORT = 0x80004004;
        public const uint E_ACCESSDENIED = 0x80070005;
        public const uint E_FAIL = 0x80004005;
        public const uint E_HANDLE = 0x80070006;
        public const uint E_INVALIDARG = 0x80070057;
        public const uint E_NOINTERFACE = 0x80004002;
        public const uint E_NOTIMPL = 0x80004001;
        public const uint E_OUTOFMEMORY = 0x8007000E;
        public const uint E_POINTER = 0x80004003;
        public const uint E_UNEXPECTED = 0x8000FFFF;

        public const uint DISP_E_BADPARAMCOUNT = 0x8002000E;
        public const uint DISP_E_BADVARTYPE = 0x80020008;
        public const uint DISP_E_EXCEPTION = 0x80020009;
        public const uint DISP_E_MEMBERNOTFOUND = 0x80020003;
        public const uint DISP_E_NONAMEDARGS = 0x80020007;
        public const uint DISP_E_OVERFLOW = 0x8002000A;
        public const uint DISP_E_PARAMNOTFOUND = 0x80020004;
        public const uint DISP_E_TYPEMISMATCH = 0x80020005;
        public const uint DISP_E_UNKNOWNINTERFACE = 0x80020001;
        public const uint DISP_E_UNKNOWNLCID = 0x8002000C;
        public const uint DISP_E_PARAMNOTOPTIONAL = 0x8002000F;
        public const uint DISP_E_UNKNOWNNAME = 0x80020006;
        public const uint DISP_E_BADINDEX = 0x8002000B;
    }

    public enum HRESULT : uint //From WinError.h
    {
        S_OK = 0x00000000,
        E_ABORT = 0x80004004,
        E_ACCESSDENIED = 0x80070005,
        E_FAIL = 0x80004005,
        E_HANDLE = 0x80070006,
        E_INVALIDARG = 0x80070057,
        E_NOINTERFACE = 0x80004002,
        E_NOTIMPL = 0x80004001,
        E_OUTOFMEMORY = 0x8007000E,
        E_POINTER = 0x80004003,
        E_UNEXPECTED = 0x8000FFFF,

        DISP_E_BADPARAMCOUNT = 0x8002000E,
        DISP_E_BADVARTYPE = 0x80020008,
        DISP_E_EXCEPTION = 0x80020009,
        DISP_E_MEMBERNOTFOUND = 0x80020003,
        DISP_E_NONAMEDARGS = 0x80020007,
        DISP_E_OVERFLOW = 0x8002000A,
        DISP_E_PARAMNOTFOUND = 0x80020004,
        DISP_E_TYPEMISMATCH = 0x80020005,
        DISP_E_UNKNOWNINTERFACE = 0x80020001,
        DISP_E_UNKNOWNLCID = 0x8002000C,
        DISP_E_PARAMNOTOPTIONAL = 0x8002000F,
        DISP_E_UNKNOWNNAME = 0x80020006,
        DISP_E_BADINDEX = 0x8002000B,
    }
}
