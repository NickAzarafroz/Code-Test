using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            transform.SetParent(collision.gameObject.transform, true);

            transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y
               + 0.9f, collision.gameObject.transform.position.z);

            GetComponent<CircleCollider2D>().radius = 10f;
            Destroy(gameObject, 12f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Token")) 
        {
            Transform coinTransform = collision.transform;
            Vector3 playerPosition = player.transform.position;

            coinTransform.position = Vector3.Lerp(coinTransform.position, playerPosition, 5f * Time.deltaTime);
        }
    }
}
