﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

using MessagePack;

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace JsonDictionaryCore
{
    internal static class JsonIo
    {
        public static bool SaveJson<T>(T data, string fileName, bool formatted = false)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            try
            {
                File.WriteAllText(fileName, JsonConvert.SerializeObject(data, formatted ? Formatting.Indented : Formatting.None));
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static T LoadJson<T>(string fileName)
        {
            T newValues = default;

            if (string.IsNullOrEmpty(fileName))
                return newValues;

            if (!File.Exists(fileName))
                return newValues;

            using StreamReader jsonFile = File.OpenText(fileName);
            JsonSerializer serializer = new JsonSerializer();
            newValues = (T)serializer.Deserialize(jsonFile, typeof(T));

            return newValues;
        }

        public static bool SaveBson<T>(T data, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            try
            {
                using (Stream file = File.Open(fileName, FileMode.Create))
                {
                    using (var writer = new BsonWriter(file))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, data);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static T LoadBson<T>(string fileName)
        {
            T nodeList = default;
            if (string.IsNullOrEmpty(fileName))
                return nodeList;

            using (Stream file = File.Open(fileName, FileMode.Open))
            {
                using (BsonReader reader = new BsonReader(file))
                {
                    reader.ReadRootValueAsArray = true;
                    JsonSerializer serializer = new JsonSerializer();
                    nodeList = serializer.Deserialize<T>(reader);
                }
            }

            return nodeList;
        }

        public static bool SaveBinary<T>(T data, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            using (Stream file = File.Open(fileName, FileMode.Create))
            {
                MessagePackSerializer.Serialize(file, data, MessagePackSerializerOptions
                    .Standard
                    .WithCompression(MessagePackCompression.Lz4BlockArray)
                    .WithResolver(MessagePack.Resolvers.StandardResolverAllowPrivate.Instance));
            }

            return true;
        }

        public static T LoadBinary<T>(string fileName)
        {
            T data = default;
            if (string.IsNullOrEmpty(fileName))
                return data;

            using (Stream file = File.Open(fileName, FileMode.Open))
            {
                data = MessagePackSerializer.Deserialize<T>(file,
                    MessagePackSerializerOptions
                    .Standard
                    .WithCompression(MessagePackCompression.Lz4BlockArray)
                    .WithResolver(MessagePack.Resolvers.StandardResolverAllowPrivate.Instance));
            }

            return data;
        }

        public static bool SaveBinaryTree<T>(T data, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            try
            {
                using (Stream file = File.Open(fileName, FileMode.Create))
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(file, data);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static T LoadBinaryTree<T>(string fileName)
        {
            T nodeList = default;
            if (string.IsNullOrEmpty(fileName))
                return nodeList;

            using (Stream file = File.Open(fileName, FileMode.Open))
            {
                var bf = new BinaryFormatter();
                nodeList = (T)bf.Deserialize(file);
            }

            return nodeList;
        }

        public static string[] ConvertTextToStringList(string data)
        {
            var stringCollection = new List<string>();
            if (string.IsNullOrEmpty(data))
                return stringCollection.ToArray();

            var lineDivider = new List<char> { '\x0d', '\x0a' };
            var unparsedData = new StringBuilder();
            foreach (var t in data)
            {
                if (lineDivider.Contains(t))
                {
                    if (unparsedData.Length > 0)
                    {
                        stringCollection.Add(unparsedData.ToString());
                        unparsedData.Clear();
                    }
                }
                else
                {
                    unparsedData.Append(t);
                }
            }

            if (unparsedData.Length > 0)
                stringCollection.Add(unparsedData.ToString());

            return stringCollection.ToArray();
        }

        public static string TrimJson(string original, bool trimEol)
        {
            if (string.IsNullOrEmpty(original))
                return original;

            original = original.Trim();
            if (string.IsNullOrEmpty(original))
                return original;

            if (trimEol)
            {
                original = original.Replace("\r\n", "\n");
                original = original.Replace('\r', '\n');
            }

            var i = original.IndexOf("\n ", StringComparison.Ordinal);
            while (i >= 0)
            {
                original = original.Replace("\n ", "\n");
                i = original.IndexOf("\n ", i, StringComparison.Ordinal);
            }

            if (trimEol)
                return original;

            i = original.IndexOf("\r ", StringComparison.Ordinal);
            while (i >= 0)
            {
                original = original.Replace("\r ", "\r");
                i = original.IndexOf("\r ", i, StringComparison.Ordinal);
            }

            return original;
        }

        public static string CompactJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                return json;

            json = json.Trim();

            return string.IsNullOrEmpty(json) ? json : ReformatJson(json, Formatting.None);
        }

        public static string BeautifyJson(string json, bool singleLineBrackets)
        {
            if (string.IsNullOrEmpty(json))
                return json;

            json = json.Trim();
            json = ReformatJson(json, Formatting.Indented);

            return singleLineBrackets ? JsonShiftBrackets_v2(json) : json;
        }

        public static string ReformatJson(string json, Formatting formatting)
        {
            bool trim = false;
            if (json[0] != '{' && json[0] != '[')
            {
                json = "{" + json + "}";
                trim = true;
            }
            try
            {
                using (var stringReader = new StringReader(json))
                {
                    using (var stringWriter = new StringWriter())
                    {
                        using (var jsonReader = new JsonTextReader(stringReader))
                        {
                            jsonReader.SupportMultipleContent = true;
                            using (var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = formatting })
                            {
                                jsonWriter.WriteToken(jsonReader);
                                json = stringWriter.ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return trim ? json.Substring(1, json.Length - 2).Trim() : json;
        }

        // possibly need rework
        public static string JsonShiftBrackets(string original)
        {
            if (string.IsNullOrEmpty(original))
                return original;

            var searchTokens = new[] { ": {", ": [" };
            foreach (var token in searchTokens)
            {
                var i = original.IndexOf(token, StringComparison.Ordinal);
                while (i >= 0)
                {
                    int currentPos;
                    if (original[i + token.Length] != '\r' &&
                        original[i + token.Length] != '\n') // not a single bracket
                    {
                        currentPos = i + 3;
                    }
                    else // need to shift bracket down the line
                    {
                        var j = i - 1;
                        var trail = 0;

                        if (j >= 0)
                            while (original[j] != '\n' && original[j] != '\r' && j >= 0)
                            {
                                if (original[j] == ' ')
                                    trail++;
                                else
                                    trail = 0;

                                j--;
                            }

                        if (j < 0)
                            j = 0;

                        if (!(original[j] == '/' && original[j + 1] == '/')) // if it's a comment
                            original = original.Insert(i + 2, Environment.NewLine + new string(' ', trail));

                        currentPos = i + 3;
                    }

                    i = original.IndexOf(token, currentPos, StringComparison.Ordinal);
                }
            }

            return original;
        }

        // definitely need rework
        public static string JsonShiftBrackets_v2(string original)
        {
            if (string.IsNullOrEmpty(original))
                return original;

            var searchTokens = new[] { ": {", ": [", ":{", ":[" };
            try
            {
                foreach (var token in searchTokens)
                {
                    var i = original.IndexOf(token, StringComparison.Ordinal);
                    while (i >= 0)
                    {
                        int currentPos;
                        // not a single bracket
                        if (original[i + token.Length] != '\r' && original[i + token.Length] != '\n')
                        {
                            currentPos = i + token.Length;
                        }
                        // need to shift bracket down the line
                        else
                        {
                            var j = i - 1;
                            var trail = 0;

                            if (j >= 0)
                            {
                                while (original[j] != '\n' && original[j] != '\r' && j >= 0)
                                {
                                    if (original[j] == ' ')
                                        trail++;
                                    else
                                        trail = 0;

                                    j--;
                                }
                            }

                            if (j < 0)
                                j = 0;

                            if (!(original[j] == '/' && original[j + 1] == '/')) // if it's a comment
                            {
                                original = original.Insert(i + 2, Environment.NewLine + new string(' ', trail));
                            }

                            currentPos = i + token.Length;
                        }

                        i = original.IndexOf(token, currentPos, StringComparison.Ordinal);
                    }
                }
            }
            catch
            {
                return original;
            }

            var stringList = ConvertTextToStringList(original);

            const char prefixItem = ' ';
            const int prefixStep = 2;
            var openBrackets = new[] { '{', '[' };
            var closeBrackets = new[] { '}', ']' };

            var prefixLength = 0;
            var prefix = "";
            var result = new StringBuilder();

            try
            {
                for (var i = 0; i < stringList.Length; i++)
                {
                    stringList[i] = stringList[i].Trim();
                    if (closeBrackets.Contains(stringList[i][0]))
                    {
                        prefixLength -= prefixStep;
                        if (prefixLength >= 0)
                            prefix = new string(prefixItem, prefixLength);
                    }

                    result.AppendLine(prefix + stringList[i]);

                    if (openBrackets.Contains(stringList[i][0]))
                    {
                        prefixLength += prefixStep;
                        if (stringList[i].Length > 1 && closeBrackets.Contains(stringList[i][stringList[i].Length - 1]))
                            prefixLength -= prefixStep;

                        if (prefixLength >= 0)
                            prefix = new string(prefixItem, prefixLength);
                    }
                }
            }
            catch
            {
                return original;
            }

            return result.ToString().Trim();
        }

        public static string ConvertStringForJSON(string s)
        {
            if (s == null || s.Length == 0)
            {
                return "";
            }

            char[] _escapeChars = new char[] { '\"', '\\', '/', 'b', 'f', 'n', 'r', 't', 'u' };
            char c;
            int len = s.Length;
            StringBuilder sb = new StringBuilder((int)(len * 1.1));
            String t;

            for (var i = 0; i < len; i += 1)
            {
                c = s[i];

                if (c == '\\' && i < len - 1 && _escapeChars.Contains(s[i + 1]))
                {
                    i++;
                    sb.Append("\\" + s[i]);
                }
                else
                {
                    switch (c)
                    {
                        case '"':
                        case '/':
                        case '\\':
                            sb.Append('\\');
                            sb.Append(c);
                            break;
                        case '\b':
                            sb.Append("\\b");
                            break;
                        case '\t':
                            sb.Append("\\t");
                            break;
                        case '\n':
                            sb.Append("\\n");
                            break;
                        case '\f':
                            sb.Append("\\f");
                            break;
                        case '\r':
                            sb.Append("\\r");
                            break;
                        default:
                            if (c < ' ')
                            {
                                t = $"\\u{((int)c).ToString("X4")}";
                                sb.Append($"\\u{t.Substring(t.Length - 4)}");
                            }
                            else
                            {
                                sb.Append(c);
                            }
                            break;
                    }
                }
            }
            return sb.ToString();
        }

    }
}
