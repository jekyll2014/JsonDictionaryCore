using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using JsonDictionaryCore.Panels;
using JsonDictionaryCore.SchemaGenerator;

namespace JsonDictionaryCore.Panels
{
    public partial class ObjectDataPanel : UserControl
    {
        private readonly SchemaObject _sourceSchemaObject;

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

        public string ObjectAdditionalText => textBox_additional.Text;

        public Color ObjectAdditionalBackColor
        {
            get => textBox_additional.BackColor;
            set => textBox_additional.BackColor = value;
        }

        public string ObjectRequiredText => textBox_required.Text;

        public Color ObjectRequiredBackColor
        {
            get => textBox_required.BackColor;
            set => textBox_required.BackColor = value;
        }

        public ObjectDataPanel()
        {
            InitializeComponent();
        }

        //public ObjectDataPanel(SchemaObject dataObject, string treePath)
        public ObjectDataPanel(SchemaObject dataObject)
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
            textBox_additional.Text = _sourceSchemaObject.AdditionalProperties.ToString();

            if (_sourceSchemaObject.Required != null && _sourceSchemaObject.Required.Count > 0)
            {
                t = new StringBuilder();
                foreach (var e in _sourceSchemaObject.Required)
                    t.AppendLine(e);

                textBox_required.Text = t.ToString();
            }
        }

        public event EventHandler<RefLinkClickEventArgs> OnRefClick;

        private void TextBox_reference_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnRefClick?.Invoke(this, new RefLinkClickEventArgs(textBox_reference.Text));
        }

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
            if (bool.TryParse(textBox_additional.Text, out var ap))
                _sourceSchemaObject.AdditionalProperties = ap;
            else
                _sourceSchemaObject.AdditionalProperties = null;

            _sourceSchemaObject.Required.Clear();
            _sourceSchemaObject.Required.AddRange(textBox_required.Text.Split(Environment.NewLine));
        }

        private void TextBox_type_Leave(object sender, EventArgs e)
        {
            var valueList = new List<string>();
            valueList.AddRange(textBox_type.Text.ToLower().Split(';'));
            for (var i = 0; i < valueList.Count; i++)
            {
                if (!ISchemaBase.SchemaDataTypes.Contains(valueList[i]))
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

        private void TextBox_additional_Leave(object sender, EventArgs e)
        {
            var newValue = textBox_additional.Text.Trim().ToLower();

            if (!ISchemaBase.SchemaBoolTypes.Contains(newValue))
                textBox_additional.Text = string.Empty;
        }
    }
}
