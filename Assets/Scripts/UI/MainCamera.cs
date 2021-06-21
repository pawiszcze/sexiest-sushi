using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public float yOffset;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = this.gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (target)
        {
            Vector3 point = mainCamera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.35f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}