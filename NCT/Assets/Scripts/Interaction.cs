using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Animal")) // ������ ���
        {
            collider.GetComponent<Outline>().enabled = true; // Outline Ȱ��ȭ
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Animal")) // ������ ���
        {
            collider.GetComponent<Outline>().enabled = false; // Outline ��Ȱ��ȭ
        }
    }
}
