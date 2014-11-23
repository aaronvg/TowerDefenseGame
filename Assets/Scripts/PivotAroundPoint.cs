﻿using UnityEngine;
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

    private float _targetDistance;
    private float _targetYaw;

    private Vector3 _lastMousePosition;

    void Start()
    {
        if (Application.isPlaying)
        {
            _targetYaw = YawAngleFromPoint;
            _targetDistance = DistanceFromPoint;
        }
    }

    void Update()
    {
        // input
        if (Application.isPlaying)
        {
            if (Input.GetMouseButton(0))
            {
                var motion = Input.mousePosition - _lastMousePosition;
                _targetYaw += motion.x / 2;
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                _targetDistance -= Input.mouseScrollDelta.y * 5;
            }
            
            // clamp target values
            _targetDistance = Mathf.Clamp(_targetDistance, 10, 50);

            // lerping angles
            YawAngleFromPoint = Mathf.LerpAngle(YawAngleFromPoint, _targetYaw, .1f);
            DistanceFromPoint = Mathf.Lerp(DistanceFromPoint, _targetDistance, .025f);

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
