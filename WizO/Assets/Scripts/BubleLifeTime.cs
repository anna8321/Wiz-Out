using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubleLifeTime : MonoBehaviour
{
    private ChangePotion liquide;
    [SerializeField] Material[] matArray = new Material[3];

    // Start is called before the first frame update
    void Start()
    {
        liquide = GameObject.FindWithTag("Liquide").GetComponent<ChangePotion>();
        if(liquide.isStep1)
        {
            gameObject.GetComponent<MeshRenderer>().material = matArray[0];
        }
        else if(liquide.isStep2)
        {
            gameObject.GetComponent<MeshRenderer>().material = matArray[1];
        }
        else if(liquide.isStep3)
        {
            gameObject.GetComponent<MeshRenderer>().material = matArray[2];
        }

        StartCoroutine(Life());
    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(Vector3.up * 0.002f);
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(3.01f);
        Destroy(gameObject);
    }
}
