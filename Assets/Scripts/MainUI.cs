using UnityEngine;

public class MainUI : MonoBehaviour {

	private int width, height;
	private MapEditorManager editorInstance;

	void Start()
	{
		editorInstance = MapEditorManager.instance;
	}

	public void OnCreateClicked()
	{
		editorInstance.SetSize();
		width = editorInstance.Width;
		height = editorInstance.Height;
		editorInstance.CreateTiles(width, height);
	}

	public void OnExportClicked()
	{
		editorInstance.Save();
	}
}
