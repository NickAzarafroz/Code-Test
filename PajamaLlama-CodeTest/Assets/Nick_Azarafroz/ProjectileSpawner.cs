using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;
    public float distanceThreshold = 10f;

    private GameObject player;
    private float distance;
    // Start is called before the first frame update
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
                Instantiate(projectile, projectilePos.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
}
