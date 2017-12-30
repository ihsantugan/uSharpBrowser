using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace uSharpBrowser.Utilities
{
    public static class DispatchUtility
    {
        #region Public Methods

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static Type GetType(JqueryObject jObject, bool throwIfNotFound)
        {
            Guard.ArgumentNotNull(jObject, "jObject");
            return GetType(jObject.Dispatch, throwIfNotFound);
        }

        /// <summary>
        /// Tries to get the DISPID for the requested member name.
        /// </summary>
        /// <param name="jObject">An object that implements IDispatch.</param>
        /// <param name="name">The name of a member to lookup.</param>
        /// <param name="dispId">If the method returns true, this holds the DISPID on output.
        /// If the method returns false, this value should be ignored.</param>
        /// <returns>True if the member was found and resolved to a DISPID.  False otherwise.</returns>
        /// <exception cref="InvalidCastException">If <paramref name="jObject"/> doesn't implement IDispatch.</exception>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static bool TryGetDispId(JqueryObject jObject, string memberName, out int dispId)
        {
            Guard.ArgumentNotNull(jObject, "jObject");
            Guard.ArgumentNotNullOrEmpty(memberName, "memberName");
            if (string.IsNullOrEmpty(memberName)) { throw new ArgumentNullException(memberName); }

            return TryGetDispId(jObject.Dispatch, memberName, out dispId);
        }

        /// <summary>
        /// Invokes a member by DISPID.
        /// </summary>
        /// <param name="jObject">An object that implements IDispatch.</param>
        /// <param name="dispId">The DISPID of a member.  This can be obtained using
        /// <see cref="TryGetDispId(object, string, out int)"/>.</param>
        /// <param name="args">The arguments to pass to the member.</param>
        /// <returns>The member's return value.</returns>
        /// <remarks>This can invoke a method or a property get accessor.</remarks>
        public static object Invoke(JqueryObject jObject, int dispId, object[] args = null)
        {
            Guard.ArgumentNotNull(jObject, "jObject");

            string memberName = "[DispId=" + dispId + "]";
            return Invoke(jObject, memberName, args);
        }

        /// <summary>
        /// Invokes a member by name.
        /// </summary>
        /// <param name="obj">An object.</param>
        /// <param name="memberName">The name of the member to invoke.</param>
        /// <param name="args">The arguments to pass to the member.</param>
        /// <returns>The member's return value.</returns>
        /// <remarks>
        /// This can invoke a method or a property get accessor.
        /// </remarks>
        public static object Invoke(JqueryObject jObject, string memberName, object[] args = null)
        {
            Guard.ArgumentNotNull(jObject, "jObject");
            Guard.ArgumentNotNullOrEmpty(memberName, "memberName");

            Type type = jObject.Dispatch.GetType();
            return type.InvokeMember(memberName, BindingFlags.InvokeMethod, null, jObject.Dispatch, args, null);
        }

        #endregion

        #region Private Methods



        private static Type GetType(IDispatch dispatch, bool throwIfNotFound)
        {
            Guard.ArgumentNotNull(dispatch, "dispatch");

            Type result = null;
            int typeInfoCount;
            HRESULT hr = dispatch.GetTypeInfoCount(out typeInfoCount);
            if (hr == HRESULT.S_OK && typeInfoCount > 0)
            {
                dispatch.GetTypeInfo(0, DispatchConstants.LOCALE_SYSTEM_DEFAULT, out result);
            }

            if (result == null && throwIfNotFound)
            {
                Marshal.ThrowExceptionForHR((int)hr);
                throw new TypeLoadException();
            }

            return result;
        }

        /// <summary>
        /// Tries to get the DISPID for the requested member name.
        /// </summary>
        /// <param name="dispatch">An object that implements IDispatch.</param>
        /// <param name="name">The name of a member to lookup.</param>
        /// <param name="dispId">If the method returns true, this holds the DISPID on output.
        /// If the method returns false, this value should be ignored.</param>
        /// <returns>True if the member was found and resolved to a DISPID.  False otherwise.</returns>
        private static bool TryGetDispId(IDispatch dispatch, string memberName, out int dispId)
        {
            Guard.ArgumentNotNull(dispatch, "dispatch");
            Guard.ArgumentNotNullOrEmpty(memberName, "memberName");

            bool result = false;
            Guid iidNull = Guid.Empty;
            HRESULT hr = dispatch.GetDispId(ref iidNull, ref memberName, 1, DispatchConstants.LOCALE_SYSTEM_DEFAULT, out dispId);

            if (hr == HRESULT.S_OK)
            {
                result = true;
            }
            else if (hr == HRESULT.DISP_E_UNKNOWNNAME && dispId == DISPIDConstants.DISPID_UNKNOWN)
            {
                result = false;
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)hr);
            }
            return result;
        }

        #endregion
    }
}
