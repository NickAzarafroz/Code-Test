using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private Transform projectilePos;
    [SerializeField] private AudioClip asteroidSpawn;
    [SerializeField] private float distanceThreshold = 10f;

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
            yield return new WaitForSeconds(2.5f);
        }
    }
}
