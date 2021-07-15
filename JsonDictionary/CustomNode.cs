using System.Collections.Generic;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace JsonDictionaryCore
{
    public class CustomNode
    {
        public CustomNode()
        {
            this.Children = new List<CustomNode>();
        }

        public CustomNode(string value, List<CustomNode> children = null, bool isChecked = false)
        {
            Node = value;
            this.IsChecked = isChecked;
            if (children != null)
            {
                this.Children = children;
            }
        }
        [JsonProperty("value")]
        public string Node { get; set; }

        //[JsonProperty("isChecked")]
        [JsonIgnore]
        public bool IsChecked { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public List<CustomNode> Children { get; set; }

        [JsonIgnore]
        public string JSon
        {
            get
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }
        }

        public static string GetJsonTree(TreeNode rootNode)
        {
            List<CustomNode> parents = new List<CustomNode>();
            foreach (TreeNode node1 in rootNode.Nodes)
            {
                List<CustomNode> childs = CustomNode.RunNode(node1);
                parents.Add(new CustomNode(node1.Text, childs, rootNode.Checked));
            }
            var root = new CustomNode("root", parents, true);

            return root.JSon;
        }

        public static List<CustomNode> RunNode(TreeNode node)
        {
            List<CustomNode> nodeOut = new List<CustomNode>();
            foreach (TreeNode child in node.Nodes)
            {
                List<CustomNode> grandchild = RunNode(child);
                nodeOut.Add(new CustomNode(child.Text, grandchild, child.Checked));
            }
            return nodeOut;
        }

    }
}
