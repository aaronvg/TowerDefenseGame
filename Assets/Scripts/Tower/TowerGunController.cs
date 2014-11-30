using UnityEngine;
using System.Collections;

/**
  * Control code for a gun, which may or may not be used. While a turret rotates horizontally, a gun
  * tilts vertically to track targets. This code controls that tilt.
  **/
public class TowerGunController : MonoBehaviour {

	/* Maximum delta angle per second */
	public float angularSpeed;

	void LookAt(GameObject target) {
		Vector3 relativePos = transform.position - target.transform.position;
		Quaternion dest = Quaternion.LookRotation(relativePos);
		Quaternion look = Quaternion.RotateTowards (gameObject.transform.rotation, dest, angularSpeed * Time.deltaTime);
		
		Vector3 eulerAngles = look.eulerAngles;
		eulerAngles.y = gameObject.transform.eulerAngles.y;
		eulerAngles.z = gameObject.transform.eulerAngles.z;
		
		gameObject.transform.eulerAngles = eulerAngles;
	}
}
