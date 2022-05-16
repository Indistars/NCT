using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Animal"))
        {
            collider.GetComponent<Outline>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Animal"))
        {
            collider.GetComponent<Outline>().enabled = false;
        }
    }
}
