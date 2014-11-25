using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Queue, List, Dictionary

/**
  * Control code for a turret. Registers targets in the order they enter the turret's range, rotates
  * horizontally to face those targets, and shoots at those targets once the turret is facing them.
  **/
public class TowerController : MonoBehaviour {

	/* Maximum delta angle per second */
	public float angularSpeed;
	
	/* Enemy types this tower can target */
	public List<GameObject> enemies;

	private Queue<GameObject> targets;
	private Dictionary<string, bool> targetable;
	private bool tracking = false;


	void Start () { 
		targets = new Queue<GameObject> ();
		targetable = new Dictionary<string, bool> ();
		foreach (GameObject enemy in enemies)
			targetable [enemy.tag] = true;
	}

	/* Tells the tower to begin attacking a target that enters its range
	   if it is allowed to target that enemy type. If tower is already attacking
	   something, add the target to its queue instead */
	void OnTriggerEnter (Collider other)
	{
		bool isEnemy;
		targetable.TryGetValue (other.tag, out isEnemy);
		if (isEnemy)
		{
			targets.Enqueue (other.gameObject);
			if (!tracking)
				StartCoroutine (Attack ());
		}
	}

	/* Attacks an enemy, called once an enemy enters this tower's range 
	   and ends when there are no enemies in range */
	IEnumerator Attack () {
		tracking = true;
		while (targets.Count > 0) {
			GameObject target = targets.Dequeue ();

			while (tracking && target != null) {
				// If target has moved out of range, exit loop and acquire a new target
				if(!isInRange (target))
					break;
				
				// Rotate to face target
				Vector3 relativePos = transform.position - target.transform.position;
				relativePos.y = 0;
				Quaternion dest = Quaternion.LookRotation (relativePos);
				Quaternion look = Quaternion.RotateTowards (gameObject.transform.rotation, dest, angularSpeed * Time.deltaTime);
				gameObject.transform.rotation = look;
				BroadcastMessage ("LookAt", target);

				// Shoot at target if turret is pointing at it
				if(Quaternion.Angle (look, dest) < angularSpeed * Time.deltaTime)
					BroadcastMessage ("ShootAt", target);	

				yield return null;
			}
		}
		tracking = false;
	}
	
	/* Checks if a chosen target is within this tower's range */
	bool isInRange (GameObject target) {
		if (target != null) {
			float dist = Vector3.Distance (transform.position, target.transform.position);
			SphereCollider range = GetComponent ("SphereCollider") as SphereCollider;
			
			/* Why plus 2? Because dist is the distance between the centers of both objects.
			   Since enemies enter the sphere collider edge first, we must find a way to
			   account for the radius of the enemies elegantly (as in not +2) */
			if (dist > range.radius + 2)
				return false;
			else 
				return true;
		}
		return false;
	}
}
