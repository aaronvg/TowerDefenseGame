
using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{
	private GameObject _gameManager;
	public int bonusRate;

	void Start() {
		_gameManager = GameObject.FindGameObjectWithTag("GameController");
		if (_gameManager == null)
		{
			Debug.LogError(
				"Game manager could not be set; is there a game object with the tag GameController in the scene?", this);
		}

		_gameManager.SendMessage ("UpdateInternetPresenceRate", bonusRate);
	}
	
	
	void Update() {
		
	}

	void onDestroy() {
		_gameManager.SendMessage ("UpdateInternetPresenceRate", -bonusRate);
	}
}
