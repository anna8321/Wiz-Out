using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerLimit : MonoBehaviour
{
    [SerializeField] private float min;
    [SerializeField] private float max;

    private void Update()
    {
        if (gameObject.transform.localPosition.z > max)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, max);
        }
        else if (gameObject.transform.localPosition.z < min)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, min);
        }
    }
}
