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
	private GameObject GameManager;

    public int NumPointDrops = 5;
    public int PointsPerDrop = 2;

	private bool ReachedEnd = false;
	// Use this for initialization
	void Start ()
	{
	    GameManager = GameObject.FindGameObjectWithTag("GameController");
        _maxHealth = Health;

        // handle healthbar UI
        var canvas = GameObject.FindGameObjectWithTag("UIRoot").GetComponent<Canvas>();
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
	        } else if(ReachedEnd == false){
				ReachedEnd = true;
				if(gimmick != null) {
					//gimmick.GetComponent<GimmickResponseHandler>().SetEnemy (gameObject);
					GameObject gim = Instantiate (gimmick, transform.position, Quaternion.identity) as GameObject;
					gim.GetComponent<GimmickResponseHandler>().SetEnemy(gameObject);
				}
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

	    for (int i = 0; i < NumPointDrops; i++)
	    {
            GameObject point = (GameObject)Instantiate(PointSphere, transform.position, transform.rotation);
            point.transform.position += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            point.rigidbody.AddForce(new Vector3(Random.Range(-10f, 10f), Random.Range(5f, 10f), Random.Range(-10f, 10f)),
                                     ForceMode.Impulse);
            point.GetComponent<PickupPoints>().NumPoints = PointsPerDrop;
	    }


		Destroy(gameObject);

		//GameManager.SendMessage ("UpdateCurrency", NumPoints);
		GameManager.SendMessage ("KillEnemy");
	}
}
