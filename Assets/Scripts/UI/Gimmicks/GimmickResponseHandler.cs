using UnityEngine;
using System.Collections;

public class GimmickResponseHandler : MonoBehaviour {

	public bool isGood;
	public GameObject GameManager;
	private GameObject AttachedEnemy;

	void Start() {
		GameManager = GameObject.FindGameObjectWithTag ("GameController");
	}

	void Close() {
		Debug.Log("Action on close");
		if (isGood) {
			// do something sorta bad
			GameManager.SendMessage("UpdateInternetPresencePoints", -2);
		}
		else {
			// do something sorta good
			GameManager.SendMessage("UpdateCurrency", 2);
			GameManager.SendMessage("UpdateInternetPresencePoints", 2);

		}
		Destroy (gameObject);
		Destroy (AttachedEnemy);
	}

	void Yes() {
		Debug.Log("Action on reply");
		if (isGood) {
			// do something good
			GameManager.SendMessage("UpdateCurrency", 2);
			GameManager.SendMessage("UpdateInternetPresencePoints", 10);
		}
		else {
			// do something bad
			GameManager.SendMessage("UpdateInternetPresencePoints", -10);
		}
		Destroy (gameObject);
		Destroy (AttachedEnemy);
	}
	
	void No() {
		Debug.Log("Action on delete");
		if (isGood) {
			// do something bad
			GameManager.SendMessage("UpdateInternetPresencePoints", -10);
		}
		else {
			// do something good
			GameManager.SendMessage("UpdateCurrency", 2);
			GameManager.SendMessage("UpdateInternetPresencePoints", 10);
		}
		Destroy (gameObject);
		Destroy (AttachedEnemy);
	}

	public void SetEnemy(GameObject enemy) {
		AttachedEnemy = enemy;
	}
}
