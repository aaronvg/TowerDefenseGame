using UnityEngine;
using System.Collections;

public class PickupPoints : MonoBehaviour {

	public int NumPoints = 5;
	public float timeLeft = 15f;

	public GameObject GameManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if ( timeLeft < 0 ){
			Destroy(this.gameObject);
		}
	}

	void OnMouseOver(){
		Debug.Log("hello");
		GameManager.SendMessage ("UpdateCurrency", NumPoints);
		Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider other){
		if(!other.isTrigger){
			//Debug.Log("Collided with " + other.gameObject.name);
			rigidbody.velocity=Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			this.rigidbody.isKinematic = true;
			this.collider.isTrigger = false;
		}
	}
}
