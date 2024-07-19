using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Room lightings")]

    [SerializeField] private Light lantern1;
    [SerializeField] private Light lantern2;
    [SerializeField] private Light startingSpotlight;

    [Header("Props Scripts")]
    [SerializeField] private Bougie candle;

    [SerializeField] private GameObject handFire;
    private bool lanternIsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        lantern1.enabled = false;
        lantern2.enabled = false;
        startingSpotlight.enabled = true;
        handFire.SetActive(false);
    }

    public void GrabCandle()
    {
        handFire.SetActive (true);
    }

    public void DropCandle()
    {
        handFire.SetActive(false);
    }

    private void AllumeLanterne()
    {
        lantern1.enabled = true;
        lantern2.enabled = true;
    }

    private void EteintLanterne()
    {
        lantern1.enabled = false;
        lantern2.enabled = false;
    }

    private void FixedUpdate()
    {
        if(candle.candleIsOn == true && !lanternIsOn)
        {
            lanternIsOn = true;
            AllumeLanterne();
        }
        else if(candle.candleIsOn == false && lanternIsOn)
        {
            lanternIsOn= false;
            EteintLanterne();
        }
        
    }

}
