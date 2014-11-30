using UnityEngine;
using System.Collections;

public class PickupPoints : MonoBehaviour {

	public int NumPoints = 5;
	public float timeLeft = 5f;

	private GameObject _gameManager;

	void Start()
    {
	    _gameManager = GameObject.FindGameObjectWithTag("GameController");

	    timeLeft *= Random.Range(.9f, 1f);
    }
	
	void Update()
    {
		timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            _gameManager.SendMessage("UpdateCurrency", NumPoints);
			Destroy(gameObject);
		}


        // check for mouseover
        // OnMouseOver is meant for GUI
	    RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    if (collider != null && collider.Raycast(ray, out hit, 100f))
	    {
	        _gameManager.SendMessage("UpdateCurrency", NumPoints);
            Destroy(gameObject);
	    }
    }

	void OnTriggerEnter(Collider other)
    {
		/*if(!other.isTrigger){
			//Debug.Log("Collided with " + other.gameObject.name);
			rigidbody.velocity=Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			this.rigidbody.isKinematic = true;
			this.collider.isTrigger = false;
		}*/
	}
}
