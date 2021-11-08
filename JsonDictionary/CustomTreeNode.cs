using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using MessagePack;
using MessagePack.Resolvers;

using Newtonsoft.Json;

namespace JsonDictionaryCore
{
    public class CustomTreeNode
    {
        [JsonProperty("Node")] public string NodeText { get; set; }

        [IgnoreMember] public string Description { get; set; }

        [JsonIgnore] public string NodePath { get; set; }

        [JsonIgnore] public JsonProperty NodeTag { get; set; }

        [JsonProperty("Children", NullValueHandling = NullValueHandling.Ignore)]
        public List<CustomTreeNode> ChildNodes { get; set; }

        [JsonIgnore]
        public string JsonString =>
            JsonConvert.SerializeObject(this,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });

        [JsonIgnore]
        public byte[] MessagePack =>
            MessagePackSerializer.Serialize(this,
                MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray)
                    .WithResolver(ContractlessStandardResolverAllowPrivate.Instance));

        public CustomTreeNode()
        {
            NodeText = string.Empty;
            NodePath = string.Empty;
            Description = null;
            NodeTag = new JsonProperty();
            ChildNodes = new List<CustomTreeNode>();
        }

        public CustomTreeNode(TreeNode rootNode, Dictionary<string, string> nodeDescriptions = null)
        {
            var newNode = RunNodeMsgPack(rootNode);

            string nodeDescription = null;
            nodeDescriptions?.TryGetValue(rootNode.Name, out nodeDescription);

            NodeText = newNode.NodeText;
            Description = nodeDescription;
            NodePath = newNode.NodePath;
            NodeTag = newNode.NodeTag;
            ChildNodes = newNode.ChildNodes;
        }

        public CustomTreeNode(string text, string nodeDescription, string nodePath, JsonProperty nodeTag,
            List<CustomTreeNode> childNodes = null)
        {
            NodeText = text;
            Description = nodeDescription;
            NodePath = nodePath;
            NodeTag = nodeTag;

            if (childNodes != null)
                ChildNodes = childNodes;
        }

        public string GetJsonTree(TreeNode rootNode, Dictionary<string, string> nodeDescriptions = null)
        {
            if (rootNode == null)
                return null;

            var root = RunNode(rootNode, nodeDescriptions);

            return root.JsonString;
        }

        public CustomTreeNode RunNode(TreeNode rootNode, Dictionary<string, string> nodeDescriptions = null)
        {
            if (rootNode == null)
                return null;

            string nodeDesc = null;
            nodeDescriptions?.TryGetValue(rootNode.Name, out nodeDesc);
            var root = new CustomTreeNode(rootNode.Text, nodeDesc, rootNode.Name, (JsonProperty) rootNode.Tag);

            var children = new List<CustomTreeNode>();
            foreach (TreeNode node1 in rootNode.Nodes)
            {
                var child = RunNode(node1, nodeDescriptions);
                if (child != null)
                    children.Add(child);
            }

            if (children.Count > 0) root.ChildNodes = children;

            return root;
        }

        public byte[] GetMsgPackTree(TreeNode rootNode)
        {
            var newNode = RunNodeMsgPack(rootNode);

            NodeText = newNode.NodeText;
            NodePath = newNode.NodePath;
            NodeTag = newNode.NodeTag;
            ChildNodes = newNode.ChildNodes;

            return MessagePack;
        }

        public CustomTreeNode RunNodeMsgPack(TreeNode rootNode)
        {
            var parent = new CustomTreeNode
            {
                NodeText = rootNode.Text,
                NodePath = rootNode.Name,
                NodeTag = (JsonProperty) rootNode.Tag
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
                    MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray)
                        .WithResolver(ContractlessStandardResolverAllowPrivate.Instance));

                var tree = RestoreTree(data);

                return tree;
            }
        }

        public TreeNode RestoreTree(CustomTreeNode customNode)
        {
            var nodeOut = new TreeNode
            {
                Text = customNode.NodeText,
                Name = customNode.NodePath,
                Tag = customNode.NodeTag
            };

            foreach (var childNode in customNode.ChildNodes.Select(child => RestoreTree(child)))
                nodeOut.Nodes.Add(childNode);

            return nodeOut;
        }
    }
}
