using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float cameraShakeDurarion;
    [SerializeField] private AnimationCurve curve;

    private bool cameraShakeStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraShakeStart)
        {
            cameraShakeStart = false;
            StartCoroutine(Shaking());
        }
    }

    public bool SetShake(bool value)
    {
        return cameraShakeStart = value;
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < cameraShakeDurarion)
        {
            startPosition = transform.position;
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / cameraShakeDurarion);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}
