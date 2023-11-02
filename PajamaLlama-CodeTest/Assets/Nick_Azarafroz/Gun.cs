using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public GameObject effect;
    public Transform spawnTransform;
    public AudioClip shootSound;

    private bool playerPickedMeUp = false;
    private float flipAngleThreshold = 90f;

    private SpriteRenderer sr;
    private AudioSource audioSource;
    private GameObject mainCamera;
    private CameraShake shakeCamera;
    // Start is called before the first frame update

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        shakeCamera = mainCamera.GetComponent<CameraShake>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPickedMeUp) 
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg;

            transform.eulerAngles = new Vector3(0, 0, angle);

            if (Mathf.Abs(angle) > flipAngleThreshold)
            {
                sr.flipY = true;
            }
            else
            {
                sr.flipY = false;
            }

            if (Input.GetButtonUp("Attack"))
            {
                shakeCamera.SetShake(true);
                audioSource.PlayOneShot(shootSound);
                Instantiate(effect, spawnTransform.position, transform.rotation);
                Instantiate(projectile, spawnTransform.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.SetParent(collision.gameObject.transform, true);

            transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y
               + 0.7f, collision.gameObject.transform.position.z);

            playerPickedMeUp = true;
        }
    }
}
