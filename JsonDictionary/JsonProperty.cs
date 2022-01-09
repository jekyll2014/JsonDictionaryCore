// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using JsonPathParserLib;

namespace JsonDictionaryCore
{
    [DataContract]
    public class JsonProperty
    {
        [DataMember] public char PathDelimiter = '.';
        [DataMember] public string FullFileName; // original path + file name
        [DataMember] public string Name; // property name
        [DataMember] public string Value; // property value
        [DataMember] public JsonValueType VariableType; // type of the variable (array, object, property, ...)
        [DataMember] public JsonPropertyType ObjectType; // if variable is a property/array/object
        [DataMember] public string ContentType; // file type (event, string, rules, ...)
        [DataMember] public string Version; // schema version declared in the beginning of the file

        [DataMember] private string _jsonPath;

        public string JsonPath // JSON path of the property
        {
            set
            {
                _jsonPath = value;
                FlattenedJsonPath = value;
                _jsonDepth = -1;
            }
            get => _jsonPath;
        }

        [DataMember] private string _parent; // parent name

        public string Parent // JSON path of the property
        {
            get
            {
                if (_parent == null)
                {
                    if (!string.IsNullOrEmpty(JsonPath) && JsonPath.Contains(PathDelimiter))
                    {
                        if (JsonPath.EndsWith("]"))
                        {
                            var startPos = JsonPath.LastIndexOf(PathDelimiter);
                            var endPos = JsonPath.LastIndexOf('[');
                            _parent = JsonPath.Substring(startPos + 1, endPos - startPos - 1);
                        }
                        else
                        {
                            var tokens = JsonPath.Split(PathDelimiter);
                            _parent = tokens[tokens.Length - 2];
                        }
                    }
                    else
                    {
                        _parent = "";
                    }
                }

                return _parent;
            }
        }

        [DataMember] private string _unifiedParent; // parent name

        public string UnifiedParent // JSON path of the property
        {
            get
            {
                if (_unifiedParent == null) _unifiedParent = UnifyPath(Parent, PathDelimiter);

                return _unifiedParent;
            }
        }

        [DataMember] private string _parentPath;

        public string ParentPath // parent object path
        {
            get
            {
                if (_parentPath == null)
                {
                    if (!string.IsNullOrEmpty(JsonPath) && JsonPath.Contains(PathDelimiter))
                        _parentPath = JsonPath.Substring(0, JsonPath.LastIndexOf(PathDelimiter));
                    else
                        _parentPath = "";
                }

                return _parentPath;
            }
        }

        [DataMember] public string FlattenedJsonPath; // JSON path of the property after  flattening

        [DataMember] private string _unifiedFlattenedJsonPath;

        public string UnifiedFlattenedJsonPath =>
            _unifiedFlattenedJsonPath ??=
                UnifyPath(FlattenedJsonPath, PathDelimiter); // json flattened path with no array [] brackets

        [DataMember] private int _jsonDepth = -1; // depth in the original JSON structure

        public int JsonDepth
        {
            get
            {
                if (_jsonDepth < 0) _jsonDepth = GetPathDepth(_jsonPath, PathDelimiter);

                return _jsonDepth;
            }
        }

        [DataMember] private string _unifiedPath;

        public string UnifiedPath =>
            _unifiedPath ??= UnifyPath(JsonPath, PathDelimiter); // json path with no array [] brackets

        public JsonProperty()
        {
            FullFileName = "";
            JsonPath = "";
            Name = "";
            Value = "";
            VariableType = JsonValueType.Unknown;
            ObjectType = JsonPropertyType.Unknown;
            ContentType = "";
            Version = "";
        }

        public static string UnifyPath(string path, char delimiter)
        {
            if (string.IsNullOrEmpty(path))
                return "";

            var unifiedPath = new StringBuilder();
            foreach (var token in path.Split(delimiter))
            {
                var pos = token.IndexOf('[');
                if (pos >= 0)
                    unifiedPath.Append(token.Substring(0, pos) + delimiter);
                else
                    unifiedPath.Append(token + delimiter);
            }

            return unifiedPath.ToString().TrimStart(delimiter).TrimEnd(delimiter);
        }

        public static int GetPathDepth(string path, char delimiter)
        {
            return string.IsNullOrEmpty(path) ? 0 : path.Count(c => c == delimiter);
        }
    }
}
