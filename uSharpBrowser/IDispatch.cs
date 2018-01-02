using System;
using System.Runtime.InteropServices;

namespace uSharpBrowser
{
    /// <summary>
    /// A partial declaration of IDispatch used to lookup Type information and DISPIDs.
    /// </summary>
    /// <remarks>
    /// This interface only declares the first three methods of IDispatch.  It omits the
    /// fourth method (Invoke) because there are already plenty of ways to do dynamic
    /// invocation in .NET.  But the first three methods provide dynamic type metadata
    /// discovery, which .NET doesn't provide normally if you have a System.__ComObject
    /// RCW instead of a strongly-typed RCW.
    /// <para/>
    /// Note: The original declaration of IDispatch is in OAIdl.idl.
    /// </remarks>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00020400-0000-0000-C000-000000000046")]
    public interface IDispatch
    {
        /// <summary>
        /// Gets the number of Types that the object provides (0 or 1).
        /// </summary>
        /// <param name="typeInfoCount">Returns 0 or 1 for the number of Types provided by <see cref="GetTypeInfo"/>.</param>
        /// <returns>S_OK | E_NOTIMPL</returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/da876d53-cb8a-465c-a43e-c0eb272e2a12(VS.85)</remarks>
        [PreserveSig]
        HRESULT GetTypeInfoCount(out int typeInfoCount);


        /// <summary>
        /// Gets the Type information for an object if <see cref="GetTypeInfoCount"/> returned 1.
        /// </summary>
        /// <param name="typeInfoIndex">Must be 0.</param>
        /// <param name="lcid">Typically, LOCALE_SYSTEM_DEFAULT (2048).</param>
        /// <param name="typeInfo">Returns the object's Type information.</param>
        /// <returns>S_OK | DISP_E_BADINDEX</returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/cc1ec9aa-6c40-4e70-819c-a7c6dd6b8c99(VS.85)</remarks>
        [PreserveSig]
        int GetTypeInfo(
            [MarshalAs(UnmanagedType.U4)] int iTInfo,
            [MarshalAs(UnmanagedType.U4)] int lcid,
            out System.Runtime.InteropServices.ComTypes.ITypeInfo typeInfo);
        //[PreserveSig]
        //void GetTypeInfo(int typeInfoIndex, int lcid,
        //    [MarshalAs(UnmanagedType.CustomMarshaler,
        //    MarshalTypeRef = typeof(System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler))] out Type typeInfo);

        //[return: MarshalAs(UnmanagedType.Interface)]
        //ITypeInfo GetTypeInfo([In, MarshalAs(UnmanagedType.U4)] int iTInfo, [In, MarshalAs(UnmanagedType.U4)] int lcid);

        /// <summary>
        /// Gets the DISPID of the specified member name.
        /// </summary>
        /// <param name="riid">Must be IID_NULL.  Pass a copy of Guid.Empty.</param>
        /// <param name="name">The name of the member to look up.</param>
        /// <param name="nameCount">Must be 1.</param>
        /// <param name="lcid">Typically, LOCALE_SYSTEM_DEFAULT (2048).</param>
        /// <param name="dispId">If a member with the requested <paramref name="name"/>
        /// is found, this returns its DISPID and the method's return value is 0.
        /// If the method returns a non-zero value, then this parameter's output value is
        /// undefined.</param>
        /// <returns>Zero for success. Non-zero for failure.</returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/6f6cf233-3481-436e-8d6a-51f93bf91619(VS.85)</remarks>
        [PreserveSig]
        HRESULT GetDispId(ref Guid riid, ref string name, int nameCount, int lcid, out int dispId);

        /// <summary>
        /// Maps a single member and an optional set of argument names to a corresponding set of integer DISPIDs, which can be used on subsequent calls to Invoke
        /// </summary>
        /// <param name="riid">Reserved for future use. Must be IID_NULL</param>
        /// <param name="rgszNames">The array of names to be mapped</param>
        /// <param name="cNames">The count of the names to be mapped</param>
        /// <param name="lcid">The locale context in which to interpret the names</param>
        /// <param name="rgDispId">Caller-allocated array, each element of which contains an identifier (ID) corresponding to one of the names passed in the rgszNames array. The first element represents the member name. The subsequent elements represent each of the member's parameters.</param>
        /// <returns>S_OK | E_OUTOFMEMORY | DISP_E_UNKNOWNNAME | DISP_E_UNKNOWNLCID</returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms221306(v=vs.85).aspx</remarks>
        [PreserveSig]
        HRESULT GetIDsOfNames(
            [In] ref Guid riid,
            [In, MarshalAs(UnmanagedType.LPArray)] string[] rgszNames,
            [In, MarshalAs(UnmanagedType.U4)] int cNames,
            [In, MarshalAs(UnmanagedType.U4)] int lcid,
            [Out, MarshalAs(UnmanagedType.LPArray)] int[] rgDispId);

        /// <summary>
        /// Provides access to properties and methods exposed by an object. 
        /// </summary>
        /// <param name="dispIdMember">Identifies the member. Use GetIDsOfNames or the object's documentation to obtain the dispatch identifier.</param>
        /// <param name="riid">Reserved for future use. Must be IID_NULL</param>
        /// <param name="lcid">The locale context in which to interpret arguments. The lcid is used by the GetIDsOfNames function, and is also passed to Invoke to allow the object to interpret its arguments specific to a locale.</param>
        /// <param name="wFlags">Flags describing the context of the Invoke call.</param>
        /// <param name="pDispParams">Pointer to a DISPPARAMS structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
        /// <param name="pVarResult">Pointer to the location where the result is to be stored, or NULL if the caller expects no result. This argument is ignored if DISPATCH_PROPERTYPUT or DISPATCH_PROPERTYPUTREF is specified.</param>
        /// <param name="pExcepInfo">Pointer to a structure that contains exception information. This structure should be filled in if DISP_E_EXCEPTION is returned. Can be NULL.</param>
        /// <param name="pArgErr">The index within rgvarg of the first argument that has an error. Arguments are stored in pDispParams->rgvarg in reverse order, so the first argument is the one with the highest index in the array. This parameter is returned only when the resulting return value is DISP_E_TYPEMISMATCH or DISP_E_PARAMNOTFOUND. This argument can be set to null</param>
        /// <returns>S_OK | E_OUTOFMEMORY | DISP_E_UNKNOWNNAME | DISP_E_UNKNOWNLCID</returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms221479(v=vs.85).aspx</remarks>
        [PreserveSig]
        HRESULT Invoke(int dispIdMember,
            ref Guid riid,
            uint lcid,
            ushort wFlags,
            ref System.Runtime.InteropServices.ComTypes.DISPPARAMS pDispParams,
            out object pVarResult,
            ref System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo,
            out UInt32 pArgErr);
    }
}
