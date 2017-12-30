using mshtml;

namespace uSharpBrowser.Dom
{
    public class Script : BaseDomObject
    {
        public string Src { get; set; }

        public Script(HTMLScriptElement element) : base((IHTMLElement)element)
        {
            Src = element.src;
        }
    }
}
