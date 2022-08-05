using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : MonoBehaviour
{
    AudioSource audioData;

    private void OnTriggerEnter(Collider other)
    {
        audioData.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioData.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
