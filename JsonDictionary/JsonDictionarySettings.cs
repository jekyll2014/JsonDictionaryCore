using Newtonsoft.Json;

using static JsonDictionaryCore.Form1;

namespace JsonDictionaryCore
{
    public struct ProcessingOptions
    {
        public string ContentType; // file type name
        public string[] ParentNames; // parent node name
        public string ItemName; // property name to search
        public string MoveToPath; // make this a parent path
    }

    public struct WinPosition
    {
        public int WinX;
        public int WinY;
        public int WinW;
        public int WinH;

        [JsonIgnore] public bool Initialized => !(WinX <= 0 && WinY <= 0 && WinW <= 0 && WinH <= 0);
    }

    public class JsonDictionarySettings
    {
        //[JsonProperty(Required = Required.Always)]
        public string LastRootFolder { get; set; } = "";
        public bool ReformatJson { get; set; } = false;
        public bool BeautifyJson { get; set; } = false;
        public string LastDbName { get; set; } = "";
        public bool ShowPreview { get; set; } = true;
        public bool AlwaysOnTop { get; set; } = false;
        public bool LoadDbOnStart { get; set; } = false;
        public bool UseVsCode { get; set; } = false;
        public bool SchemaFollowSelection { get; set; } = false;
        public bool SaveJsonTree { get; set; } = true;
        public string DefaultDescriptionFileName { get; set; } = "descriptions.json";
        public string FileMask { get; set; } = "*.json";
        public int TreeSplitterDistance { get; set; } = 100;
        public int DescriptionSplitterDistance { get; set; } = 100;
        public int FileListSplitterDistance { get; set; } = 100;
        public string DefaultVersionCaption { get; set; } = "Any";
        public string VersionTagName { get; set; } = ".contentVersion";
        public char TableListDelimiter { get; set; } = ';';
        public char JsonPathDiv { get; set; } = '.';
        public string RootNodeName { get; set; } = "Kinetic";
        public float CellSizeAdjust { get; set; } = 0.7f;
        public string PreViewCaption { get; set; } = "[Preview] ";
        public string DefaultEditorFormCaption { get; set; } = "JsonDictionary [Kinetic]";
        public string DefaultTreeFileExtension { get; set; } = "tree";
        public string DefaultExamplesFileExtension { get; set; } = "examples";
        public string DefaultFiledialogFormCaption { get; set; } = "Kinetic data";
        public string DefaultSaveFileName { get; set; } = "KineticDictionary_";

        public WinPosition EditorPosition { get; set; } = new WinPosition
        {
            WinX = 400,
            WinY = 10,
            WinW = 200,
            WinH = 400
        };

        public WinPosition MainWindowPosition { get; set; } = new WinPosition
        {
            WinX = 10,
            WinY = 10,
            WinW = 200,
            WinH = 400
        };

        public ContentTypeItem[] FileTypes =
        {
            new ContentTypeItem
            {
                FileTypeSign = "dataviews.jsonc",
                PropertyTypeName = "dataviews",
                ContentType = "DataViews"
            },
            new ContentTypeItem
            {
                FileTypeSign = "events.jsonc",
                PropertyTypeName = "events",
                ContentType = "Events"
            },
            new ContentTypeItem
            {
                FileTypeSign = "layout.jsonc",
                PropertyTypeName = "layout",
                ContentType = "Layout"
            },
            new ContentTypeItem
            {
                FileTypeSign = "rules.jsonc",
                PropertyTypeName = "rules",
                ContentType = "Rules"
            },
            new ContentTypeItem
            {
                FileTypeSign = "tools.jsonc",
                PropertyTypeName = "tools",
                ContentType = "Tools"
            },
            new ContentTypeItem
            {
                FileTypeSign = "strings.jsonc",
                PropertyTypeName = "strings",
                ContentType = "Strings"
            },
            new ContentTypeItem
            {
                FileTypeSign = "patch.jsonc",
                PropertyTypeName = "patch",
                ContentType = "Patch"
            },
            new ContentTypeItem
            {
                FileTypeSign = "search.jsonc",
                PropertyTypeName = "search",
                ContentType = "Search"
            },
            new ContentTypeItem
            {
                FileTypeSign = "combo.jsonc",
                PropertyTypeName = "combo",
                ContentType = "Combo"
            }
        };

        public ProcessingOptions[] FlattenParameters =
        {
            new ProcessingOptions
            {
                ContentType = "Events",
                ItemName = "type",
                MoveToPath = "Events.actions",
                ParentNames = new[]
                {
                    "actions",
                    "onsuccess",
                    "onfailure",
                    "onerror",
                    "onsinglematch",
                    "onmultiplematch",
                    "onnomatch",
                    "onyes",
                    "onno",
                    "onok",
                    "oncancel",
                    "onabort",
                    "onempty"
                }
            },
            new ProcessingOptions
            {
                ContentType = "Layout",
                ItemName = "sourcetypeid",
                MoveToPath = "",
                ParentNames = new[] {"components"}
            },
            new ProcessingOptions
            {
                ContentType = "Rules",
                ItemName = "action",
                MoveToPath = "",
                ParentNames = new[] {"actions"}
            },
            new ProcessingOptions
            {
                ContentType = "Search",
                ItemName = "sourcetypeid",
                MoveToPath = "",
                ParentNames = new[] {"component"}
            }
        };
    }
}
