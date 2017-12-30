using System;
using System.Runtime.InteropServices;

namespace uSharpBrowser
{
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("3050F669-98B5-11CF-BB82-00AA00BDCE0B")]
    internal interface IHTMLElementRenderFixed
    {
        void DrawToDC(IntPtr hdc);
        void SetDocumentPrinter(string bstrPrinterName, IntPtr hdc);
    }

    //[ComImport, InterfaceType((short)1), Guid("25336920-03F9-11CF-8FD0-00AA00686F13")]
    //internal interface HtmlDocumentRendered
    //{
    //}
}
