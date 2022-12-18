using System;

namespace JsonDictionaryCore.Panels
{
    public class RefLinkClickEventArgs : EventArgs
    {
        public readonly string LinkText;

        public RefLinkClickEventArgs(string text)
        {
            LinkText = text;
        }
    }
}
