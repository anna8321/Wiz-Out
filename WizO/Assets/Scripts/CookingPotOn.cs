using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPotOn : MonoBehaviour
{
    [SerializeField] private GameObject vfxEmpty;
    [HideInInspector] public bool fireIsOn = false;
    public int spawnIndex = 0;

    [Header("Spawn bulles")]
    [SerializeField] private GameObject spawn1;
    [SerializeField] private GameObject spawn2;
    [SerializeField] private GameObject spawn3;
    [SerializeField] private GameObject prefab;

    [Header("Audio")]
    [SerializeField] private AudioSource water;
    [SerializeField] private AudioSource fire;

    private GameObject newBuble;

    private void Start()
    {
        vfxEmpty.SetActive(false);
    }

    public void FireIsOn()
    {
        vfxEmpty.SetActive(true);
        water.Play();
        fire.Play();
        fireIsOn = true;
        StartCoroutine(CookingBuble());
    }

    IEnumerator CookingBuble()
    {
        while(fireIsOn)
        {
            if(spawnIndex == 0)
            {
                newBuble = Instantiate(prefab, spawn1.transform.position, Quaternion.identity);
                newBuble.transform.parent = null;
                spawnIndex = 1;
                yield return new WaitForSeconds(1);
            }
            else if(spawnIndex == 1)
            {
                newBuble = Instantiate(prefab, spawn2.transform.position, Quaternion.identity);
                newBuble.transform.parent = null;
                spawnIndex = 2;
                yield return new WaitForSeconds(1);
            }
            else if(spawnIndex == 2)
            {
                newBuble = Instantiate(prefab, spawn3.transform.position, Quaternion.identity);
                newBuble.transform.parent = null;
                spawnIndex = 0;
                yield return new WaitForSeconds(1);
            }
        }
    }
}