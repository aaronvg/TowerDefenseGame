using UnityEngine;
using System.Collections;

/**
  * Control code for bullets. Seeks the target (which should be set by whatever fires the bullet using SetTarget())
  * and destroys both itself and the target on contact. When the bullet is destroyed, it detaches its child (which is
  * an empty object with a particle system) so the particles will fade out naturally.
  **/
public class TDProjectile : MonoBehaviour {
	
	/* Guaranteed time to target (in seconds) */
	public float hitTime;

    /** Damage to apply. */
    public float Damage;

	private GameObject target;
	private float speed;

	void SetTarget(GameObject _target) {
		target = _target;
		speed = Vector3.Distance(gameObject.transform.position, target.transform.position) / hitTime;
	}

	void Update() {
		// look at target
		if(target != null)
			gameObject.transform.LookAt (target.transform.position);

		// check collisions
		RaycastHit hit;
		if(Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 2.0f * speed * Time.deltaTime)) {
			// If it hits a target, destroy both the bullet and a target. Detach the particle system.
			if(hit.collider.gameObject.tag == "Enemy") {
				hit.collider.gameObject.SendMessage("ApplyDamage", Damage, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
				transform.DetachChildren();
			}
			// if it hits anything else that isn't a tower, destroy the bullet. Detach the particle system.
			/*if(hit.collider.gameObject.tag != "Tower") {
				Destroy(gameObject);
				transform.DetachChildren();
			}*/
		}
		// move forward
		gameObject.transform.position += transform.forward * speed * Time.deltaTime;
	}
}
