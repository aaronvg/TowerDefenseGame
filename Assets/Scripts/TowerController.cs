using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Queue

/**
  * Control code for a turret. Registers targets in the order they enter the turret's range, rotates
  * horizontally to face those targets, and shoots at those targets once the turret is facing them.
  **/
public class TowerController : MonoBehaviour {

	/* Maximum delta angle per second */
	public float angularSpeed;

	private Queue<GameObject> targets;
	private bool tracking = false;

	void Start() {
		targets = new Queue<GameObject> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			targets.Enqueue(other.gameObject);
			if(!tracking)
				StartCoroutine(Attack());
		}
	}

	void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy") && targets.Count == 0)
        {
			tracking = false;
		}
	}

	IEnumerator Attack() {
		tracking = true;
		while (targets.Count > 0) {
			GameObject target = targets.Dequeue();

			while (tracking && target != null) {
				// Rotate to face target
				Vector3 relativePos = transform.position - target.transform.position;
				relativePos.y = 0;
				Quaternion dest = Quaternion.LookRotation (relativePos);
				Quaternion look = Quaternion.RotateTowards (gameObject.transform.rotation, dest, angularSpeed * Time.deltaTime);
				gameObject.transform.rotation = look;

				// Shoot at target if turret is pointing at it
				if(Quaternion.Angle(look, dest) < angularSpeed * Time.deltaTime)
					BroadcastMessage ("ShootAt", target);	

				yield return null;
			}
		}
		tracking = false;
	}
}
