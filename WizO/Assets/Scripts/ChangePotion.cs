using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePotion : MonoBehaviour
{
    [SerializeField] private Color32 colorStep1;
    [SerializeField] private Color32 colorStep2;
    [SerializeField] private Color32 colorStep3;
    [SerializeField] private GameObject SpawnPotion;

    [SerializeField] private CookingPotOn boolFire;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource wrongAnswer;
    [SerializeField] private AudioSource goodAnswer;
    [SerializeField] private AudioSource exitPotionSpawn;

    [HideInInspector] public bool isStarting;
    [HideInInspector] public bool isStep1;
    [HideInInspector] public bool isStep2;
    [HideInInspector] public bool isStep3;

    [Header("Valid Mark for recipe book")]
    [SerializeField] private GameObject mark1;
    [SerializeField] private GameObject mark2;
    [SerializeField] private GameObject mark3;

    [Header("Empty Wrong Answer")]
    [SerializeField] private GameObject empty1;
    [SerializeField] private GameObject empty2;
    [SerializeField] private GameObject empty3;


    private void Start()
    {
        isStarting = true;
        isStep1 = false;
        isStep2 = false;
        isStep3 = false;

        mark1.SetActive(false);
        mark2.SetActive(false);
        mark3.SetActive(false);
        SpawnPotion.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (boolFire.fireIsOn)
        {
            if (other.gameObject.CompareTag("Ingredient1"))
            {
                if (isStarting)
                {
                    isStarting = false;
                    isStep1 = true;

                    gameObject.GetComponent<SpriteRenderer>().color = colorStep1;
                    Destroy(other.gameObject);
                    goodAnswer.Play();
                    mark1.SetActive(true);
                }
            }
            else if (other.gameObject.CompareTag("Ingredient2"))
            {
                if (isStep1)
                {
                    isStep1 = false;
                    isStep2 = true;

                    gameObject.GetComponent<SpriteRenderer>().color = colorStep2;
                    Destroy(other.gameObject);
                    goodAnswer.Play();
                    mark2.SetActive(true);
                }
                else
                {
                    wrongAnswer.Play();
                    other.gameObject.transform.position = empty2.transform.position;

                }
            }
            else if (other.gameObject.CompareTag("Ingredient3"))
            {
                if (isStep2)
                {
                    isStep2 = false;
                    isStep3 = true;
                    gameObject.GetComponent<SpriteRenderer>().color = colorStep3;
                    SpawnPotion.SetActive(true);
                    Destroy(other.gameObject);
                    goodAnswer.Play();
                    mark3.SetActive(true);
                    exitPotionSpawn.Play();
                }
                else
                {
                    wrongAnswer.Play();
                    other.gameObject.transform.position = empty3.transform.position;
                }
            }
        }
        else if (boolFire.fireIsOn == false)
        {
            if (other.gameObject.CompareTag("Ingredient1"))
            {
                wrongAnswer.Play();
                other.gameObject.transform.position = empty1.transform.position;
            }
            else if (other.gameObject.CompareTag("Ingredient2"))
            {
                wrongAnswer.Play();
                other.gameObject.transform.position = empty2.transform.position;
            }
            else if (other.gameObject.CompareTag("Ingredient3"))
            {
                wrongAnswer.Play();
                other.gameObject.transform.position = empty3.transform.position;
            }
        }
    }
}
