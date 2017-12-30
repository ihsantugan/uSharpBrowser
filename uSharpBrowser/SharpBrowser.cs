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
        private HtmlElement _scriptElement;
        private bool _loadingCompleted;
        private string jqueryText;

        private List<Script> _Scripts = new List<Script>();
        public ReadOnlyCollection<Script> Scripts { get { return _Scripts.AsReadOnly(); } }

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
            _scriptElement = null;
        }

        protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
        {
            if (Document != null && ReadyState == WebBrowserReadyState.Complete)
            {
                _Scripts.Clear();
                IHTMLDocument2 doc = (IHTMLDocument2)Document.DomDocument;

                IHTMLElementCollection scripts = doc.scripts;
                foreach (HTMLScriptElement script in scripts)
                {
                    _Scripts.Add(new Script(script));
                }

                HtmlElement head = Document.GetElementsByTagName("head")[0];
                _scriptElement = Document.CreateElement("script");
                if (_scriptElement != null)
                {
                    _scriptElement.SetAttribute("type", "text/javascript");
                    ((IHTMLScriptElement)_scriptElement.DomElement).text = jqueryText;
                    head.AppendChild(_scriptElement);
                }
                else
                {
                    throw new Exception("Script registration failed");
                }

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

            if (_scriptElement == null)
            {
                RegisterJsFunction();
            }

            ((IHTMLScriptElement)_scriptElement.DomElement).text = "function uWebBrowser() { return $('.headerRow'); }";
            IDispatch dispatch = Document.InvokeScript("uWebBrowser") as IDispatch;

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
            if (Document != null)
            {
                HtmlElement head = Document.GetElementsByTagName("head")[0];
                _scriptElement = Document.CreateElement("script");
                if (_scriptElement != null)
                {
                    IHTMLScriptElement element = (IHTMLScriptElement)_scriptElement.DomElement;
                    head.AppendChild(_scriptElement);
                }
                else
                {
                    throw new Exception("Script registration failed");
                }
            }
        }

        private void WaitUntilDocumentLoad()
        {
            if (!_loadingCompleted)
            {
                while (true)
                {
                    if (ReadyState == WebBrowserReadyState.Complete)
                    {
                        break;
                    }
                    Application.DoEvents();
                }
            }
        }
    }
}
