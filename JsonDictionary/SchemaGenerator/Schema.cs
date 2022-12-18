using JsonPathParserLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JsonDictionaryCore.SchemaGenerator
{
    public interface ISchemaBase
    {
        public static readonly string[] SchemaDataTypes = new[]
        {
            "string",
            "number",
            "integer",
            "boolean",
            "null",
            "array",
            "object"
        };

        public static readonly string[] SchemaBoolTypes = new[]
        {
            "true",
            "false",
            "null"
        };

        public static class JsonSchemaTypes
        {
            public const string String = "string";
            public const string Number = "number";
            public const string Integer = "integer";
            public const string Object = "object";
            public const string Array = "array";
            public const string Boolean = "boolean";
            public const string Null = "null";
        }

        /// <summary>
        /// Parent node reference
        /// </summary>
        public ISchemaBase Parent { get; set; }

        /// <summary>
        /// property name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// "$id" property
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// "type"
        /// </summary>
        public List<string> Type { get; set; }

        /// <summary>
        /// "description"(v07) or "title"(v04)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Reference to predefined set of properties "$ref"
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        ///     "examples"
        /// </summary>
        public List<string> Examples { get; set; }

        public string GetReferencePath(ISchemaBase child = null);

        /// <summary>
        ///     find node by it's path
        /// </summary>
        /// <param name="id"></param>
        public ISchemaBase FindNodeByPath(string id);

        /// <summary>
        /// Find all nodes with reference to selected node
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public List<ISchemaBase> FindReferences(string reference);

        /// <summary>
        /// Change node name
        /// </summary>
        /// <param name="newName"></param>
        public void RenameNode(string newName);

        /// <summary>
        /// Delete current node from parent
        /// </summary>
        public bool DeleteNode();

        /// <summary>
        ///     Deep merge two objects
        /// </summary>
        /// <param name="newProperty"></param>
        /// <returns></returns>
        public bool Merge(ISchemaBase newProperty);

        /// <summary>
        ///     Replace all object content with new content
        /// </summary>
        /// <param name="newProperty"></param>
        /// <returns></returns>
        public bool Import(ISchemaBase newProperty);

        /// <summary>
        ///     Deep compare with object
        /// </summary>
        /// <param name="newProperty"></param>
        /// <param name="baseOnly"></param>
        /// <returns></returns>
        public bool Compare(ISchemaBase newProperty, bool baseOnly = false);

        /// <summary>
        ///     Returns JSON text of the object
        /// </summary>
        /// <returns></returns>
        public string ToJson(bool noSamples = true);
    }

    // default v07 schema property names
    public class SchemaPropertyNames
    {
        public char Divider = '/';
        public string Id = "$id";
        public string Schema = "$schema";
        public string Ref = "$ref";
        public string Comment = "$comment";
        public string Title = "title";
        public string Description = "description";
        public string Default = "default";
        public string ReadOnly = "readOnly";
        public string WriteOnly = "writeOnly";
        public string Examples = "examples";
        public string MultipleOf = "multipleOf";
        public string Maximum = "maximum";
        public string ExclusiveMaximum = "exclusiveMaximum";
        public string Minimum = "minimum";
        public string ExclusiveMinimum = "exclusiveMinimum";
        public string MaxLength = "maxLength";
        public string MinLength = "minLength";
        public string Pattern = "pattern";
        public string AdditionalItems = "additionalItems";
        public string Items = "items";
        public string MaxItems = "maxItems";
        public string MinItems = "minItems";
        public string UniqueItems = "uniqueItems";
        public string Contains = "contains";
        public string MaxProperties = "maxProperties";
        public string MinProperties = "minProperties";
        public string Required = "required";
        public string AdditionalProperties = "additionalProperties";
        public string Definitions = "definitions";
        public string Properties = "properties";
        public string PatternProperties = "patternProperties";
        public string Dependencies = "dependencies";
        public string PropertyNames = "propertyNames";
        public string Const = "const";
        public string Enum = "enum";
        public string Type = "type";
        public string Format = "format";
        public string ContentMediaType = "contentMediaType";
        public string ContentEncoding = "contentEncoding";
        public string If = "if";
        public string Then = "then";
        public string Else = "else";
        public string AllOf = "allOf";
        public string AnyOf = "anyOf";
        public string OneOf = "oneOf";
        public string Not = "not";
    }

    public class SchemaBaseClass_v07
    {
        private const string TypeErrorMessage = "Incorrect data type";
        #region Common fields

        /// <summary>
        ///     Name of the property
        /// </summary>
        public string Name;

        /// <summary>
        ///     The $id keyword defines a URI for the schema, and the base URI that other URI references within the schema are
        ///     resolved against.
        /// </summary>
        public string Id
        {
            get => _id;
            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute))
                    _id = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(Id));
            }
        }

        private string _id;

        /// <summary>
        ///     The $schema keyword states that this schema is written according to a specific draft of the standard and used for a
        ///     variety of reasons, primarily version control.
        /// </summary>
        public string Schema
        {
            get => _schema;
            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute))
                    _schema = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(Schema));
            }
        }

        private string _schema;

        /// <summary>
        /// </summary>
        public string Reference
        {
            get => _reference;
            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute))
                    _reference = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(Reference));
            }
        }

        private string _reference;

        /// <summary>
        ///     The $comment keyword is strictly intended for adding comments to a schema
        /// </summary>
        public string Comment;

        public string Title;
        public string Description;

        /// <summary>
        ///     The length of a string can be constrained using the maxLength keyword. The value must be a non-negative number
        /// </summary>
        public int? MaxLength
        {
            get => _maxLength;
            set
            {
                if (value >= 0)
                    _maxLength = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(MaxLength));
            }
        }

        private int? _maxLength;

        /// <summary>
        ///     The length of a string can be constrained using the minLength keyword. The value must be a non-negative number
        /// </summary>
        public int? MinLength
        {
            get => _minLength;
            set
            {
                if (value >= 0)
                    _minLength = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(MinLength));
            }
        }

        private int? _minLength = 0;

        /// <summary>
        ///     The boolean keywords readOnly and writeOnly are typically used in an API context. readOnly indicates that a value
        ///     should not be modified. It could be used to indicate that a PUT request that changes a value would result in a 400
        ///     Bad Request response
        /// </summary>
        public bool? ReadOnly;

        /// <summary>
        ///     writeOnly indicates that a value may be set, but will remain hidden. In could be used to indicate you can set a
        ///     value with a PUT request, but it would not be included when retrieving that record with a GET request
        /// </summary>
        public bool? WriteOnly;

        /// <summary>
        ///     The default keyword specifies a default value. This value is not used to fill in missing values during the
        ///     validation process
        /// </summary>
        public SchemaBaseClass_v07 Default;

        /// <summary>
        ///     The type validation keyword defines the first constraint on our JSON data
        /// </summary>
        public List<string> Type;

        public List<string> Examples;

        /// <summary>
        ///     The enum keyword is used to restrict a value to a fixed set of values
        /// </summary>
        public List<string> Enum; // "enum"

        public string Const;

        public List<SchemaBaseClass_v07> Definitions { get; set; }

        #endregion

        #region string specific

        /// <summary>
        ///     used to restrict a string to a particular regular expression. The regular expression syntax is the one defined in
        ///     JavaScript (ECMA 262 specifically) with Unicode support
        /// </summary>
        public string Pattern
        {
            get => _pattern;
            set
            {
                var rx = new Regex(@"\[Ref:(-?[0-9]+)/(-?[0-9]+)/(-?[0-9]+)\]");

                if (rx.IsMatch(value))
                    _pattern = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(Pattern));
            }
        }

        private string _pattern;

        /// <summary>
        ///     allows for basic semantic identification of certain kinds of string values that are commonly used.
        ///     Built-in formats:
        ///     - date-time
        ///     - time
        ///     - date
        ///     - duration - New in draft 2019-09
        ///     - email
        ///     - idn-email
        ///     - hostname
        ///     - idn-hostname
        ///     - ipv4
        ///     - ipv6
        ///     - uuid
        ///     - uri
        ///     - uri-reference
        ///     - iri
        ///     - iri-reference
        ///     - uri-template
        ///     - json-pointer
        ///     - relative-json-pointer
        ///     - regex
        /// </summary>
        public string Format;

        #endregion

        #region number specific

        /// <summary>
        ///     Ranges of numbers are specified using a combination of the minimum and maximum keywords, (or exclusiveMinimum and
        ///     exclusiveMaximum for expressing exclusive range).
        ///     x ≥ minimum
        ///     x > exclusiveMinimum
        ///     x ≤ maximum
        ///     x < exclusiveMaximum
        ///     x < summary
        public decimal? MultipleOf
        {
            get => _multipleOf;
            set
            {
                if (value > 0)
                    _multipleOf = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(MultipleOf));
            }
        }

        private decimal? _multipleOf;

        public decimal? Maximum;

        public decimal? ExclusiveMaximum;

        /// <summary>
        ///     include zero as a valid value
        /// </summary>
        public decimal? Minimum;

        /// <summary>
        ///     value must be something other than zero
        /// </summary>
        public decimal? ExclusiveMinimum;

        #endregion

        #region Object specific

        /// <summary>
        ///     The properties (key-value pairs) on an object are defined using the properties keyword. The value of properties is
        ///     an object, where each key is the name of a property and each value is a schema used to validate that property. Any
        ///     property that doesn’t match any of the property names in the properties keyword is ignored by this keyword
        /// </summary>
        public List<SchemaBaseClass_v07> Properties;

        /// <summary>
        ///     it maps regular expressions to schemas. If a property name matches the given regular expression, the property value
        ///     must validate against the corresponding schema
        /// </summary>
        public List<SchemaBaseClass_v07> PatternProperties;

        /// <summary>
        ///     The names of properties can be validated against a schema, irrespective of their values. You might, for example,
        ///     want to enforce that all names are valid ASCII tokens so they can be used as attributes in a particular programming
        ///     language.
        /// </summary>
        public SchemaBaseClass_v07 PropertyNames;

        /// <summary>
        ///     is used to control the handling of extra stuff, that is, properties whose names are not listed in the properties
        ///     keyword or match any of the regular expressions in the patternProperties keyword. By default any additional
        ///     properties are allowed.
        ///     // can be a list of objects or bool
        /// </summary>
        public object AdditionalProperties;

        /// <summary>
        ///     one can provide a list of required properties
        /// </summary>
        public List<string> Required;

        /// <summary>
        ///     The number of properties on an object can be restricted using the minProperties and maxProperties keywords. Each of
        ///     these must be a non-negative integer.
        /// </summary>
        private int? _maxProperties;

        /// <summary>
        ///     The number of properties on an object can be restricted using the minProperties and maxProperties keywords. Each of
        ///     these must be a non-negative integer.
        /// </summary>
        public int? MaxProperties
        {
            get => _maxProperties;
            set
            {
                if (value >= 0)
                    _maxProperties = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(MaxProperties));
            }
        }

        /// <summary>
        ///     The number of properties on an object can be restricted using the minProperties and maxProperties keywords. Each of
        ///     these must be a non-negative integer.
        /// </summary>
        private int? _minProperties = 0;

        /// <summary>
        ///     The number of properties on an object can be restricted using the minProperties and maxProperties keywords. Each of
        ///     these must be a non-negative integer.
        /// </summary>
        public int? MinProperties
        {
            get => _minProperties;
            set
            {
                if (value >= 0)
                    _minProperties = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(MinProperties));
            }
        }

        #endregion

        #region Array specific

        /// <summary>
        ///     define what appears in the array
        /// </summary>
        public object Items; // can be a List<SchemaBaseClass>, bool

        public object AdditionalItems; // can be a List<SchemaBaseClass>, bool

        /// <summary>
        ///     The items keyword can be used to control whether it’s valid to have additional items in a tuple beyond what is
        ///     defined in prefixItems. The value of the items keyword is a schema that all additional items must pass in order for
        ///     the keyword to validate
        /// </summary>
        public List<SchemaBaseClass_v07> PrefixItems;

        /// <summary>
        ///     While the items schema must be valid for every item in the array, the contains schema only needs to validate
        ///     against one or more items in the array
        /// </summary>
        public object Contains;

        /// <summary>
        ///     The length of the array can be specified using the minItems and maxItems keywords. The value of each keyword must
        ///     be a non-negative number
        /// </summary>
        public int? MaxItems
        {
            get => _maxItems;
            set
            {
                if (value >= 0)
                    _maxItems = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(MaxItems));
            }
        }

        private int? _maxItems;

        /// <summary>
        ///     The length of the array can be specified using the minItems and maxItems keywords. The value of each keyword must
        ///     be a non-negative number
        /// </summary>
        public int? MinItems
        {
            get => _minItems;
            set
            {
                if (value >= 0)
                    _minItems = value;
                else
                    throw new ArgumentException(TypeErrorMessage, nameof(MinItems));
            }
        }

        private int? _minItems = 0;

        /// <summary>
        ///     keyword notes all of the items in the array must be unique relative to one another
        /// </summary>
        public bool? UniqueItemsOnly;

        #endregion

        #region Media

        public string ContentMediaType;

        public string ContentEncoding;

        #endregion

        #region Schema Composition

        public List<SchemaBaseClass_v07> AllOf;

        public List<SchemaBaseClass_v07> AnyOf;

        public List<SchemaBaseClass_v07> OneOf;

        #endregion

        #region If-Then-Else

        public List<SchemaBaseClass_v07> If;

        public List<SchemaBaseClass_v07> Then;

        public List<SchemaBaseClass_v07> Else;

        public List<SchemaBaseClass_v07> Not;

        #endregion

        public SchemaBaseClass_v07()
        {
        }

        public SchemaBaseClass_v07(string name)
        {
            Name = name;
        }

        public string ToJson(bool noSamples = true)
        {
            var text = new StringBuilder();

            if (Name != null)
                text.AppendLine($"\"{Name}\": {{");

            if (Id != null)
                text.AppendLine($"\"$id\": \"{Id}\",");

            if (Type != null)
            {
                var t = new StringBuilder();
                if (Type.Count > 1)
                {
                    t.Append('[');

                    foreach (var item in Type)
                        t.Append($"\"{item}\",");

                    t.Append(']');
                }
                else if (Type.Count == 1)
                {
                    t.Append($"\"{Type[0]}\"");
                }
                else
                {
                    t.Append("\"\"");
                }

                text.AppendLine($"\"type\": {t},");
            }

            if (Description != null)
                text.AppendLine($"\"title\": \"{Description}\",");

            if (Reference != null)
                text.AppendLine($"\"$ref\": \"{Reference}\",");

            if (!noSamples && Examples != null && Examples.Count > 0)
            {
                var t = new StringBuilder();
                t.AppendLine("[");
                foreach (var item in Examples)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (Type?.Contains(ISchemaBase.JsonSchemaTypes.String) ?? false)
                            t.AppendLine($"\"{item}\",");
                        else
                            t.AppendLine($"{item},");
                    }
                }

                t.Append(']');
                text.AppendLine($"\"examples\": {t},");
            }

            text.AppendLine("}");

            return text.ToString();
        }

        public static SchemaBaseClass_v07 JsonPropertyListToSchemaObject_v7(
            IEnumerable<ParsedProperty> rootCollection,
            string startPath,
            string propertyName, char jsonPathDiv)
        {
            if (rootCollection == null) return null;

            // select properties describing current node from complete collection
            var properties = rootCollection.Where(n => n.ParentPath == startPath).ToArray();

            if (!properties.Any())
                return null;

            var propertyNames = new SchemaPropertyNames();

            // get "schema"
            var nodeSchemaVersion = properties.FirstOrDefault(n => n.Name == propertyNames.Schema)?.Value;

            if (!string.IsNullOrEmpty(nodeSchemaVersion) && nodeSchemaVersion.Equals("http://json-schema.org/draft-04/schema#",
                    StringComparison.OrdinalIgnoreCase))
                propertyNames.Id = "id";

            //get "type" of the object
            var nodeTypes = new List<string>();
            var typePropertyPathSample = startPath + jsonPathDiv + propertyNames.Type;
            var currentNodeType = properties.FirstOrDefault(n => n.Path == typePropertyPathSample) ??
                                  new ParsedProperty();

            if (currentNodeType.JsonPropertyType == JsonPropertyType.Array)
            {
                var childNodesTypes = rootCollection
                    .Where(n => n.ParentPath == typePropertyPathSample)
                    .Select(n => n.Value);
                nodeTypes.AddRange(childNodesTypes);
            }
            else
            {
                nodeTypes.Add(currentNodeType.Value);
            }

            nodeTypes.Sort();

            // get "id"
            var nodeId = properties.FirstOrDefault(n => n.Name == propertyNames.Id)?.Value;

            //get "title"
            var nodeDescription = properties.FirstOrDefault(n => n.Name == propertyNames.Title)?.Value;
            var reference = properties.FirstOrDefault(n => n.Name == propertyNames.Ref)?.Value;
            var nodeExamples = rootCollection
                .Where(n => n.ParentPath == startPath + jsonPathDiv + propertyNames.Examples)
                .Select(n => n.Value)
                .OrderBy(n => n)
                .ToList();

            // to do - get all available properties even if there is a mix of object/array/property
            if (nodeTypes.Any(n => n == ISchemaBase.JsonSchemaTypes.Array))
            {
                var arrayNode = new SchemaBaseClass_v07(propertyName)
                {
                    Type = nodeTypes,
                    Id = nodeId,
                    Description = nodeDescription,
                    Reference = reference,
                    Examples = nodeExamples
                };

                if (bool.TryParse(properties.FirstOrDefault(n => n.Name == propertyNames.UniqueItems)?.Value,
                    out var ap))
                    arrayNode.UniqueItemsOnly = ap;
                else
                    arrayNode.UniqueItemsOnly = null;

                var newItem = JsonPropertyListToSchemaObject_v7(rootCollection,
                    startPath + jsonPathDiv + propertyNames.Items,
                    propertyNames.Items,
                    jsonPathDiv);
                arrayNode.Items = newItem;

                return arrayNode;
            }

            if (nodeTypes.Any(n => n == ISchemaBase.JsonSchemaTypes.Object))
            {
                var objectNode = new SchemaBaseClass_v07(propertyName)
                {
                    Name = propertyName,
                    Type = nodeTypes,
                    Id = nodeId,
                    Description = nodeDescription,
                    Reference = reference,
                    Examples = nodeExamples,
                    Required = rootCollection
                        .Where(n => n.ParentPath == startPath + jsonPathDiv + propertyNames.Required)
                        .Select(n => n.Value)
                        .OrderBy(n => n)
                        .ToList()
                };

                if (bool.TryParse(properties.FirstOrDefault(n => n.Name == propertyNames.AdditionalProperties)?.Value,
                    out var ap))
                    objectNode.AdditionalProperties = ap;
                else
                    objectNode.AdditionalProperties = null;

                var childNodes = rootCollection
                    .Where(n => n.ParentPath == startPath + jsonPathDiv + propertyNames.Properties)
                    .OrderBy(n => n.Name);

                foreach (var item in childNodes)
                {
                    var newProperty =
                        JsonPropertyListToSchemaObject_v7(rootCollection, item.Path, item.Name, jsonPathDiv);
                    objectNode.Properties.Add(newProperty);
                }

                if (startPath == "#")
                {
                    objectNode.Schema = properties.FirstOrDefault(n => n.Name == propertyNames.Schema)?.Value;

                    var childDefs = rootCollection
                        .Where(n => n.ParentPath == startPath + jsonPathDiv + propertyNames.Definitions)
                        .OrderBy(n => n.Name);

                    foreach (var item in childDefs)
                    {
                        var newProperty =
                            JsonPropertyListToSchemaObject_v7(rootCollection, item.Path, item.Name, jsonPathDiv);
                        objectNode.Definitions.Add(newProperty);
                    }
                }

                return objectNode;
            }

            var propertyNode = new SchemaBaseClass_v07(propertyName)
            {
                Type = nodeTypes,
                Id = nodeId,
                Description = nodeDescription,
                Reference = reference,
                Examples = nodeExamples,
                Pattern = properties.FirstOrDefault(n => n.Name == propertyNames.Pattern)?.Value,
                Enum = rootCollection
                    .Where(n => n.ParentPath == startPath + jsonPathDiv + propertyNames.Enum)
                    .Select(n => n.Value)
                    .OrderBy(n => n)
                    .ToList(),
                Default = JsonPropertyListToSchemaObject_v7(rootCollection,
                startPath + jsonPathDiv + propertyNames.Default, propertyNames.Default, jsonPathDiv)
            };

            return propertyNode;
        }
    }

    public class SchemaBase : ISchemaBase
    {
        public ISchemaBase Parent { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Type { get; set; } = new List<string>();
        public string Description { get; set; }
        public string Reference { get; set; }
        public List<string> Examples { get; set; } = new List<string>();

        public SchemaBase()
        {
        }

        public SchemaBase(string name)
        {
            Name = name;
        }

        public string GetReferencePath(ISchemaBase child = null)
        {
            if (child != null)
                throw new ArgumentOutOfRangeException(nameof(child), "Property can not have child");

            var referencePath = "";

            if (Parent is SchemaObject oobj)
            {
                referencePath = oobj.GetReferencePath(this);
            }
            else if (Parent is SchemaArray aobj)
            {
                referencePath = aobj.GetReferencePath(this);
            }

            referencePath += "/" + Name;

            return referencePath;
        }

        public ISchemaBase FindNodeByPath(string id)
        {
            return Id == id ? this : null;
        }

        public List<ISchemaBase> FindReferences(string reference)
        {
            var result = new List<ISchemaBase>();
            if (Reference.StartsWith(reference))
                result.Add(this);

            return result;
        }

        public void RenameNode(string newName)
        {
            Name = newName;
        }

        public bool DeleteNode()
        {
            ISchemaBase result = null;
            var parent = Parent;
            var name = Name;

            if (parent is SchemaObject schemaObject)
            {
                foreach (var childNode in schemaObject.Properties)
                {
                    if (childNode.Name == name)
                        result = childNode;
                }

                if (result != null)
                {
                    schemaObject.Properties.Remove(result);
                    return true;
                }

                foreach (var childNode in schemaObject.Definitions)
                {
                    if (childNode.Name == name)
                        result = childNode;
                }

                if (result != null)
                {
                    schemaObject.Definitions.Remove(result);
                    return true;
                }
            }
            else if (parent is SchemaArray schemaArray)
            {
                if (schemaArray.Items.Name == name)
                    schemaArray.Items = null;
                else if (schemaArray.Items is SchemaObject tmpItem)
                {
                    foreach (var childNode in tmpItem.Properties)
                    {
                        if (childNode.Name == name)
                            result = childNode;
                    }
                    if (result != null)
                    {
                        tmpItem.Properties.Remove(result);
                        return true;
                    }

                    foreach (var childNode in tmpItem.Definitions)
                    {
                        if (childNode.Name == name)
                            result = childNode;
                    }

                    if (result != null)
                    {
                        tmpItem.Definitions.Remove(result);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Merge(ISchemaBase newProperty)
        {
            var tmpProperty = new SchemaProperty();
            if (!Compare(newProperty)) return false;

            if (newProperty is SchemaProperty prop)
            {
                tmpProperty.Name = Name;
                tmpProperty.Id = Id;

                if (Description != prop.Description)
                    tmpProperty.Description = (Description + prop.Description).Trim();

                if (string.IsNullOrEmpty(Reference) || string.IsNullOrEmpty(prop.Reference))
                    tmpProperty.Reference = (Reference + prop.Reference).Trim();
                else
                    return false;

                foreach (var t1 in prop.Type)
                {
                    if (!Type.Contains(t1))
                        Type.Add(t1);
                }

                foreach (var t1 in prop.Examples)
                {
                    if (!Examples.Contains(t1))
                        Examples.Add(t1);
                }

                Import(tmpProperty);

                return true;
            }

            return false;
        }

        public bool Import(ISchemaBase newProperty)
        {
            if (newProperty is SchemaProperty prop)
            {
                Name = prop.Name;
                Id = prop.Id;

                Description = prop.Description;
                Reference = prop.Reference;

                Type = new List<string>();
                foreach (var t2 in prop.Type) Type.Add(t2);

                Examples = new List<string>();
                foreach (var t2 in prop.Examples) Examples.Add(t2);

                return true;
            }

            return false;
        }

        public bool Compare(ISchemaBase newProperty, bool baseOnly = false)
        {
            if (newProperty is SchemaProperty prop)
            {
                if (Name != prop.Name)
                    return false;
                else if (Id != prop.Id)
                    return false;
                else if (!baseOnly)
                {
                    if (Description != prop.Description)
                        return false;
                    else if (Reference != prop.Reference)
                        return false;
                    else
                    {
                        if (Type.Count == prop.Type.Count)
                        {
                            foreach (var t1 in Type)
                            {
                                if (!prop.Type.Contains(t1))
                                    return false;
                            }
                        }
                        else
                            return false;

                        if (Examples.Count == prop.Examples.Count)
                        {
                            foreach (var t1 in Examples)
                            {
                                if (!prop.Examples.Contains(t1))
                                    return false;
                            }
                        }
                        else
                            return false;
                    }
                }
            }
            else
                return false;

            return true;
        }

        public string ToJson(bool noSamples = true)
        {
            var text = new StringBuilder();

            if (Name != null)
                text.AppendLine($"\"{Name}\": {{");

            if (Id != null)
                text.AppendLine($"\"$id\": \"{Id}\",");

            if (Type != null)
            {
                var t = new StringBuilder();
                if (Type.Count > 1)
                {
                    t.Append('[');

                    foreach (var item in Type) t.Append($"\"{item}\",");

                    t.Append(']');
                }
                else if (Type.Count == 1)
                {
                    t.Append($"\"{Type[0]}\"");
                }
                else
                {
                    t.Append("\"\"");
                }

                text.AppendLine($"\"type\": {t},");
            }

            if (Description != null)
                text.AppendLine($"\"title\": \"{Description}\",");

            if (Reference != null)
                text.AppendLine($"\"$ref\": \"{Reference}\",");

            if (!noSamples && Examples != null && Examples.Count > 0)
            {
                var t = new StringBuilder();
                t.AppendLine("[");
                foreach (var item in Examples)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (Type?.Contains(ISchemaBase.JsonSchemaTypes.String) ?? false)
                            t.AppendLine($"\"{item}\",");
                        else
                            t.AppendLine($"{item},");
                    }
                }

                t.Append(']');
                text.AppendLine($"\"examples\": {t},");
            }

            text.AppendLine("}");

            return text.ToString();
        }
    }

    public class SchemaProperty : ISchemaBase
    {
        public ISchemaBase Parent { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Type { get; set; } = new List<string>();
        public string Description { get; set; }
        public string Reference { get; set; }
        public List<string> Examples { get; set; } = new List<string>();

        // specific properties
        public string Default { get; set; } // "default"

        public string Pattern { get; set; } // "pattern"

        public List<string> Enum { get; set; } = new List<string>(); // "enum"

        public SchemaProperty()
        {
        }

        public SchemaProperty(string name)
        {
            Name = name;
        }

        public string GetReferencePath(ISchemaBase child = null)
        {
            if (child != null)
                throw new ArgumentOutOfRangeException(nameof(child), "Property can not have child");

            var referencePath = "";

            if (Parent is SchemaObject oobj)
            {
                referencePath = oobj.GetReferencePath(this);
            }
            else if (Parent is SchemaArray aobj)
            {
                referencePath = aobj.GetReferencePath(this);
            }

            if (!string.IsNullOrEmpty(referencePath)) referencePath += "/";
            referencePath += Name;

            return referencePath;
        }

        public ISchemaBase FindNodeByPath(string id)
        {
            return Id == id ? this : null;
        }

        public List<ISchemaBase> FindReferences(string reference)
        {
            var result = new List<ISchemaBase>();
            if (Reference != null && Reference.StartsWith(reference))
                result.Add(this);

            return result;
        }

        public void RenameNode(string newName)
        {
            var oldRefPath = GetReferencePath();
            Name = newName;
            var newRefPath = GetReferencePath();

            ISchemaBase rootNode = this;
            while (rootNode.Parent != null)
                rootNode = rootNode.Parent;

            var references = rootNode.FindReferences(oldRefPath);
            if (references?.Count > 0)
            {
                foreach (var schemaItem in references)
                {
                    schemaItem.Reference = newRefPath + schemaItem.Reference.Substring(oldRefPath.Length);
                }
            }
        }

        public bool DeleteNode()
        {
            ISchemaBase result = null;
            var parent = Parent;
            var name = Name;

            if (parent is SchemaObject schemaObject)
            {
                foreach (var childNode in schemaObject.Properties)
                {
                    if (childNode.Name == name)
                        result = childNode;
                }

                if (result != null)
                {
                    schemaObject.Properties.Remove(result);
                    return true;
                }

                foreach (var childNode in schemaObject.Definitions)
                {
                    if (childNode.Name == name)
                        result = childNode;
                }

                if (result != null)
                {
                    schemaObject.Definitions.Remove(result);
                    return true;
                }
            }
            else if (parent is SchemaArray schemaArray)
            {
                if (schemaArray.Items.Name == name)
                    schemaArray.Items = null;
                else if (schemaArray.Items is SchemaObject tmpItem)
                {
                    foreach (var childNode in tmpItem.Properties)
                    {
                        if (childNode.Name == name)
                            result = childNode;
                    }
                    if (result != null)
                    {
                        tmpItem.Properties.Remove(result);
                        return true;
                    }

                    foreach (var childNode in tmpItem.Definitions)
                    {
                        if (childNode.Name == name)
                            result = childNode;
                    }

                    if (result != null)
                    {
                        tmpItem.Definitions.Remove(result);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Merge(ISchemaBase newProperty)
        {
            var tmpProperty = new SchemaProperty();
            if (!Compare(newProperty)) return false;

            if (newProperty is SchemaProperty prop)
            {
                tmpProperty.Name = Name;
                tmpProperty.Id = Id;

                if (Description != prop.Description)
                    tmpProperty.Description = (Description + prop.Description).Trim();

                if (string.IsNullOrEmpty(Reference) || string.IsNullOrEmpty(prop.Reference))
                    tmpProperty.Reference = (Reference + prop.Reference).Trim();
                else
                    return false;

                foreach (var t1 in prop.Type)
                {
                    if (!Type.Contains(t1))
                        Type.Add(t1);
                }

                foreach (var t1 in prop.Examples)
                {
                    if (!Examples.Contains(t1))
                        Examples.Add(t1);
                }

                foreach (var t1 in prop.Enum)
                {
                    if (!Enum.Contains(t1))
                        Enum.Add(t1);
                }

                if (string.IsNullOrEmpty(Default) || string.IsNullOrEmpty(prop.Default))
                    tmpProperty.Default = (Default + prop.Default).Trim();
                else
                    return false;

                if (string.IsNullOrEmpty(Pattern) || string.IsNullOrEmpty(prop.Pattern))
                    tmpProperty.Pattern = (Reference + prop.Pattern).Trim();
                else
                    return false;

                Import(tmpProperty);

                return true;
            }

            return false;
        }

        public bool Import(ISchemaBase newProperty)
        {
            if (newProperty is SchemaProperty prop)
            {
                Name = prop.Name;
                Id = prop.Id;

                Description = prop.Description;
                Reference = prop.Reference;

                Type = new List<string>();
                foreach (var t2 in prop.Type) Type.Add(t2);

                Examples = new List<string>();
                foreach (var t2 in prop.Examples) Examples.Add(t2);

                Default = prop.Default;
                Pattern = prop.Pattern;

                Enum = new List<string>();

                foreach (var t2 in prop.Enum)
                    Enum.Add(t2);

                return true;
            }

            return false;
        }

        public bool Compare(ISchemaBase newProperty, bool baseOnly = false)
        {
            if (newProperty is SchemaProperty prop)
            {
                if (Name != prop.Name)
                    return false;
                else if (Id != prop.Id)
                    return false;
                else if (!baseOnly)
                {
                    if (Description != prop.Description)
                        return false;
                    else if (Reference != prop.Reference)
                        return false;
                    else
                    {
                        if (Type.Count == prop.Type.Count)
                        {
                            foreach (var t1 in Type)
                            {
                                if (!prop.Type.Contains(t1))
                                    return false;
                            }
                        }
                        else
                            return false;

                        if (Examples.Count == prop.Examples.Count)
                        {
                            foreach (var t1 in Examples)
                            {
                                if (!prop.Examples.Contains(t1))
                                    return false;
                            }
                        }
                        else
                            return false;
                    }

                    if (Default != prop.Default)
                        return false;
                    else if (Pattern != prop.Pattern)
                        return false;
                    else if (Enum.Count == prop.Enum.Count)
                    {
                        foreach (var t1 in Enum)
                        {
                            if (!prop.Enum.Contains(t1))
                                return false;
                        }
                    }
                    else
                        return false;
                }
            }
            else
                return false;

            return true;
        }

        public string ToJson(bool noSamples = true)
        {
            var text = new StringBuilder();

            if (Name != null)
                text.AppendLine($"\"{Name}\": {{");

            if (Id != null)
                text.AppendLine($"\"$id\": \"{Id}\",");

            if (Type != null)
            {
                var t = new StringBuilder();
                if (Type.Count > 1)
                {
                    t.Append('[');

                    foreach (var item in Type) t.Append($"\"{item}\",");

                    t.Append(']');
                }
                else if (Type.Count == 1)
                {
                    t.Append($"\"{Type[0]}\"");
                }
                else
                {
                    t.Append("\"\"");
                }

                text.AppendLine($"\"type\": {t},");
            }

            if (Description != null)
                text.AppendLine($"\"title\": \"{Description}\",");

            if (Reference != null)
                text.AppendLine($"\"$ref\": \"{Reference}\",");

            if (!noSamples && Examples != null && Examples.Count > 0)
            {
                var t = new StringBuilder();
                t.AppendLine("[");
                foreach (var item in Examples)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (Type?.Contains(ISchemaBase.JsonSchemaTypes.String) ?? false)
                            t.AppendLine($"\"{item}\",");
                        else
                            t.AppendLine($"{item},");
                    }
                }

                t.Append(']');
                text.AppendLine($"\"examples\": {t},");
            }

            if (Default != null)
            {
                if (Type?.Contains(ISchemaBase.JsonSchemaTypes.String) ?? false)
                    text.AppendLine($"\"default\": \"{Default}\",");
                else
                    text.AppendLine($"\"default\": {Default},");
            }

            if (Pattern != null)
                text.AppendLine($"\"pattern\": \"{Pattern}\",");

            if (Enum != null && Enum.Count > 0)
            {
                var t = new StringBuilder();
                t.AppendLine("[");
                foreach (var item in Enum)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (Type?.Contains(ISchemaBase.JsonSchemaTypes.String) ?? false)
                            t.AppendLine($"\"{item}\",");
                        else
                            t.AppendLine($"{item},");
                    }
                }

                if (Type != null)
                {
                    if (Type.Contains(ISchemaBase.JsonSchemaTypes.Boolean))
                    {
                        t.AppendLine("true,");
                        t.AppendLine("false,");
                    }

                    if (Type.Contains(ISchemaBase.JsonSchemaTypes.Null))
                        t.AppendLine("null,");
                }

                t.Append(']');
                text.AppendLine($"\"enum\": {t},");
            }

            text.AppendLine("}");

            return text.ToString();
        }
    }

    public class SchemaObject : ISchemaBase
    {
        public ISchemaBase Parent { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Type { get; set; } = new List<string>();
        public string Description { get; set; }
        public string Reference { get; set; }
        public List<string> Examples { get; set; } = new List<string>();

        // specific properties
        public string SchemaName { get; set; } // "$schema"

        public bool? AdditionalProperties { get; set; } = false; // "additionalProperties"

        public List<string> Required { get; set; } = new List<string>(); // "required"

        public List<ISchemaBase> Properties { get; set; } = new List<ISchemaBase>(); // "properties"

        public List<ISchemaBase> Definitions { get; set; } = new List<ISchemaBase>(); // "definitions"

        public SchemaObject()
        {
        }

        public SchemaObject(string name)
        {
            Name = name;
        }

        public string GetReferencePath(ISchemaBase child = null)
        {
            var referencePath = "";

            if (Parent is SchemaObject oobj)
            {
                referencePath = oobj.GetReferencePath(this);
            }
            else if (Parent is SchemaArray aobj)
            {
                referencePath = aobj.GetReferencePath(this);
            }

            if (!string.IsNullOrEmpty(referencePath)) referencePath += "/";
            referencePath += Name;

            if (child != null)
            {
                referencePath += "/properties";
            }

            return referencePath;
        }

        public ISchemaBase FindNodeByPath(string id)
        {
            if (Id == id) return this;

            foreach (var property in Properties)
            {
                var foundProperty = property.FindNodeByPath(id);

                if (foundProperty != null)
                    return foundProperty;
            }

            return null;
        }

        public List<ISchemaBase> FindReferences(string reference)
        {
            var result = new List<ISchemaBase>();

            if (Reference != null && Reference.StartsWith(reference))
                result.Add(this);

            foreach (var property in Properties)
            {
                var foundProperty = property.FindReferences(reference);

                if (foundProperty != null && foundProperty.Count > 0)
                    result.AddRange(foundProperty);
            }

            return result;
        }

        public void RenameNode(string newName)
        {
            var oldRefPath = GetReferencePath();
            Name = newName;
            var newRefPath = GetReferencePath();

            ISchemaBase rootNode = this;
            while (rootNode.Parent != null)
                rootNode = rootNode.Parent;

            var references = rootNode.FindReferences(oldRefPath);
            if (references?.Count > 0)
            {
                foreach (var schemaItem in references)
                {
                    schemaItem.Reference = newRefPath + schemaItem.Reference.Substring(oldRefPath.Length);
                }
            }
        }

        public bool DeleteNode()
        {
            ISchemaBase result = null;
            var parent = Parent;
            var name = Name;

            if (parent is SchemaObject schemaObject)
            {
                foreach (var childNode in schemaObject.Properties)
                {
                    if (childNode.Name == name)
                        result = childNode;
                }

                if (result != null)
                {
                    schemaObject.Properties.Remove(result);
                    return true;
                }

                foreach (var childNode in schemaObject.Definitions)
                {
                    if (childNode.Name == name)
                        result = childNode;
                }

                if (result != null)
                {
                    schemaObject.Definitions.Remove(result);
                    return true;
                }
            }
            else if (parent is SchemaArray schemaArray)
            {
                if (schemaArray.Items.Name == name)
                    schemaArray.Items = null;
                else if (schemaArray.Items is SchemaObject tmpItem)
                {
                    foreach (var childNode in tmpItem.Properties)
                    {
                        if (childNode.Name == name)
                            result = childNode;
                    }
                    if (result != null)
                    {
                        tmpItem.Properties.Remove(result);
                        return true;
                    }

                    foreach (var childNode in tmpItem.Definitions)
                    {
                        if (childNode.Name == name)
                            result = childNode;
                    }

                    if (result != null)
                    {
                        tmpItem.Definitions.Remove(result);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Merge(ISchemaBase newProperty)
        {
            var tmpProperty = new SchemaObject();

            if (!Compare(newProperty))
                return false;

            if (newProperty is SchemaObject prop)
            {
                tmpProperty.Name = Name;
                tmpProperty.Id = Id;

                if (Description != prop.Description)
                    tmpProperty.Description = (Description + prop.Description).Trim();

                if (string.IsNullOrEmpty(Reference) || string.IsNullOrEmpty(prop.Reference))
                    tmpProperty.Reference = (Reference + prop.Reference).Trim();
                else
                    return false;

                foreach (var p in Type)
                    tmpProperty.Type.Add(p);

                foreach (var t1 in prop.Type)
                {
                    if (!tmpProperty.Type.Contains(t1))
                        tmpProperty.Type.Add(t1);
                }

                foreach (var p in Examples)
                    tmpProperty.Examples.Add(p);

                foreach (var t1 in prop.Examples)
                {
                    if (!tmpProperty.Examples.Contains(t1))
                        tmpProperty.Examples.Add(t1);
                }

                foreach (var p in Required)
                    tmpProperty.Required.Add(p);

                foreach (var t1 in prop.Required)
                {
                    if (!tmpProperty.Required.Contains(t1))
                        tmpProperty.Required.Add(t1);
                }

                foreach (var p in Properties)
                    tmpProperty.Properties.Add(p);

                for (var i = 0; i < tmpProperty.Properties.Count; i++)
                {
                    foreach (var p2 in prop.Properties)
                    {
                        if (tmpProperty.Properties[i].Compare(p2))
                        {
                            tmpProperty.Properties[i].Merge(p2);
                        }
                        else
                        {
                            ISchemaBase newProp;
                            if (p2 is SchemaArray)
                                newProp = new SchemaArray();
                            else if (p2 is SchemaObject)
                                newProp = new SchemaObject();
                            else
                                newProp = new SchemaProperty();

                            newProp.Import(p2);
                            tmpProperty.Properties.Add(newProp);
                        }
                    }
                }

                Import(tmpProperty);

                return true;
            }

            return false;
        }

        public bool Import(ISchemaBase newProperty)
        {
            if (newProperty is SchemaObject prop)
            {
                Name = prop.Name;
                Id = prop.Id;

                Description = prop.Description;
                Reference = prop.Reference;

                Type = new List<string>();
                foreach (var t2 in prop.Type)
                    Type.Add(t2);

                Examples = new List<string>();
                foreach (var t2 in prop.Examples)
                    Examples.Add(t2);

                AdditionalProperties = prop.AdditionalProperties;

                Required = new List<string>();
                foreach (var t2 in prop.Required)
                    Required.Add(t2);

                // process Properties
                Properties = new List<ISchemaBase>();
                foreach (var p2 in prop.Properties)
                {
                    ISchemaBase newProp;
                    if (p2 is SchemaArray)
                        newProp = new SchemaArray();
                    else if (p2 is SchemaObject)
                        newProp = new SchemaObject();
                    else
                        newProp = new SchemaProperty();

                    newProp.Import(p2);
                    Properties.Add(newProp);
                }

                return true;
            }

            return false;
        }

        // TODO: redesign
        public bool Compare(ISchemaBase newProperty, bool baseOnly = false)
        {
            if (newProperty is SchemaObject prop)
            {
                if (Name != prop.Name)
                    return false;
                else if (Id != prop.Id)
                    return false;
                else if (!baseOnly)
                {
                    if (Description != prop.Description)
                        return false;
                    else if (Reference != prop.Reference)
                        return false;
                    else
                    {
                        if (Type.Count == prop.Type.Count)
                        {
                            foreach (var t1 in Type)
                            {
                                if (!prop.Type.Contains(t1))
                                    return false;
                            }
                        }
                        else
                            return false;

                        if (Examples.Count == prop.Examples.Count)
                        {
                            foreach (var t1 in Examples)
                            {
                                if (!prop.Examples.Contains(t1))
                                    return false;
                            }
                        }
                        else
                            return false;
                    }

                    if (AdditionalProperties != prop.AdditionalProperties)
                        return false;
                    else if (Required.Count == prop.Required.Count)
                    {
                        foreach (var t1 in Required)
                        {
                            if (!prop.Required.Contains(t1))
                                return false;
                        }
                    }
                    else
                        return false;

                    if (Properties.Count != prop.Properties.Count)
                        return false;

                    foreach (var t1 in Properties)
                    {
                        foreach (var t2 in prop.Properties)
                        {
                            if (t2.Name == t1.Name && !t1.Compare(t2))
                                return false;
                        }
                    }
                }
            }
            else
                return false;

            return true;
        }

        public string ToJson(bool noSamples = true)
        {
            var text = new StringBuilder();

            if (string.IsNullOrEmpty(Name) || Name == "#")
                text.Append('{');
            else
                text.AppendLine($"\"{Name}\": {{");

            if (SchemaName != null)
                text.AppendLine($"\"$schema\": \"{SchemaName}\",");

            if (Id != null)
                text.AppendLine($"\"$id\": \"{Id}\",");

            if (Type != null)
            {
                var t = new StringBuilder();
                if (Type.Count > 1)
                {
                    t.Append('[');

                    foreach (var item in Type) t.Append($"\"{item}\",");

                    t.Append(']');
                }
                else if (Type.Count == 1)
                {
                    t.Append($"\"{Type[0]}\"");
                }
                else
                {
                    t.Append("\"\"");
                }

                text.AppendLine($"\"type\": {t},");
            }

            if (Description != null)
                text.AppendLine($"\"title\": \"{Description}\",");

            if (Reference != null)
                text.AppendLine($"\"$ref\": \"{Reference}\",");

            if (!noSamples && Examples != null && Examples.Count > 0)
            {
                var t = new StringBuilder();
                t.AppendLine("[");
                foreach (var item in Examples)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (Type?.Contains(ISchemaBase.JsonSchemaTypes.String) ?? false)
                            t.AppendLine($"\"{item}\",");
                        else
                            t.AppendLine($"{item},");
                    }
                }

                t.Append(']');
                text.AppendLine($"\"examples\": {t},");
            }

            if (Required != null && Required.Count > 0)
            {
                var t = new StringBuilder();
                t.AppendLine("[");
                foreach (var item in Required)
                {
                    if (!string.IsNullOrEmpty(item))
                        t.AppendLine($"\"{item}\",");
                }

                t.Append(']');
                text.AppendLine($"\"required\": {t},");
            }

            if (AdditionalProperties != null)
                text.AppendLine($"\"additionalProperties\": {AdditionalProperties.ToString().ToLower()},");

            if (Properties != null && Properties.Count > 0)
            {
                text.AppendLine("\"properties\": {");
                foreach (var prop in Properties)
                    text.AppendLine(prop.ToJson() + ",");

                text.AppendLine("}");
            }

            if (Definitions != null && Definitions.Count > 0)
            {
                text.AppendLine("\"definitions\": {");
                foreach (var prop in Definitions)
                    text.AppendLine(prop.ToJson() + ",");

                text.AppendLine("}");
            }

            text.AppendLine("}");

            return text.ToString();
        }
    }

    public class SchemaArray : ISchemaBase
    {
        public ISchemaBase Parent { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Type { get; set; } = new List<string>();
        public string Description { get; set; }
        public string Reference { get; set; }
        public List<string> Examples { get; set; } = new List<string>();

        // specific properties
        public bool? UniqueItemsOnly { get; set; } // "uniqueItems"

        public ISchemaBase Items { get; set; } // "items"

        public SchemaArray()
        {
        }

        public SchemaArray(string name)
        {
            Name = name;
        }

        public string GetReferencePath(ISchemaBase child = null)
        {
            var referencePath = "";

            if (Parent is SchemaObject Oobj)
            {
                referencePath = Oobj.GetReferencePath(this);
            }
            else if (Parent is SchemaArray Aobj)
            {
                referencePath = Aobj.GetReferencePath(this);
            }

            if (!string.IsNullOrEmpty(referencePath)) referencePath += "/";
            referencePath += Name;

            /*if (child != null)
            {
                referencePath += "/items/properties";
            }*/

            return referencePath;
        }

        public ISchemaBase FindNodeByPath(string id)
        {
            if (Id == id)
                return this;

            if (Items is SchemaProperty schemaProperty)
                return schemaProperty.FindNodeByPath(id);

            if (Items is SchemaObject schemaObject)
            {
                foreach (var property in schemaObject.Properties)
                {
                    var foundProperty = property.FindNodeByPath(id);
                    if (foundProperty != null)
                        return foundProperty;
                }
            }
            else if (Items is SchemaArray schemaArray)
                return schemaArray.FindNodeByPath(id);

            return null;
        }

        public List<ISchemaBase> FindReferences(string reference)
        {
            var result = new List<ISchemaBase>();

            if (Reference != null && Reference.StartsWith(reference))
                result.Add(this);

            var foundProperty = Items.FindReferences(reference);
            if (foundProperty != null && foundProperty.Count > 0)
                result.AddRange(foundProperty);

            return result;
        }

        public void RenameNode(string newName)
        {
            var oldRefPath = GetReferencePath();
            Name = newName;
            var newRefPath = GetReferencePath();

            ISchemaBase rootNode = this;
            while (rootNode.Parent != null)
                rootNode = rootNode.Parent;

            var references = rootNode.FindReferences(oldRefPath);
            if (references?.Count > 0)
            {
                foreach (var schemaItem in references)
                {
                    schemaItem.Reference = newRefPath + schemaItem.Reference.Substring(oldRefPath.Length);
                }
            }
        }

        public bool DeleteNode()
        {
            ISchemaBase result = null;
            var parent = Parent;
            var name = Name;

            if (parent is SchemaObject schemaObject)
            {
                foreach (var childNode in schemaObject.Properties)
                {
                    if (childNode.Name == name)
                        result = childNode;
                }

                if (result != null)
                {
                    schemaObject.Properties.Remove(result);
                    return true;
                }

                foreach (var childNode in schemaObject.Definitions)
                {
                    if (childNode.Name == name)
                        result = childNode;
                }

                if (result != null)
                {
                    schemaObject.Definitions.Remove(result);
                    return true;
                }
            }
            else if (parent is SchemaArray schemaArray)
            {
                if (schemaArray.Items.Name == name)
                    schemaArray.Items = null;
                else if (schemaArray.Items is SchemaObject tmpItem)
                {
                    foreach (var childNode in tmpItem.Properties)
                    {
                        if (childNode.Name == name)
                            result = childNode;
                    }
                    if (result != null)
                    {
                        tmpItem.Properties.Remove(result);
                        return true;
                    }

                    foreach (var childNode in tmpItem.Definitions)
                    {
                        if (childNode.Name == name)
                            result = childNode;
                    }

                    if (result != null)
                    {
                        tmpItem.Definitions.Remove(result);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Merge(ISchemaBase newProperty)
        {
            var tmpProperty = new SchemaArray();
            if (!Compare(newProperty))
                return false;

            if (newProperty is SchemaArray prop)
            {
                tmpProperty.Name = Name;
                tmpProperty.Id = Id;

                if (Description != prop.Description)
                    tmpProperty.Description = (Description + prop.Description).Trim();

                if (string.IsNullOrEmpty(Reference) || string.IsNullOrEmpty(prop.Reference))
                    tmpProperty.Reference = (Reference + prop.Reference).Trim();
                else
                    return false;

                foreach (var t1 in prop.Type)
                {
                    if (!Type.Contains(t1))
                        Type.Add(t1);
                }

                foreach (var t1 in prop.Examples)
                {
                    if (!Examples.Contains(t1))
                        Examples.Add(t1);
                }

                if (UniqueItemsOnly != null && prop.UniqueItemsOnly != null)
                {
                    tmpProperty.UniqueItemsOnly = (bool)UniqueItemsOnly || (bool)prop.UniqueItemsOnly;
                }

                tmpProperty.Items.Import(prop.Items);
                Import(tmpProperty);

                return true;
            }


            return false;
        }

        public bool Import(ISchemaBase newProperty)
        {
            if (newProperty is SchemaArray prop)
            {
                Name = prop.Name;
                Id = prop.Id;

                Description = prop.Description;
                Reference = prop.Reference;

                Type = new List<string>();
                foreach (var t2 in prop.Type)
                    Type.Add(t2);

                Examples = new List<string>();
                foreach (var t2 in prop.Examples)
                    Examples.Add(t2);

                UniqueItemsOnly = prop.UniqueItemsOnly;

                Items.Import(prop.Items);

                return true;
            }

            return false;
        }

        public bool Compare(ISchemaBase newProperty, bool baseOnly = false)
        {
            if (newProperty == null)
                return false;

            if (newProperty is SchemaArray prop)
            {
                if (Name != prop.Name)
                    return false;
                else if (Id != prop.Id)
                    return false;
                else if (!baseOnly)
                {
                    if (Description != prop.Description)
                        return false;
                    else if (Reference != prop.Reference)
                        return false;
                    else
                    {
                        if (Type.Count == prop.Type.Count)
                        {
                            foreach (var t1 in Type)
                            {
                                if (!prop.Type.Contains(t1))
                                    return false;
                            }
                        }
                        else
                            return false;

                        if (Examples.Count == prop.Examples.Count)
                        {
                            foreach (var t1 in Examples)
                            {
                                if (!prop.Examples.Contains(t1))
                                    return false;
                            }
                        }
                        else
                            return false;
                    }

                    if (UniqueItemsOnly != prop.UniqueItemsOnly)
                        return false;
                    else if (Items == null && prop.Items != null || Items != null && prop.Items == null)
                        return false;
                    else if (Items != null && prop.Items != null && !Items.Compare(prop.Items))
                        return false;
                }
            }

            return true;
        }

        public string ToJson(bool noSamples = true)
        {
            var text = new StringBuilder();

            if (!string.IsNullOrEmpty(Name))
                text.AppendLine($"\"{Name}\": {{");

            if (Id != null)
                text.AppendLine($"\"$id\": \"{Id}\",");

            if (Type != null)
            {
                var t = new StringBuilder();
                if (Type.Count > 1)
                {
                    t.Append('[');

                    foreach (var item in Type) t.Append($"\"{item}\",");

                    t.Append(']');
                }
                else if (Type.Count == 1)
                {
                    t.Append($"\"{Type[0]}\"");
                }
                else
                {
                    t.Append("\"\"");
                }

                text.AppendLine($"\"type\": {t},");
            }

            if (Description != null)
                text.AppendLine($"\"title\": \"{Description}\",");

            if (Reference != null)
                text.AppendLine($"\"$ref\": \"{Reference}\",");

            if (!noSamples && Examples != null && Examples.Count > 0)
            {
                var t = new StringBuilder();
                t.AppendLine("[");
                foreach (var item in Examples)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (Type?.Contains(ISchemaBase.JsonSchemaTypes.String) ?? false)
                            t.AppendLine($"\"{item}\",");
                        else
                            t.AppendLine($"{item},");
                    }
                }

                t.Append(']');
                text.AppendLine($"\"examples\": {t},");
            }

            if (UniqueItemsOnly != null)
                text.AppendLine($"\"uniqueItems\": {UniqueItemsOnly.ToString().ToLower()},");

            if (Items != null)
                text.AppendLine(Items.ToJson());

            text.AppendLine("}");

            return text.ToString();
        }
    }
}