using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    [Range(1,20)] public float Health = 10;

    public Transform Destination;
	// Use this for initialization
	void Start ()
	{
	    _navMeshAgent = GetComponent<NavMeshAgent>();
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

    void SetDestination(Transform dest)
    {
        Destination = dest;
        GetComponent<NavMeshAgent>().SetDestination(Destination.position);
    }

    void ApplyDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
