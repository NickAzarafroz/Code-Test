using Platformer.Gameplay;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Platformer.Core.Simulation;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private float force = 5f;

    private Rigidbody2D rb;
    private AudioSource audioSource;

    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation + 90f);

        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {
            var enemyHealth = collision.GetComponent<Health>();
            var enemyController = collision.GetComponent<EnemyController>();
            var enemyDamage = collision.GetComponent<DamageHit>();

            enemyHealth.Decrement(0.4f);
            enemyDamage.Flash();

            if (!enemyHealth.IsAlive) 
            {
                Schedule<EnemyDeath>().enemy = enemyController;
            }

            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
