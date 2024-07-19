using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bougie : MonoBehaviour
{
    [SerializeField] private GameObject flame;
    [HideInInspector] public bool candleIsOn = false;

    private void Start()
    {
        EteintBougie();
    }

    public void AllumeBougie()
    {
        flame.SetActive(true);
        candleIsOn = true;
        //StartCoroutine(FlameLifetime());
    }

    public void EteintBougie()
    {
        flame.SetActive(false);
        candleIsOn = false;
    }

    IEnumerator FlameLifetime()
    {
        new WaitForSeconds(120);
        EteintBougie();
        yield return null;
    }
}
