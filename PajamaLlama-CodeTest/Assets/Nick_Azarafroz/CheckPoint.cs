using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Model;
using Platformer.Core;

public class CheckPoint : MonoBehaviour
{
    PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    // Start is called before the first frame update
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
            model.spawnPoint.position = transform.position;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
