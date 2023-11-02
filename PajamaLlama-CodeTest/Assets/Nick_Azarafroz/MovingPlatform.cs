using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed = 2f;
    public float amplitude = 2f;
    public bool horizontal;
    public bool vertical;

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
