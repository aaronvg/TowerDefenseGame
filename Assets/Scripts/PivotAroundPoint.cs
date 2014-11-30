using System;
using UnityEngine;
using System.Collections;

/**
 * Pivots the object around a given point
 */
public class PivotAroundPoint : MonoBehaviour
{
    public Transform PivotPoint;

    [Range(10, 100)]
    public float DistanceFromPoint = 10;

    [Range(0, 360)] public float YawAngleFromPoint = 0;

    [Range(0, 90)] public float PitchAngleFromPoint = 45;

    [NonSerialized]
    public float TargetDistance;
    [NonSerialized]
    public float TargetYaw;
    [NonSerialized]
    public float TargetPitch;

    private Vector3 _lastMousePosition;

    void Start()
    {
        if (Application.isPlaying)
        {
            TargetYaw = YawAngleFromPoint;
            TargetDistance = DistanceFromPoint;
            TargetPitch = PitchAngleFromPoint;
        }
    }

    void Update()
    {
        // input
        if (Application.isPlaying)
        {
            if (Input.GetMouseButton(1))
            {
                var motion = Input.mousePosition - _lastMousePosition;
                TargetYaw += motion.x / 2;
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                TargetDistance -= Input.mouseScrollDelta.y * 5;
            }
            
            // clamp target values
            TargetDistance = Mathf.Clamp(TargetDistance, 10, 50);

            // lerping angles
            // We lerp these relative to a 144f framerate because that's the framerate I had when I was setting this up.
            // It's entirely arbitrary and we can adjust it for 60 in the future if it's REEEALLY necessary.
            YawAngleFromPoint = Mathf.LerpAngle(YawAngleFromPoint, TargetYaw, (.1f / (1f/144f)) * Time.deltaTime);
            PitchAngleFromPoint = Mathf.LerpAngle(PitchAngleFromPoint, TargetPitch, (.1f/(1f/144f)) * Time.deltaTime);
            DistanceFromPoint = Mathf.Lerp(DistanceFromPoint, TargetDistance, (.025f / (1f/144f)) * Time.deltaTime);

            // reset mouse position
            _lastMousePosition = Input.mousePosition;
        }

        if (PivotPoint != null)
        {
            // set distance
            var position = transform.position = PivotPoint.position;
            transform.eulerAngles = new Vector3(0, 0, 0);

            var offset = new Vector3(0, 0, DistanceFromPoint);
            offset = Quaternion.AngleAxis(-PitchAngleFromPoint, Vector3.right) * offset; // pitch
            offset = Quaternion.AngleAxis(YawAngleFromPoint, Vector3.up)*offset; // yaw

            position += offset;
            transform.position = position;
            transform.LookAt(PivotPoint);

            Debug.DrawLine(PivotPoint.position, PivotPoint.position + offset);
        }

        if (YawAngleFromPoint > 360) YawAngleFromPoint -= 360;
        if (YawAngleFromPoint < 0) YawAngleFromPoint += 360;
        if (PitchAngleFromPoint > 360) PitchAngleFromPoint -= 360;
        if (PitchAngleFromPoint < 0) PitchAngleFromPoint += 360;
    }
}
