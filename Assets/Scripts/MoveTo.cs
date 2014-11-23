using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveTo : MonoBehaviour
{

    public Transform Destination;

    private Vector3 _startingPosition;
    private bool _destinationSet;
    private bool _returning;

	void Start ()
	{
	    _startingPosition = transform.position;
	}
	
	void Update ()
    {
	    if (!_destinationSet)
	    {
	        GetComponent<NavMeshAgent>().SetDestination(_returning ? _startingPosition : Destination.position);
	        _destinationSet = true;
	        _returning = _returning != true;
	    }
	    else
	    {
	        if ((GetComponent<NavMeshAgent>().destination - transform.position).magnitude < 3)
	        {
	            _destinationSet = false;
	        }
	    }
	}

}
