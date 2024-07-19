using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelection : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject fireballEmpty;
    [SerializeField] private GameObject soluceEmpty;
    [SerializeField] private GameObject magicCircle;
    [HideInInspector] public bool fireballSelected = false;
    [HideInInspector] public bool soluceSelected = false;
    [HideInInspector] private bool nothingSelected = true;

    private void Start()
    {
        //UseNothing();
        UseFireball();
    }

    public void UseFireball()
    {
        fireballEmpty.SetActive(true);
        soluceEmpty.SetActive(false);
        print("Use fireball");
    }

    public void UseSoluce()
    {
        fireballEmpty.SetActive(false);
        soluceEmpty.SetActive(true);
        print("Use soluce");
    }

    public void UseNothing()
    {
        fireballEmpty.SetActive(false);
        soluceEmpty.SetActive(false);
        print("Use nothing");
    }

    public void shootFireball()
    {
        GameObject newFireball = Instantiate(fireballPrefab, magicCircle.transform.position, Quaternion.identity);
        newFireball.transform.parent = magicCircle.transform;
        newFireball.GetComponent<Rigidbody>().AddForce(Vector3.forward * 200);
        newFireball.transform.parent = null;
    }
}
