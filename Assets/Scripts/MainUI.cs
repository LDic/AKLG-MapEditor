using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {
	
	private static Image selectedImage;

	void Start()
	{
		selectedImage = transform.Find("SelectedImage").GetComponent<Image>();
	}

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

	// 타일 선택할 때 현재 선택한 개체 이미지 변경
	public static void SetSelectedImage(Sprite spriteImage)
	{
		selectedImage.sprite = spriteImage;
	}
}
