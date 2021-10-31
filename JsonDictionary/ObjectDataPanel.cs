using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JsonDictionaryCore
{
    public partial class ObjectDataPanel : UserControl
    {
        private readonly SchemaTreeObject SourceSchemaObject;

        public ObjectDataPanel()
        {
            InitializeComponent();
        }

        public ObjectDataPanel(SchemaTreeObject dataObject)
        {
            SourceSchemaObject = dataObject;

            if (SourceSchemaObject == null) return;

            InitializeComponent();
            textBox_path.Text = SourceSchemaObject.Path;

            var t = new StringBuilder();
            if (SourceSchemaObject.Type != null && SourceSchemaObject.Type.Count > 0)
            {
                foreach (var e in SourceSchemaObject.Type)
                    t.Append(e + ";");
                textBox_type.Text = t.ToString();
            }

            textBox_description.Text = SourceSchemaObject.Description;

            if (SourceSchemaObject.Examples != null && SourceSchemaObject.Examples.Count > 0)
            {
                t = new StringBuilder();
                foreach (var e in SourceSchemaObject.Examples)
                    t.AppendLine(e);

                textBox_examples.Text = t.ToString();
            }

            textBox_reference.Text = SourceSchemaObject.Reference;
            textBox_additional.Text = SourceSchemaObject.AdditionalProperties.ToString();

            if (SourceSchemaObject.Required != null && SourceSchemaObject.Required.Count > 0)
            {
                t = new StringBuilder();
                foreach (var e in SourceSchemaObject.Required)
                    t.AppendLine(e);

                textBox_required.Text = t.ToString();
            }
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
            // common properties
            SourceSchemaObject.Type.Clear();
            SourceSchemaObject.Type.AddRange(textBox_type.Text.Split(';'));

            SourceSchemaObject.Description = textBox_description.Text;
            SourceSchemaObject.Reference = textBox_reference.Text;
            SourceSchemaObject.Examples.Clear();
            SourceSchemaObject.Examples.AddRange(textBox_examples.Text.Split(Environment.NewLine));

            // specific properties
            if (bool.TryParse(textBox_additional.Text, out var ap))
            {
                SourceSchemaObject.AdditionalProperties = ap;
            }
            else
            {
                SourceSchemaObject.AdditionalProperties = null;
            }

            SourceSchemaObject.Required.Clear();
            SourceSchemaObject.Required.AddRange(textBox_required.Text.Split(Environment.NewLine));
        }

        private void TextBox_type_Leave(object sender, EventArgs e)
        {
            var allowedTypes = new List<string>
            {
                "String",
                "Number",
                "Integer",
                "Boolean",
                "Null",
                "Array",
                "Object"
            };

            var valueList = new List<string>();
            valueList.AddRange(textBox_type.Text.Split(';'));
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

        private void TextBox_additional_Leave(object sender, EventArgs e)
        {
            var allowedTypes = new List<string>
            {
                "true",
                "false",
                "null",
            };

            var newValue = textBox_additional.Text.Trim().ToLower();
            if (!allowedTypes.Contains(newValue))
                textBox_additional.Text = string.Empty;
        }
    }
}
