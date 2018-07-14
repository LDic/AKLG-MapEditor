using UnityEngine;

public class Camera : MonoBehaviour {

	public float speed;
	public float borderThickness;

	private float[] cameraLimits = {15f, -5f, -5f, 15f};

	void Start()
	{
		transform.position = new Vector3(2f, 10f, 2f);
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.W) || Input.mousePosition.y > Screen.height-borderThickness)
		{
			if(transform.position.z < cameraLimits[0])
				transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
		}
		if(Input.GetKey(KeyCode.S) || Input.mousePosition.y < borderThickness)
		{
			if(transform.position.z > cameraLimits[1])
				transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
		}
		if(Input.GetKey(KeyCode.A) || Input.mousePosition.x < borderThickness)
		{
			if(transform.position.x > cameraLimits[2])
				transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
		}
		if(Input.GetKey(KeyCode.D) || Input.mousePosition.x > Screen.width - borderThickness)
		{
			if(transform.position.x < cameraLimits[3])
				transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
		}
	}
}
