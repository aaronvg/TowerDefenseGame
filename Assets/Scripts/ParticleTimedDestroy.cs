using UnityEngine;
using System.Collections;

public class ParticleTimedDestroy : MonoBehaviour {

	public float duration;

	void Start () {
		Destroy (gameObject, duration);
	}
}
