using UnityEngine;
using System.Collections;

public class TowerMenuController : MonoBehaviour {

	private Camera camera;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindObjectOfType<Camera>();
	}
	
	void Update () {
		Vector3 cameraRotation = camera.transform.rotation.eulerAngles;
		cameraRotation.x = 24;
		cameraRotation.z = 0;
		transform.eulerAngles = cameraRotation;
	}
}
