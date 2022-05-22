using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIdentification : MonoBehaviour
{
    protected bool canIntercation; // ��ȣ�ۿ��� �������� ��Ÿ���� bool
    protected GameObject interactiveObject; // ��ȣ�ۿ��� ������Ʈ�� �־�δ� �ӽ� ����

    protected void OnTriggerStay(Collider collider)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward); // ĳ������ �� ���� ����
        if (Physics.Raycast(transform.position, forward, 10) && collider.CompareTag("Animal")) // ĳ���Ͱ� �ٶ󺸰� �ְ�, ������ ���
        {
            collider.GetComponent<Outline>().enabled = true; // Outline Ȱ��ȭ
            canIntercation = true;   // ���� ����

            interactiveObject = collider.transform.parent.gameObject;  // mesh ������Ʈ�� �θ� ������Ʈ �޾ƿ���
        }
    }

    protected void OnTriggerExit(Collider collider)
    {
        canIntercation = false;  // ���� �Ұ���

        if (collider.CompareTag("Animal")) // ������ ���
        {
            interactiveObject = null;  // ��ü �ʱ�ȭ
            collider.GetComponent<Outline>().enabled = false; // Outline ��Ȱ��ȭ
        }
    }
}
