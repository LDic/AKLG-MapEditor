using UnityEngine;
using UnityEngine.UI;

public class NoticeUI : MonoBehaviour {

	private MapEditorManager editorInstance;
	private static GameObject createPanel;
	private static GameObject exportPanel;

	void Start()
	{
		editorInstance = MapEditorManager.instance;
		createPanel = transform.Find("CreatePanel").gameObject;
		exportPanel = transform.Find("ExportPanel").gameObject;
	}

	public static void AddToViewport(int index)
	{
		if(index == 0)
		{
			exportPanel.SetActive(false);
			createPanel.SetActive(true);
		}
		if(index == 1)
		{
			createPanel.SetActive(false);
			exportPanel.SetActive(true);
		}
	}

	public void ClearUI()
	{
		createPanel.SetActive(false);
		exportPanel.SetActive(false);
	}

	public void OnAdditionClicked()
	{
		editorInstance.Save();
		ClearUI();
	}

	public void OnCornfirmClicked()
	{
		editorInstance.SetSize();
		editorInstance.CreateTiles(editorInstance.Width, editorInstance.Height);
		ClearUI();
	}

	public void OnCancelClicked()
	{
		ClearUI();
	}
}
