// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace JsonDictionaryCore
{
    public partial class Form1
    {
        internal class ContentTypeItem
        {
            public string FileTypeMask; // file mask
            public string PropertyTypeName; // name of the array contains a group of certain definitions
            public JsoncContentType FileType; // file type enum

            public ContentTypeItem()
            {
                FileTypeMask = "";
                PropertyTypeName = "";
                FileType = JsoncContentType.Unknown;
            }
        }
    }
}
