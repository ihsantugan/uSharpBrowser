using mshtml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using uSharpBrowser.Dom;

namespace uSharpBrowser
{
    public class SharpBrowser : WebBrowser
    {
        private HtmlElement _jqueryScriptElement, head;
        private bool _loadingCompleted;
        private string jqueryText;

        private List<Script> _scripts = new List<Script>();
        public ReadOnlyCollection<Script> Scripts { get { return _scripts.AsReadOnly(); } }

        public SharpBrowser()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("uSharpBrowser.jquery-1.8.min.js"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    jqueryText = reader.ReadToEnd();
                }
            }
        }

        protected override void OnNavigating(WebBrowserNavigatingEventArgs e)
        {
            base.OnNavigating(e);
            _scripts.Clear();
            head = _jqueryScriptElement = null;
        }

        protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
        {
            if (Document != null && ReadyState == WebBrowserReadyState.Complete)
            {
                _scripts.Clear();
                IHTMLDocument2 doc = (IHTMLDocument2)Document.DomDocument;

                IHTMLElementCollection scripts = doc.scripts;
                foreach (HTMLScriptElement script in scripts)
                {
                    _scripts.Add(new Script(script));
                }

                head = Document.GetElementsByTagName("head")[0];
                RegisterJsFunction();

                Graphics graph;
                IntPtr hdc;
                IHTMLElementRenderFixed render;
                Bitmap image;
                foreach (IHTMLImgElement img in doc.images)
                {
                    render = (IHTMLElementRenderFixed)img;
                    image = new Bitmap(img.width, img.height);
                    graph = Graphics.FromImage(image);
                    hdc = graph.GetHdc();
                    render.DrawToDC(hdc);
                    graph.ReleaseHdc(hdc);
                }
                _loadingCompleted = true;
                base.OnDocumentCompleted(e);
            }
        }

        public JqueryObject GetElementWithJquery(string query)
        {
            WaitUntilDocumentLoad();

            if (_jqueryScriptElement == null)
            {
                RegisterJsFunction();
            }

            ((IHTMLScriptElement)_jqueryScriptElement.DomElement).text = "function uWebBrowser() { return $('" + query + "'); }";
            IDispatch dispatch = Document.InvokeScript("uWebBrowser") as IDispatch;

            //Guid dummy = Guid.Empty;
            //string name = "next";
            //dispatch.GetDispId(ref dummy, ref name, 1, 0, out int dispId);
            //string[] rgsNames = new string[1];
            //rgsNames[0] = "next";
            //int[] rgDispId = new int[1] { 0 };
            //dispatch.GetIDsOfNames(ref dummy, new string[1] { name }, 1, DispatchConstants.LOCALE_SYSTEM_DEFAULT, rgDispId);

            return new JqueryObject(dispatch);
        }

        public void SaveAsHtml(string path)
        {
            StreamWriter str = null;
            try
            {
                HTMLDocument document = (HTMLDocument)Document.DomDocument;
                str = new StreamWriter(path);
                str.Write(document.documentElement.innerHTML);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (str != null)
                {
                    str.Close();
                }
            }
        }

        private void RegisterJsFunction()
        {
            _jqueryScriptElement = Document.CreateElement("script");
            if (_jqueryScriptElement != null)
            {
                _jqueryScriptElement.SetAttribute("type", "text/javascript");
                ((IHTMLScriptElement)_jqueryScriptElement.DomElement).text = jqueryText;
                head.AppendChild(_jqueryScriptElement);
            }
            else
            {
                throw new Exception("Script registration failed");
            }
        }

        private void WaitUntilDocumentLoad()
        {
            while (true)
            {
                if (ReadyState == WebBrowserReadyState.Complete &&
                    _loadingCompleted)
                {
                    break;
                }
                Application.DoEvents();
            }
        }
    }
}
