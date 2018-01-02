using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using uSharpBrowser.Utilities;

namespace uSharpBrowser
{
    public class JqueryObject
    {
        private IDispatch _dispatch;
        private Type _type;

        internal IDispatch Dispatch { get { return _dispatch; } }

        public JqueryObject(IDispatch dispatch)
        {
            _dispatch = dispatch;
            _type = dispatch.GetType();
            ITypeInfo type = DispatchUtility.GetType(_dispatch);

            //Type t = type.GetType();
            //Type typaae = Type.GetTypeFromProgID("JScriptTypeLib.JScriptTypeInfo", true);

            //PropertyInfo[] a = type.GetProperties();
            //MethodInfo[] b = type.GetMethods();
            //MemberInfo[] c = type.GetMembers();
            //EventInfo[] e = type.GetEvents();
        }

        public JqueryObject Next()
        {
            IDispatch jObj = InvokeMember<IDispatch>("next");
            if (jObj == null)
            {
                return null;
            }
            return new JqueryObject(jObj);
        }

        public List<JqueryObject> NextAll()
        {
            IDispatch dispatch = InvokeMember<IDispatch>("nextAll");

            string name = "length";
            int dispId;
            Guid riid = Guid.Empty;
            HRESULT res = dispatch.GetDispId(ref riid, name, 1, 0, out dispId);

            int length = (int)dispatch.GetType().InvokeMember("length", BindingFlags.GetProperty, null, dispatch, null);
            int length2 = (int)dispatch.GetType().InvokeMember("length", BindingFlags.GetProperty, null, dispatch, null);
            for (int index = 0; index < length; index++)
            {
                IDispatch obj = dispatch.GetType().InvokeMember(index.ToString(), BindingFlags.GetProperty, null, dispatch, null) as IDispatch;
            }

            return null;
        }

        public JqueryObject Prev()
        {
            IDispatch jObj = InvokeMember<IDispatch>("prev");
            if (jObj == null)
            {
                return null;
            }
            return new JqueryObject(jObj);
        }

        public int Length()
        {
            return InvokeMember<int>("length");
        }

        public string Html()
        {
            return InvokeMember<string>("html").ToString();
        }

        public JqueryObject Closest(string tag)
        {
            return new JqueryObject(InvokeMember<IDispatch>("closest", new object[] { tag }));
        }

        public override string ToString()
        {
            return InvokeMember<string>("text");
        }

        private T InvokeMember<T>(string name, object[] args = null)
        {
            try
            {
                return (T)_type.InvokeMember(name, BindingFlags.InvokeMethod, null, _dispatch, args);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
