using mshtml;

namespace uSharpBrowser.Dom
{
    public abstract class BaseDomObject
    {
        private IHTMLElement _element;
        protected IHTMLElement Element { get { return _element; } }

        public BaseDomObject(IHTMLElement element)
        {
            _element = element;
        }
    }
}
