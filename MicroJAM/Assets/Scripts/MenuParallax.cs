using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    public float offsetMultiplier = 3f;
    public float smoothTime = .2f;

    private Vector2 startPosition;
    private Vector3 velocity;
    // Start is called before the first frame update
    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
    }
}