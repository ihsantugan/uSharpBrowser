using System;
using System.Collections.Generic;
using System.Reflection;

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
            //Type type = DispatchUtility.GetType(jObject, false);
            //var a = _type.GetProperties();
            //var b = _type.GetMethods();
            //var c = _type.GetMembers();
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
            IDispatch a = InvokeMember<IDispatch>("nextAll");
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
