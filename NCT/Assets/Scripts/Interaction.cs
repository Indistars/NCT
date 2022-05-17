using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    private void OnTriggerStay(Collider collider)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward); // ĳ������ �� ���� ����
        if (Physics.Raycast(transform.position, forward, 10) && collider.CompareTag("Animal")) // ĳ���Ͱ� �ٶ󺸰� �ְ�, ������ ���
        {
            collider.GetComponent<Outline>().enabled = true; // Outline Ȱ��ȭ
            print("������ ���� �Ǿ����ϴ�.");
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
