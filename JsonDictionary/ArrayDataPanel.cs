using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JsonDictionaryCore
{
    public partial class ArrayDataPanel : UserControl
    {
        private readonly SchemaTreeArray _sourceSchemaObject;

        public string ObjectPathText => textBox_path.Text;

        public Color ObjectPathBackColor
        {
            get => textBox_path.BackColor;
            set => textBox_path.BackColor = value;
        }

        public string ObjectTypeText => textBox_type.Text;

        public Color ObjectTypeBackColor
        {
            get => textBox_type.BackColor;
            set => textBox_type.BackColor = value;
        }

        public string ObjectDescText => textBox_description.Text;

        public Color ObjectDescBackColor
        {
            get => textBox_description.BackColor;
            set => textBox_description.BackColor = value;
        }

        public string ObjectRefText => textBox_reference.Text;

        public Color ObjectRefBackColor
        {
            get => textBox_reference.BackColor;
            set => textBox_reference.BackColor = value;
        }

        public string ObjectUniqueText => textBox_unique.Text;

        public Color ObjectUniqueBackColor
        {
            get => textBox_unique.BackColor;
            set => textBox_unique.BackColor = value;
        }

        public ArrayDataPanel()
        {
            InitializeComponent();
        }

        public ArrayDataPanel(SchemaTreeArray dataObject, string treePath)
        {
            _sourceSchemaObject = dataObject;

            if (_sourceSchemaObject == null) return;

            InitializeComponent();
            textBox_path.Text = _sourceSchemaObject.Id;

            if (!string.IsNullOrEmpty(treePath))
            {
                treePath = treePath.Replace('\\', '/');
                if (treePath.EndsWith("/properties"))
                {
                    var pos = treePath.LastIndexOf("/properties", StringComparison.Ordinal);
                    treePath = treePath.Substring(0, pos);
                }
            }

            var t = new StringBuilder();
            if (_sourceSchemaObject.Type != null && _sourceSchemaObject.Type.Count > 0)
            {
                foreach (var e in _sourceSchemaObject.Type)
                    t.Append(e + "; ");

                textBox_type.Text = t.ToString();
            }

            textBox_description.Text = _sourceSchemaObject.Description;

            if (_sourceSchemaObject.Examples != null && _sourceSchemaObject.Examples.Count > 0)
            {
                t = new StringBuilder();
                foreach (var e in _sourceSchemaObject.Examples)
                    t.AppendLine(e);

                textBox_examples.Text = t.ToString();
            }

            textBox_reference.Text = _sourceSchemaObject.Reference;
            textBox_unique.Text = _sourceSchemaObject.UniqueItemsOnly.ToString();
        }

        public event EventHandler<RefLinkClickEventArgs> OnRefClick;

        private void TextBox_reference_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var eventDelegate = OnRefClick;

            if (eventDelegate != null)
                eventDelegate(this, new RefLinkClickEventArgs(textBox_reference.Text));
        }

        private void Button_save_Click(object sender, EventArgs e)
        {
            _sourceSchemaObject.Type.Clear();
            _sourceSchemaObject.Type.AddRange(textBox_type.Text.Split(';'));

            _sourceSchemaObject.Description = textBox_description.Text;
            _sourceSchemaObject.Reference = textBox_reference.Text;
            _sourceSchemaObject.Examples.Clear();
            _sourceSchemaObject.Examples.AddRange(textBox_examples.Text.Split(Environment.NewLine));

            // specific properties
            if (bool.TryParse(textBox_unique.Text, out var ap))
                _sourceSchemaObject.UniqueItemsOnly = ap;
            else
                _sourceSchemaObject.UniqueItemsOnly = null;
        }

        private void TextBox_type_Leave(object sender, EventArgs e)
        {
            var allowedTypes = new List<string>
            {
                "string",
                "number",
                "integer",
                "boolean",
                "null",
                "array",
                "object"
            };

            var valueList = new List<string>();
            valueList.AddRange(textBox_type.Text.ToLower().Split(';'));
            for (var i = 0; i < valueList.Count; i++)
            {
                if (!allowedTypes.Contains(valueList[i]))
                {
                    valueList.RemoveAt(i);
                    i--;
                }
            }

            var t = new StringBuilder();
            foreach (var l in valueList)
                t.Append(l + "; ");

            textBox_type.Text = t.ToString();
        }

        private void TextBox_unique_Leave(object sender, EventArgs e)
        {
            var allowedTypes = new List<string>
            {
                "true",
                "false",
                "null"
            };

            var newValue = textBox_unique.Text.Trim().ToLower();

            if (!allowedTypes.Contains(newValue))
                textBox_unique.Text = string.Empty;
        }
    }
}
