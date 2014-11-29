using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehavior : MonoBehaviour
{
    private Canvas _canvas;

    public GameObject UIHealthbarPrefab;
    public GameObject deathAnimation;
	public GameObject PointSphere;
    
    private GameObject _healthbar;
    private float _defaultHealthbarLength;

    [Range(1,20)] public float Health = 10;
    private float _maxHealth;

    public Transform Destination;
    public GameObject gimmick;
	public GameObject GameManager;
	public int NumPoints = 10;

	// Use this for initialization
	void Start ()
	{
        _maxHealth = Health;

        // handle healthbar UI
        var canvas = FindObjectOfType<Canvas>();
        if (canvas)
        {
            _canvas = canvas;
            var hb = (GameObject) Instantiate(UIHealthbarPrefab);
            hb.GetComponent<RectTransform>().SetParent(_canvas.GetComponent<RectTransform>());
            hb.GetComponent<RectTransform>().position = new Vector3();
            _healthbar = hb;
            hb.SendMessage("SetMaxHealth", _maxHealth);
            hb.GetComponent<HealthbarUIHandler>().TrackingObject = transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (Destination && (Destination.position - transform.position).magnitude < 2f)
	    {
	        // See if this destination has a PathMarker
	        var pm = Destination.GetComponent<PathMarker>();
	        if (pm)
	        {
                SetDestination(pm.NextDestination);
	        }
	    }
	}

    public void SetDestination(Transform dest)
    {
        Destination = dest;
        GetComponent<NavMeshAgent>().SetDestination(Destination.position);
    }

    void ApplyDamage(float Damage)
    {
        Health -= Damage;

        if (_healthbar)
        {
            _healthbar.SendMessage("SetHealth", Health);
        }
        if (Health <= 0)
        {
			Death();
        }
    }

	void Death(){
		Instantiate (deathAnimation, transform.position, transform.rotation);

		GameObject point = (GameObject)Instantiate(PointSphere, transform.position, transform.rotation);
		point.rigidbody.AddForce(new Vector3(Random.Range(-10f,10f), Random.Range(0f,10f), Random.Range(-10f,10f)),
		                         ForceMode.Impulse);

		Destroy(gameObject);

		GameManager.SendMessage ("UpdateCurrency", NumPoints);
		GameManager.SendMessage ("KillEnemy");
	}
}
