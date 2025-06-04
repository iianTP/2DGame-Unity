using System;
using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform playerTransform;
    Vector3 velocity = Vector3.zero;

    [Range(0, 1)] public float moveSmoothness;

    public Vector3 positionOffset;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        Vector3 targetPosition = playerTransform.position + positionOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, moveSmoothness);
    }


}
