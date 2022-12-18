// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace JsonDictionaryCore.Panels
{
    public class ContentTypeItem
    {
        public string FileTypeSign; // file ending or directory name. Rewrite to use wildcards
        public string PropertyTypeName; // name of the array contains a group of certain definitions
        public string ContentType; // file type name to use everywhere

        public ContentTypeItem()
        {
            FileTypeSign = "";
            PropertyTypeName = "";
            ContentType = "";
        }
    }
}
