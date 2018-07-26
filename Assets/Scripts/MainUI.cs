using UnityEngine;

public class MainUI : MonoBehaviour {

	public void OnCreateClicked()
	{
		NoticeUI.AddToViewport(0);
	}

	public void OnLoadClicked()
	{
		NoticeUI.AddToViewport(3);
	}

	public void OnExportClicked()
	{
		NoticeUI.AddToViewport(1);
	}
}
