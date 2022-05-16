using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Animal")) // 동물일 경우
        {
            collider.GetComponent<Outline>().enabled = true; // Outline 활성화
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Animal")) // 동물일 경우
        {
            collider.GetComponent<Outline>().enabled = false; // Outline 비활성화
        }
    }
}
