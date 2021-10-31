using System;

namespace JsonDictionaryCore
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
