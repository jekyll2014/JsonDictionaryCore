using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using MessagePack;
using MessagePack.Resolvers;

using Newtonsoft.Json;

namespace JsonDictionaryCore
{
    public class CustomTreeNode
    {
        [JsonProperty("Node")]
        public string NodeText { get; set; }

        [MessagePack.IgnoreMember]
        public string Description { get; set; }

        //[JsonProperty("isChecked")]
        [JsonIgnore]
        public string NodePath { get; set; }

        [JsonIgnore]
        public JsonProperty NodeTag { get; set; }

        //[JsonIgnore]
        [JsonProperty("Children", NullValueHandling = NullValueHandling.Ignore)]
        public List<CustomTreeNode> ChildNodes { get; set; }

        [JsonIgnore]
        public string JsonString
        {
            get
            {
                return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
        }

        [JsonIgnore]
        public byte[] MessagePack
        {
            get
            {
                return MessagePackSerializer.Serialize(this, MessagePackSerializerOptions
                    .Standard
                    .WithCompression(MessagePackCompression.Lz4BlockArray)
                    .WithResolver(ContractlessStandardResolverAllowPrivate.Instance));
            }
        }

        public CustomTreeNode()
        {
            this.NodeText = string.Empty;
            this.NodePath = string.Empty;
            this.Description = null;
            this.NodeTag = new JsonProperty();
            this.ChildNodes = new List<CustomTreeNode>();
        }

        public CustomTreeNode(TreeNode rootNode, Dictionary<string, string> nodeDescriptions = null)
        {
            var newNode = RunNodeMsgPack(rootNode);

            string nodeDescription = null;
            nodeDescriptions?.TryGetValue(rootNode.Name, out nodeDescription);

            this.NodeText = newNode.NodeText;
            this.Description = nodeDescription;
            this.NodePath = newNode.NodePath;
            this.NodeTag = newNode.NodeTag;
            this.ChildNodes = newNode.ChildNodes;
        }

        public CustomTreeNode(string text, string nodeDescription, string nodePath, JsonProperty nodeTag, List<CustomTreeNode> childNodes = null)
        {
            NodeText = text;
            Description = nodeDescription;
            NodePath = nodePath;
            NodeTag = nodeTag;

            if (childNodes != null)
            {
                this.ChildNodes = childNodes;
            }
        }

        public string GetJsonTree(TreeNode rootNode, Dictionary<string, string> nodeDescriptions = null)
        {
            if (rootNode == null || rootNode.Nodes.Count <= 0)
                return null;

            var parents = new List<CustomTreeNode>();
            foreach (TreeNode node1 in rootNode.Nodes)
            {
                List<CustomTreeNode> children = this.RunNode(node1, nodeDescriptions);
                if (children != null)
                {
                    string nodeDescription = null;
                    nodeDescriptions?.TryGetValue(node1.Name, out nodeDescription);
                    parents.Add(new CustomTreeNode(node1.Text, nodeDescription, node1.Name, (JsonProperty)node1.Tag, children));
                }
            }

            string nodeDesc = null;
            nodeDescriptions?.TryGetValue(rootNode.Name, out nodeDesc);
            var root = new CustomTreeNode(rootNode.Text, nodeDesc, rootNode.Name, (JsonProperty)rootNode.Tag, parents);

            return root.JsonString;
        }

        public List<CustomTreeNode> RunNode(TreeNode node, Dictionary<string, string> nodeDescriptions = null)
        {
            if (node == null || node.Nodes.Count <= 0)
                return null;

            var nodeOut = new List<CustomTreeNode>();
            foreach (TreeNode child in node.Nodes)
            {
                var grandchild = RunNode(child, nodeDescriptions);
                if (grandchild != null)
                {
                    string nodeDescription = null;
                    nodeDescriptions?.TryGetValue(child.Name, out nodeDescription);
                    nodeOut.Add(new CustomTreeNode(child.Text, nodeDescription, child.Name, (JsonProperty)child.Tag, grandchild));
                }
            }
            return nodeOut;
        }

        public byte[] GetMsgPackTree(TreeNode rootNode)
        {
            var newNode = RunNodeMsgPack(rootNode);

            this.NodeText = newNode.NodeText;
            this.NodePath = newNode.NodePath;
            this.NodeTag = newNode.NodeTag;
            this.ChildNodes = newNode.ChildNodes;

            return MessagePack;
        }

        public CustomTreeNode RunNodeMsgPack(TreeNode rootNode)
        {
            var parent = new CustomTreeNode()
            {
                NodeText = rootNode.Text,
                NodePath = rootNode.Name,
                NodeTag = (JsonProperty)rootNode.Tag
            };

            foreach (TreeNode node1 in rootNode.Nodes)
            {
                var newChild = RunNodeMsgPack(node1);
                parent.ChildNodes.Add(newChild);
            }

            return parent;
        }

        public TreeNode GetTreeFromMsgPack(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            using (Stream file = File.Open(fileName, FileMode.Open))
            {
                var data = MessagePackSerializer.Deserialize<CustomTreeNode>(file,
                    MessagePackSerializerOptions
                        .Standard
                        .WithCompression(MessagePackCompression.Lz4BlockArray)
                        .WithResolver(ContractlessStandardResolverAllowPrivate.Instance));

                var tree = RestoreTree(data);

                return tree;
            }
        }

        public TreeNode RestoreTree(CustomTreeNode customNode)
        {
            var nodeOut = new TreeNode()
            {
                Text = customNode.NodeText,
                Name = customNode.NodePath,
                Tag = customNode.NodeTag,
            };

            foreach (var child in customNode.ChildNodes)
            {
                var childNode = RestoreTree(child);
                nodeOut.Nodes.Add(childNode);
            }

            return nodeOut;
        }
    }
}
