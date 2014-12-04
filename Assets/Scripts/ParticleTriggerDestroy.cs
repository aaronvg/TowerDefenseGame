using UnityEngine;
using System.Collections;

public class ParticleTriggerDestroy : MonoBehaviour {

	public float duration;

	void Kill() {
		Destroy(gameObject, duration);
	}
}
