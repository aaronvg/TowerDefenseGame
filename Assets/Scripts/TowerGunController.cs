using UnityEngine;
using System.Collections;

/**
  * Control code for a gun, which may or may not be used. While a turret rotates horizontally, a gun
  * tilts vertically to track targets. This code controls that tilt.
  **/
public class TowerGunController : MonoBehaviour {

	void ShootAt(GameObject target) {
		Vector3 relativePos = transform.position - target.transform.position;
		Quaternion dest = Quaternion.LookRotation(relativePos);
		
		Vector3 eulerAngles = dest.eulerAngles;
		eulerAngles.y = gameObject.transform.eulerAngles.y;
		eulerAngles.z = gameObject.transform.eulerAngles.z;
		
		gameObject.transform.eulerAngles = eulerAngles;
	}
}
