using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockedManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<XRGrabInteractable>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Key"))
        {
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            gameObject.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }


}
