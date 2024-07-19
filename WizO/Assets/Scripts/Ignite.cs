using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Ignite : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bougie"))
        {
            other.gameObject.GetComponent<Bougie>().AllumeBougie();
        }

        if(other.gameObject.CompareTag("Chaudron"))
        {
            other.gameObject.GetComponent<CookingPotOn>().FireIsOn();
        }
    }
}
