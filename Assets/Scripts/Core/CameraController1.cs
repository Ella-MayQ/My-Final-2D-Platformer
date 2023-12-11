using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private float smooth;

    private Vector3 velocity;

    // LateUpdate is called once per frame, after Update
    private void LateUpdate()
    {
        // Move the camera towards the target with smooth damping
        transform.position = Vector3.SmoothDamp(transform.position, target.position + posOffset, ref velocity, smooth);
    }
}
