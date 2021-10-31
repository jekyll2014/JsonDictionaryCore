using static JsonDictionaryCore.Form1;

namespace JsonDictionaryCore
{
    public struct ProcessingOptions
    {
        public string FileType;
        public string ItemName;
        public string MoveToPath;
        public string[] ParentNames;
    }

    public struct WinPosition
    {
        public int WinX;
        public int WinY;
        public int WinW;
        public int WinH;

        public bool Initialized
        {
            get => !(WinX <= 0 && WinY <= 0 && WinW <= 0 && WinH <= 0);
        }
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
        public string DefaultFiledialogFormCaption { get; set; } = "KineticScheme data";
        public string DefaultSaveFileName { get; set; } = "KineticDictionary_";

        public WinPosition EditorPosition { get; set; } = new WinPosition
        {
            WinX = 400,
            WinY = 10,
            WinW = 200,
            WinH = 400,
        };

        public WinPosition MainWindowPosition { get; set; } = new WinPosition
        {
            WinX = 10,
            WinY = 10,
            WinW = 200,
            WinH = 400,
        };

        public ContentTypeItem[] FileTypes = {
            new ContentTypeItem
            {
                FileTypeMask = "dataviews.jsonc",
                PropertyTypeName = "dataviews",
                FileType = "DataViews"
            },
            new ContentTypeItem
            {
                FileTypeMask = "events.jsonc",
                PropertyTypeName = "events",
                FileType = "Events"
            },
            new ContentTypeItem
            {
                FileTypeMask = "layout.jsonc",
                PropertyTypeName = "layout",
                FileType = "Layout"
            },
            new ContentTypeItem
            {
                FileTypeMask = "rules.jsonc",
                PropertyTypeName = "rules",
                FileType = "Rules"
            },
            new ContentTypeItem
            {
                FileTypeMask = "tools.jsonc",
                PropertyTypeName = "tools",
                FileType = "Tools"
            },
            new ContentTypeItem
            {
                FileTypeMask = "strings.jsonc",
                PropertyTypeName = "strings",
                FileType = "Strings"
            },
            new ContentTypeItem
            {
                FileTypeMask = "patch.jsonc",
                PropertyTypeName = "patch",
                FileType = "Patch"
            },
            new ContentTypeItem
            {
                FileTypeMask = "search.jsonc",
                PropertyTypeName = "search",
                FileType = "Search"
            },
            new ContentTypeItem
            {
                FileTypeMask = "combo.jsonc",
                PropertyTypeName = "combo",
                FileType = "Combo"
            },
        };

        public ProcessingOptions[] FlattenParameters = {
            new ProcessingOptions
            {
                FileType = "Events",
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
                FileType = "Layout",
                ItemName = "sourcetypeid",
                MoveToPath = "",
                ParentNames = new[] { "components" }
            },
            new ProcessingOptions
            {
                FileType = "Rules",
                ItemName = "action",
                MoveToPath = "",
                ParentNames = new[] { "actions" }
            },
            new ProcessingOptions
            {
                FileType = "Search",
                ItemName = "sourcetypeid",
                MoveToPath = "",
                ParentNames = new[] { "component" }
            }
};
    }
}
