using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeDoor : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private Color emissiveColor;

    private void Start()
    {
        
        mat.DisableKeyword("_EMISSION");
        mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("CorrosiveObject"))
        {
            Destroy(collision.gameObject);
            //mat = gameObject.GetComponent<MeshRenderer>().materials[0];
            StartCoroutine(fading());
        }
    }

    IEnumerator fading()
    {
        mat.EnableKeyword("_EMISSION");

        gameObject.GetComponent<AudioSource>().Play();
        
        mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
