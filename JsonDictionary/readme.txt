English
Short manual:
1) Push "Collect database" button to show root folder of the MetaUI repository. It'll search all files marked before to parse. A tree and a examples table is generated. It may take a couple of minutes.
2) Database collected can be saved (binary serializer format) with "Save database" button to avoid regenerating it next time. There are 2 files saved - .metalib database and .metalib.tree tree.
3) "Samples tree" contains a tree to select keywerd needed and dsiplay samples with double-click on a tree element.
4) Examples table can be filtered by schema version (got from "contentVersion" field) and a search phrase. Filter is applied to a current grid view so filters can be applied sequentially. Filtering can be slow on huge amount of data so may take some time.
5) Filters applied are shown in the text field below the grid.
6) Version filter always reset grid content on new value applied. Be aware the string filters will be reset.
7) To reset the filtering double-click on the tree item again to clear the search textbox and press Enter. Version filter set to "Any" does the same.
8) Double click on grid cell text opens it for editing (to select/copy string needed). Changes won't be saved.
9) Use Readjust button to adjust grid rows height (70% of grid control height) if needed (will be reset to auto-height on next grid refill).
10) Single column height can be auto-adjusted with double-click on row header as well.
11) Following actions works in tree:
    - double-click - show examples for the selected node in the grid
    - Delete - delete node
    - Ctrl-C - copy node name to clipboard
12) Following shotcuts works in description:
    - double-click - edit the description text
    - Ctrl-Enter - save text
13) Following shotcuts works in grid:
    - double-click - open file in editor
14) Descriptions edited are saved to file on program close.

Tech. notes:
- Only the first file name for every unique example is displayed in grid by default (otherwise there'll be hundreds of files for some fields). Can be switched by "Collect all fileNames" setting.
- UI critical controls are inactive on long operations.
- It's possible to re-format json-queries to place starting brackets ("[,{") on the next line (I personally prefer this way). Works on database creation time. Can be enabled by "Reformat JSON" setting.

====
Russian
������� ����������:
1) ������� "Collect database" ��������� �������� ����� � JSONC �������. �� "��" ��������� ���� ��� *.jsonc ����� � ������ ��, �������� �������� �����. �������� ������ � � ���� ���������� �������� (��������). ��� ����� ������ ������ ������� (30 ����� �� ������ ��������).
2) ��������� ���� ����� ��������� (� ������� binary serializer), ����� �� �������� ����� �� "Save database". ����������� 2 ����� - .kineticLib � ����� �������� � .kineticLib.tree � �������.
3) �� ������� "Schema tree" ����� � ������ ������� ������������ �������� ����� � �� �������� ����� �� ��� ������ � ����� �������� �������� � ���������� �� ���� ���������.
4) �������� ����� ������������� ��������� ������ - �� ������ (�� ���� "contentVersion") � �� �������� �����. ������ ������������� ��� ������� ENTER � ��������� ����. ����������� ������� ���������� �����, ��� ��� ����� ��������������� �������� ��������� ��������. ���������� ������� ��������� � ��������� ���� ��� ���������.
5) ���������� ������� ��������� � ��������� ���� ��� ���������.
6) ������ �� ������ ������ ����������� ���������� ������� ��� ��������� ������. ��������� ������� ��� ���� ������������.
7) ��� ������ ������� ����� �������� ������ ������ ��� ������� ������� ���� �� ������� � ������.
8) ������� ������ � ����� ����� ������� ����, � ������� ������ ���� ������. ������ ����� �������, ���� ��� ��������.
9) ������ ������� ������� ����� ���� ���������� �� 70% �� ������ ������� ������� "Adjust rows".
10) ������ ���������� ������� ����� ���������� �� 70% �� ������ ������� ������� ������ �� ��������� �������.
11) � ������ �������� ��������� ��������:
    - ������� ���� - ������� ������� ��� ���������� �������� � �������
    - Delete - ������� �������
    - Ctrl-C - ����������� �������� �������� � ����� ������
12) � ���� �������� �������� ��������� ��������:
    - ������� ���� - ������������� ����� ��������
    - Ctrl-Enter - ��������� �����
13) � ������� �������� ��������� ��������:
    - ������� ���� - ������� ���� � ���������
14) ����������������� �������� ����������� � ���� ��� �������� ���������.

����������� ������:
- ��-��������� � ���� "File Name" ����������� ��� ������ ������� ����� ��� ������� ����������� ��������� ���� (����� � ��������� ����� ����� �� ����� � ����� ������).  ���������� ���������� "Collect all fileNames".
- �� ����� ���� ���������� �������� ��� ��������� �������� �� UI ���������������.
- ������������� ����������� ����������������� json-��������� ��� ����������� �������/������ ������ �� ���� ������� � ��������� ������ (��-��������� ������� ������ �������� �� ������ �������). ���������� ���������� "Reformat JSON".

----
andrey.kalugin@epicor.com
