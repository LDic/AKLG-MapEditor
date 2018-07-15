using UnityEngine;

public class MainUI : MonoBehaviour {

	private MapEditorManager editorInstance;

	void Start()
	{
		editorInstance = MapEditorManager.instance;
	}

	public void OnCreateClicked()
	{
		NoticeUI.AddToViewport(0);
	}

	public void OnExportClicked()
	{
		NoticeUI.AddToViewport(1);
	}
}
