using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using static Platformer.Core.Simulation;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private float force;

    private GameObject player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2 (direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            var playerController = collision.GetComponent<PlayerController>();

            if (!playerController.invincible) 
            {
                Schedule<PlayerDeath>();
            }

            Instantiate(effect, transform.position, transform.rotation); 
            Destroy(gameObject);
        }

        if (collision.CompareTag("Rocket"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            Instantiate(effect, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
