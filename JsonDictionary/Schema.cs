using System.Collections.Generic;
using System.Text;

namespace JsonDictionaryCore
{
    public interface ISchemaTreeBase
    {
        /// <summary>
        /// property name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// "$id" property
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// "type"
        /// </summary>
        public List<string> Type { get; set; }

        /// <summary>
        /// "description"(v07) or "title"(v04)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Reference to predefined set of properties "$ref"
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// "examples"
        /// </summary>
        public List<string> Examples { get; set; }

        /// <summary>
        /// find node by it's path
        /// </summary>
        /// <param name="path"></param>
        public ISchemaTreeBase FindNodeByPath(string path);

        /// <summary>
        /// Deep merge two objects
        /// </summary>
        /// <param name="newProperty"></param>
        /// <returns></returns>
        public bool Merge(ISchemaTreeBase newProperty);

        /// <summary>
        /// Replace all object content with new content
        /// </summary>
        /// <param name="newProperty"></param>
        /// <returns></returns>
        public bool Import(ISchemaTreeBase newProperty);

        /// <summary>
        /// Deep compare with object
        /// </summary>
        /// <param name="newProperty"></param>
        /// <param name="baseOnly"></param>
        /// <returns></returns>
        public bool Compare(ISchemaTreeBase newProperty, bool baseOnly = false);

        /// <summary>
        /// Returns JSON text of the object
        /// </summary>
        /// <returns></returns>
        public string ToJson(bool noSamples = true);
    }

    public class SchemaTreeProperty : ISchemaTreeBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> Type { get; set; } = new List<string>();
        public string Description { get; set; }
        public string Reference { get; set; }
        public List<string> Examples { get; set; } = new List<string>();

        // specific properties
        public string Default { get; set; } // "default"

        public string Pattern { get; set; } // "pattern"

        public List<string> Enum { get; set; } = new List<string>(); // "enum"

        public SchemaTreeProperty() { }

        public SchemaTreeProperty(string name)
        {
            Name = name;
        }

        public ISchemaTreeBase FindNodeByPath(string path)
        {
            if (Path == path) return this;

            return null;
        }

        public bool Merge(ISchemaTreeBase newProperty)
        {
            var tmpProperty = new SchemaTreeProperty();
            if (!this.Compare(newProperty)) return false;

            if (newProperty is SchemaTreeProperty prop)
            {
                tmpProperty.Name = this.Name;
                tmpProperty.Path = this.Path;

                if (this.Description != prop.Description)
                    tmpProperty.Description = (this.Description + prop.Description).Trim();

                if (string.IsNullOrEmpty(this.Reference) || string.IsNullOrEmpty(prop.Reference))
                    tmpProperty.Reference = (this.Reference + prop.Reference).Trim();
                else return false;

                foreach (var t1 in prop.Type)
                {
                    if (!this.Type.Contains(t1))
                        this.Type.Add(t1);
                }

                foreach (var t1 in prop.Examples)
                {
                    if (!this.Examples.Contains(t1))
                        this.Examples.Add(t1);
                }

                foreach (var t1 in prop.Enum)
                {
                    if (!this.Enum.Contains(t1))
                        this.Enum.Add(t1);
                }

                if (string.IsNullOrEmpty(this.Default) || string.IsNullOrEmpty(prop.Default))
                    tmpProperty.Default = (this.Default + prop.Default).Trim();
                else return false;

                if (string.IsNullOrEmpty(this.Pattern) || string.IsNullOrEmpty(prop.Pattern))
                    tmpProperty.Pattern = (this.Reference + prop.Pattern).Trim();
                else return false;

                this.Import(tmpProperty);

                return true;
            }

            return false;
        }

        public bool Import(ISchemaTreeBase newProperty)
        {
            if (newProperty is SchemaTreeProperty prop)
            {
                Name = prop.Name;
                Path = prop.Path;

                Description = prop.Description;
                Reference = prop.Reference;

                Type = new List<string>();
                foreach (var t2 in prop.Type)
                {
                    Type.Add(t2);
                }

                Examples = new List<string>();
                foreach (var t2 in prop.Examples)
                {
                    Examples.Add(t2);
                }

                Default = prop.Default;
                Pattern = prop.Pattern;

                Enum = new List<string>();
                foreach (var t2 in prop.Enum)
                {
                    Enum.Add(t2);
                }

                return true;
            }

            return false;
        }

        public bool Compare(ISchemaTreeBase newProperty, bool baseOnly = false)
        {
            var notSame = false;
            if (newProperty is SchemaTreeProperty prop)
            {
                if (Name != prop.Name) notSame = true;
                else if (Path != prop.Path) notSame = true;
                else if (!baseOnly)
                {
                    if (Description != prop.Description) notSame = true;
                    else if (Reference != prop.Reference) notSame = true;
                    else
                    {
                        if (Type.Count == prop.Type.Count)
                        {
                            foreach (var t1 in Type)
                            {
                                if (!prop.Type.Contains(t1))
                                {
                                    notSame = true;
                                    break;
                                }
                            }
                        }
                        else notSame = true;

                        if (!notSame && Examples.Count == prop.Examples.Count)
                        {
                            foreach (var t1 in Examples)
                            {
                                if (!prop.Examples.Contains(t1))
                                {
                                    notSame = true;
                                    break;
                                }
                            }
                        }
                        else notSame = true;
                    }

                    if (!notSame)
                    {
                        if (Default != prop.Default) notSame = true;
                        else if (Pattern != prop.Pattern) notSame = true;
                        else if (Enum.Count == prop.Enum.Count)
                        {
                            foreach (var t1 in Enum)
                            {
                                if (!prop.Enum.Contains(t1))
                                {
                                    notSame = true;
                                    break;
                                }
                            }
                        }
                        else notSame = true;
                    }
                }
            }
            else notSame = true;

            return notSame;
        }

        public string ToJson(bool noSamples = true)
        {
            var text = new StringBuilder();

            if (Name != null)
                text.AppendLine($"\"{Name}\": {{");

            if (Path != null)
                text.AppendLine($"\"$id\": \"{Path}\",");

            if (Type != null)
            {
                var t = new StringBuilder();
                if (Type.Count > 1)
                {
                    t.Append("[");

                    foreach (var item in Type)
                    {
                        t.Append($"\"{item}\",");
                    }
                    t.Append("]");
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
                text.AppendLine($"\"title\": \"{Description.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"")}\",");

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
                        if (Type?.Contains("string") ?? false)
                            t.AppendLine($"\"{item.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"")}\",");
                        else
                            t.AppendLine($"{item.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"")},");
                    }
                }
                t.Append("]");
                text.AppendLine($"\"examples\": {t},");
            }

            if (Default != null)
            {
                if (Type?.Contains("string") ?? false)
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
                        if (Type?.Contains("string") ?? false)
                            t.AppendLine($"\"{item}\",");
                        else
                            t.AppendLine($"{item},");
                    }
                }

                if (Type != null)
                {
                    if (Type.Contains("boolean"))
                    {
                        t.AppendLine("true,");
                        t.AppendLine("false,");
                    }

                    if (Type.Contains("null"))
                    {
                        t.AppendLine("null,");
                    }
                }

                t.Append("]");
                text.AppendLine($"\"enum\": {t},");
            }

            text.AppendLine("}");

            return text.ToString();
        }
    }

    public class SchemaTreeObject : ISchemaTreeBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> Type { get; set; } = new List<string>();
        public string Description { get; set; }
        public string Reference { get; set; }
        public List<string> Examples { get; set; } = new List<string>();

        // specific properties
        public string SchemaName { get; set; } // "$schema"

        public bool? AdditionalProperties { get; set; } = false; // "additionalProperties"

        public List<string> Required { get; set; } = new List<string>(); // "required"

        public List<ISchemaTreeBase> Properties { get; set; } = new List<ISchemaTreeBase>(); // "properties"

        public List<ISchemaTreeBase> Definitions { get; set; } = new List<ISchemaTreeBase>(); // "definitions"

        public SchemaTreeObject() { }

        public SchemaTreeObject(string name)
        {
            Name = name;
        }

        public ISchemaTreeBase FindNodeByPath(string path)
        {
            if (Path == path) return this;

            foreach (var property in Properties)
            {
                var foundProperty = property.FindNodeByPath(path);
                if (foundProperty != null)
                    return foundProperty;
            }

            return null;
        }

        public bool Merge(ISchemaTreeBase newProperty)
        {
            var tmpProperty = new SchemaTreeObject();
            if (!this.Compare(newProperty)) return false;

            if (newProperty is SchemaTreeObject prop)
            {
                tmpProperty.Name = this.Name;
                tmpProperty.Path = this.Path;

                if (this.Description != prop.Description)
                    tmpProperty.Description = (this.Description + prop.Description).Trim();

                if (string.IsNullOrEmpty(this.Reference) || string.IsNullOrEmpty(prop.Reference))
                    tmpProperty.Reference = (this.Reference + prop.Reference).Trim();
                else return false;

                foreach (var p in this.Type)
                {
                    tmpProperty.Type.Add(p);
                }
                foreach (var t1 in prop.Type)
                {
                    if (!tmpProperty.Type.Contains(t1))
                        tmpProperty.Type.Add(t1);
                }

                foreach (var p in this.Examples)
                {
                    tmpProperty.Examples.Add(p);
                }
                foreach (var t1 in prop.Examples)
                {
                    if (!tmpProperty.Examples.Contains(t1))
                        tmpProperty.Examples.Add(t1);
                }

                foreach (var p in this.Required)
                {
                    tmpProperty.Required.Add(p);
                }
                foreach (var t1 in prop.Required)
                {
                    if (!tmpProperty.Required.Contains(t1))
                        tmpProperty.Required.Add(t1);
                }

                foreach (var p in this.Properties)
                {
                    tmpProperty.Properties.Add(p);
                }
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
                            ISchemaTreeBase newProp;
                            if (p2 is SchemaTreeArray) newProp = new SchemaTreeArray();
                            else if (p2 is SchemaTreeObject) newProp = new SchemaTreeObject();
                            else newProp = new SchemaTreeProperty();

                            newProp.Import(p2);
                            tmpProperty.Properties.Add(newProp);
                        }
                    }
                }

                this.Import(tmpProperty);

                return true;
            }

            return false;
        }

        public bool Import(ISchemaTreeBase newProperty)
        {
            if (newProperty is SchemaTreeObject prop)
            {
                Name = prop.Name;
                Path = prop.Path;

                Description = prop.Description;
                Reference = prop.Reference;

                Type = new List<string>();
                foreach (var t2 in prop.Type)
                {
                    Type.Add(t2);
                }

                Examples = new List<string>();
                foreach (var t2 in prop.Examples)
                {
                    Examples.Add(t2);
                }

                AdditionalProperties = prop.AdditionalProperties;

                foreach (var t2 in prop.Required)
                {
                    Required.Add(t2);
                }

                // process Properties
                Properties = new List<ISchemaTreeBase>();
                foreach (var p2 in prop.Properties)
                {
                    ISchemaTreeBase newProp;
                    if (p2 is SchemaTreeArray) newProp = new SchemaTreeArray();
                    else if (p2 is SchemaTreeObject) newProp = new SchemaTreeObject();
                    else newProp = new SchemaTreeProperty();

                    newProp.Import(p2);
                    Properties.Add(newProp);
                }

                return true;
            }

            return false;
        }

        // TODO: redesign
        public bool Compare(ISchemaTreeBase newProperty, bool baseOnly = false)
        {
            var notSame = false;
            if (newProperty is SchemaTreeObject prop)
            {
                if (Name != prop.Name) notSame = true;
                else if (Path != prop.Path) notSame = true;
                else if (!baseOnly)
                {
                    if (Description != prop.Description) notSame = true;
                    else if (Reference != prop.Reference) notSame = true;
                    else
                    {
                        if (Type.Count == prop.Type.Count)
                        {
                            foreach (var t1 in Type)
                            {
                                if (!prop.Type.Contains(t1))
                                {
                                    notSame = true;
                                    break;
                                }
                            }
                        }
                        else notSame = true;

                        if (!notSame && Examples.Count == prop.Examples.Count)
                        {
                            foreach (var t1 in Examples)
                            {
                                if (!prop.Examples.Contains(t1))
                                {
                                    notSame = true;
                                    break;
                                }
                            }
                        }
                        else notSame = true;
                    }

                    if (!notSame)
                    {
                        if (AdditionalProperties != prop.AdditionalProperties) notSame = true;
                        else if (Required.Count == prop.Required.Count)
                        {
                            foreach (var t1 in Required)
                            {
                                if (!prop.Required.Contains(t1))
                                {
                                    notSame = true;
                                    break;
                                }
                            }
                        }
                        else notSame = true;

                        if (!notSame && Properties.Count == prop.Properties.Count)
                        {
                            foreach (var t1 in Properties)
                            {
                                foreach (var t2 in prop.Properties)
                                {
                                    if (t2.Name == t1.Name && !t1.Compare(t2))
                                    {
                                        notSame = true;
                                        break;
                                    }
                                }
                                if (notSame == true) break;
                            }
                        }
                        else notSame = true;
                    }
                }
            }
            else notSame = true;

            return notSame;
        }

        public string ToJson(bool noSamples = true)
        {
            var text = new StringBuilder();

            if (string.IsNullOrEmpty(Name) || Name == "#")
                text.Append("{");
            else
                text.AppendLine($"\"{Name}\": {{");

            if (SchemaName != null)
                text.AppendLine($"\"$schema\": \"{SchemaName}\",");

            if (Path != null)
                text.AppendLine($"\"$id\": \"{Path}\",");

            if (Type != null)
            {
                var t = new StringBuilder();
                if (Type.Count > 1)
                {
                    t.Append("[");

                    foreach (var item in Type)
                    {
                        t.Append($"\"{item}\",");
                    }
                    t.Append("]");
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
                text.AppendLine($"\"title\": \"{Description.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"")}\",");

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
                        if (Type?.Contains("string") ?? false)
                            t.AppendLine($"\"{item.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"")}\",");
                        else
                            t.AppendLine($"{item.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"")},");
                    }
                }
                t.Append("]");
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
                t.Append("]");
                text.AppendLine($"\"required\": {t},");
            }

            if (AdditionalProperties != null)
                text.AppendLine($"\"additionalProperties\": {AdditionalProperties.ToString().ToLower()},");

            if (Properties != null && Properties.Count > 0)
            {
                text.AppendLine("\"properties\": {");
                foreach (var prop in Properties)
                {
                    text.AppendLine(prop.ToJson() + ",");
                }
                text.AppendLine("}");
            }

            if (Definitions != null && Definitions.Count > 0)
            {
                text.AppendLine("\"definitions\": {");
                foreach (var prop in Definitions)
                {
                    text.AppendLine(prop.ToJson() + ",");
                }
                text.AppendLine("}");
            }

            text.AppendLine("}");

            return text.ToString();
        }
    }

    public class SchemaTreeArray : ISchemaTreeBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> Type { get; set; } = new List<string>();
        public string Description { get; set; }
        public string Reference { get; set; }
        public List<string> Examples { get; set; } = new List<string>();

        // specific properties
        public bool? UniqueItemsOnly { get; set; } // "uniqueItems"

        public ISchemaTreeBase Items { get; set; } // "items"

        public SchemaTreeArray() { }

        public SchemaTreeArray(string name)
        {
            Name = name;
        }

        public ISchemaTreeBase FindNodeByPath(string path)
        {
            if (Path == path) return this;

            if (Items is SchemaTreeProperty schemaProperty)
            {
                return schemaProperty.FindNodeByPath(path);
            }
            else if (Items is SchemaTreeObject schemaObject)
            {
                foreach (var property in schemaObject.Properties)
                {
                    var foundProperty = property.FindNodeByPath(path);
                    if (foundProperty != null)
                        return foundProperty;
                }
            }
            else if (Items is SchemaTreeArray schemaArray)
            {
                return schemaArray.FindNodeByPath(path);
            }

            return null;
        }

        public bool Merge(ISchemaTreeBase newProperty)
        {
            var tmpProperty = new SchemaTreeArray();
            if (!this.Compare(newProperty)) return false;

            if (newProperty is SchemaTreeArray prop)
            {
                tmpProperty.Name = this.Name;
                tmpProperty.Path = this.Path;

                if (this.Description != prop.Description)
                    tmpProperty.Description = (this.Description + prop.Description).Trim();

                if (string.IsNullOrEmpty(this.Reference) || string.IsNullOrEmpty(prop.Reference))
                    tmpProperty.Reference = (this.Reference + prop.Reference).Trim();
                else return false;

                foreach (var t1 in prop.Type)
                {
                    if (!this.Type.Contains(t1))
                        this.Type.Add(t1);
                }

                foreach (var t1 in prop.Examples)
                {
                    if (!this.Examples.Contains(t1))
                        this.Examples.Add(t1);
                }

                if (this.UniqueItemsOnly != null && prop.UniqueItemsOnly != null)
                    tmpProperty.UniqueItemsOnly = (bool)this.UniqueItemsOnly || (bool)prop.UniqueItemsOnly;

                tmpProperty.Items.Import(prop.Items);
                this.Import(tmpProperty);

                return true;
            }




            return false;
        }

        public bool Import(ISchemaTreeBase newProperty)
        {
            if (newProperty is SchemaTreeArray prop)
            {
                Name = prop.Name;
                Path = prop.Path;

                Description = prop.Description;
                Reference = prop.Reference;

                Type = new List<string>();
                foreach (var t2 in prop.Type)
                {
                    Type.Add(t2);
                }

                Examples = new List<string>();
                foreach (var t2 in prop.Examples)
                {
                    Examples.Add(t2);
                }

                UniqueItemsOnly = prop.UniqueItemsOnly;

                Items.Import(prop.Items);

                return true;
            }

            return false;
        }

        public bool Compare(ISchemaTreeBase newProperty, bool baseOnly = false)
        {
            var notSame = false;
            if (newProperty == null) return false;

            if (newProperty is SchemaTreeArray prop)
            {
                if (Name != prop.Name) notSame = true;
                else if (Path != prop.Path) notSame = true;
                else if (!baseOnly)
                {
                    if (Description != prop.Description) notSame = true;
                    else if (Reference != prop.Reference) notSame = true;
                    else
                    {
                        if (Type.Count == prop.Type.Count)
                        {
                            foreach (var t1 in Type)
                            {
                                if (!prop.Type.Contains(t1))
                                {
                                    notSame = true;
                                    break;
                                }
                            }
                        }
                        else notSame = true;

                        if (!notSame)
                        {
                            if (Examples.Count == prop.Examples.Count)
                            {
                                foreach (var t1 in Examples)
                                {
                                    if (!prop.Examples.Contains(t1))
                                    {
                                        notSame = true;
                                        break;
                                    }
                                }
                            }
                            else notSame = true;
                        }
                    }

                    if (!notSame)
                    {
                        if (UniqueItemsOnly != prop.UniqueItemsOnly) notSame = true;
                        else if (Items == null && prop.Items != null
                            || Items != null && prop.Items == null) notSame = true;
                        else if (!Items.Compare(prop.Items)) notSame = true;
                    }
                }
            }

            return notSame;
        }

        public string ToJson(bool noSamples = true)
        {
            var text = new StringBuilder();

            if (!string.IsNullOrEmpty(Name))
                text.AppendLine($"\"{Name}\": {{");

            if (Path != null)
                text.AppendLine($"\"$id\": \"{Path}\",");

            if (Type != null)
            {
                var t = new StringBuilder();
                if (Type.Count > 1)
                {
                    t.Append("[");

                    foreach (var item in Type)
                    {
                        t.Append($"\"{item}\",");
                    }
                    t.Append("]");
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
                text.AppendLine($"\"title\": \"{Description.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"")}\",");

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
                        if (Type?.Contains("string") ?? false)
                            t.AppendLine($"\"{item.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"")}\",");
                        else
                            t.AppendLine($"{item.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"")},");
                    }
                }
                t.Append("]");
                text.AppendLine($"\"examples\": {t},");
            }

            if (UniqueItemsOnly != null)
                text.AppendLine($"\"uniqueItems\": {UniqueItemsOnly.ToString().ToLower()},");

            if (Items != null)
            {
                text.AppendLine(Items.ToJson());
            }

            text.AppendLine("}");

            return text.ToString();
        }
    }
}