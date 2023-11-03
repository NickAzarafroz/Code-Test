using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public GameObject spawnEffect;
    public Transform projectilePos;
    public AudioClip asteroidSpawn;
    public float distanceThreshold = 10f;

    private GameObject player;
    private AudioSource audioSource;
    private float distance = 100f;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnProjectileInterval());
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
    }

    IEnumerator SpawnProjectileInterval() 
    {
        while (true) 
        {
            if(distance < distanceThreshold) 
            {
                audioSource.PlayOneShot(asteroidSpawn);
                Instantiate(spawnEffect, projectilePos.transform.position, Quaternion.identity);
                Instantiate(projectile, projectilePos.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
