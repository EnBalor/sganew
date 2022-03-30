using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFrustum : MonoBehaviour
{
    private Camera _Camera;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float X;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float Y;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float W;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float H;

    Vector3[] FrustumLine = new Vector3[4];

    void Start()
    {
        _Camera = GetComponent<Camera>();
    }

    void Update()
    {

        _Camera.CalculateFrustumCorners(
            new Rect(X, Y, W, H),
            _Camera.farClipPlane,
            Camera.MonoOrStereoscopicEye.Mono,
            FrustumLine );

        Debug.DrawLine(_Camera.transform.position, FrustumLine[0], Color.red);
        Debug.DrawLine(_Camera.transform.position, FrustumLine[1], Color.yellow);
        Debug.DrawLine(_Camera.transform.position, FrustumLine[2], Color.green);
        Debug.DrawLine(_Camera.transform.position, FrustumLine[3], Color.blue);

    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(FrustumLine[3], 1.0f);
    }
}
