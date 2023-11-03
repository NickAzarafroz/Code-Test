using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float speed = 2f;
    [SerializeField] private float amplitude = 2f;
    [SerializeField] private bool horizontal;
    [SerializeField] private bool vertical;

    private float startX;
    private float startY;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (horizontal)
        {
            float x = startX + amplitude * Mathf.Sin(Time.time * speed);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        if (vertical)
        {
            float y = startY + amplitude * Mathf.Sin(Time.time * speed);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
    }
}
