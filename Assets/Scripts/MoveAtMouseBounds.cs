using UnityEngine;
using System.Collections;

public class MoveAtMouseBounds : MonoBehaviour
{
    public float Speed = 2f;
    public float PanThreshold = .1f;

    private Vector3 _target;

    void Start()
    {
        _target = transform.position;
    }

    void Update()
    {
        Vector3 panDirection = new Vector3();
        // Keyboard movement
        if (Input.GetKey(KeyCode.W))
        {
            panDirection.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            panDirection.z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            panDirection.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            panDirection.x += 1;
        }

        // Mouse movement
        var mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToViewportPoint(mousePos);
        if (mousePos.x < PanThreshold)
        {
            panDirection.x -= 1;
        }
        if (mousePos.x > 1 - PanThreshold)
        {
            panDirection.x += 1;
        }
        if (mousePos.y < PanThreshold)
        {
            panDirection.z -= 1;
        }
        if (mousePos.y > 1 - PanThreshold)
        {
            panDirection.z += 1;
        }

        // Apply camera angle
        var forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        var right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();
        panDirection.Normalize();
        var zDisplacement = panDirection.z * forward;
        var xDisplacement = panDirection.x * right;

        _target += (zDisplacement + xDisplacement)*Time.deltaTime*Speed;

        // Lerp to target
        transform.position = Vector3.Lerp(transform.position, _target, 20f*Time.deltaTime);
    }
}
