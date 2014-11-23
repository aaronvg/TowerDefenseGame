using UnityEngine;
using System.Collections;

public class PathMarker : MonoBehaviour
{
    public Transform NextDestination;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .25f);

        if (NextDestination)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, NextDestination.position);
        }
    }
}
