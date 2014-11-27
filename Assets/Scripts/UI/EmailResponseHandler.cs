using UnityEngine;
using System.Collections;

public class EmailResponseHandler : MonoBehaviour {

	public bool isGood;

	void Close() {
		Debug.Log("Action on close");
		if (isGood) {
			// do something sorta bad
		}
		else {
			// do something sorta good
		}
		Destroy (gameObject);
	}

	void Reply() {
		Debug.Log("Action on reply");
		if (isGood) {
			// do something good
		}
		else {
			// do something bad
		}
		Destroy (gameObject);
	}
	
	void Delete() {
		Debug.Log("Action on delete");
		if (isGood) {
			// do something bad
		}
		else {
			// do something good
		}
		Destroy (gameObject);
	}	
}
