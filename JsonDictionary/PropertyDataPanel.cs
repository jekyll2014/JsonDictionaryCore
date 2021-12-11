using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JsonDictionaryCore
{
    public partial class PropertyDataPanel : UserControl
    {
        private readonly SchemaProperty _sourceSchemaObject;

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

        public string ObjectDefaultText => textBox_default.Text;

        public Color ObjectDefaultBackColor
        {
            get => textBox_default.BackColor;
            set => textBox_default.BackColor = value;
        }

        public string ObjectEnumText => textBox_enum.Text;

        public Color ObjectEnumBackColor
        {
            get => textBox_enum.BackColor;
            set => textBox_enum.BackColor = value;
        }

        public PropertyDataPanel()
        {
            InitializeComponent();
        }

        //public PropertyDataPanel(SchemaProperty dataObject, string treePath)
        public PropertyDataPanel(SchemaProperty dataObject)
        {
            _sourceSchemaObject = dataObject;

            if (_sourceSchemaObject == null)
                return;

            InitializeComponent();
            textBox_path.Text = _sourceSchemaObject.Id;

            /*if (!string.IsNullOrEmpty(treePath))
            {
                treePath = treePath.Replace('\\', '/');

                if (treePath.EndsWith("/properties"))
                {
                    var pos = treePath.LastIndexOf("/properties", StringComparison.OrdinalIgnoreCase);
                    treePath = treePath.Substring(0, pos);
                }
            }*/

            var t = new StringBuilder();
            if (_sourceSchemaObject.Type != null && _sourceSchemaObject.Type.Count > 0)
            {
                foreach (var e in _sourceSchemaObject.Type)
                    t.Append(e + ";");

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
            textBox_default.Text = _sourceSchemaObject.Default;
            textBox_pattern.Text = _sourceSchemaObject.Pattern;

            if (_sourceSchemaObject.Enum != null && _sourceSchemaObject.Enum.Count > 0)
            {
                t = new StringBuilder();
                foreach (var e in _sourceSchemaObject.Enum)
                    t.AppendLine(e);

                textBox_enum.Text = t.ToString();
            }
        }

        public event EventHandler<RefLinkClickEventArgs> OnRefClick;

        private void TextBox_reference_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var eventDelegate = OnRefClick;

            if (eventDelegate != null)
                eventDelegate(this, new RefLinkClickEventArgs(textBox_reference.Text));
        }

        // create name/Path change recursive method
        private void Button_save_Click(object sender, EventArgs e)
        {
            // common properties
            _sourceSchemaObject.Type.Clear();
            _sourceSchemaObject.Type.AddRange(textBox_type.Text.Split(';'));

            _sourceSchemaObject.Description = textBox_description.Text;
            _sourceSchemaObject.Reference = textBox_reference.Text;
            _sourceSchemaObject.Examples.Clear();
            _sourceSchemaObject.Examples.AddRange(textBox_examples.Text.Split(Environment.NewLine));

            // specific properties
            _sourceSchemaObject.Pattern = textBox_pattern.Text;
            _sourceSchemaObject.Default = textBox_default.Text;

            _sourceSchemaObject.Enum.Clear();
            _sourceSchemaObject.Enum.AddRange(textBox_enum.Text.Split(Environment.NewLine));
        }

        private void TextBox_type_Leave(object sender, EventArgs e)
        {
            var valueList = new List<string>();
            valueList.AddRange(textBox_type.Text.ToLower().Split(';'));
            for (var i = 0; i < valueList.Count; i++)
            {
                if (!ISchemaBase.AllowedDataTypes.Contains(valueList[i]))
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
    }
}
