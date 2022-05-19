using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    bool isIntercation; //���簡 �������� ��Ÿ���� bool
    GameObject go;      //������ ������Ʈ�� �־�δ� �ӽ� ����

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && isIntercation)
        {
            Interactioning(go);
        }
    }


    void Interactioning(GameObject ob)
    {
        Debug.Log(ob);
    }

    private void OnTriggerStay(Collider collider)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward); // ĳ������ �� ���� ����
        if (Physics.Raycast(transform.position, forward, 10) && collider.CompareTag("Animal")) // ĳ���Ͱ� �ٶ󺸰� �ְ�, ������ ���
        {
            collider.GetComponent<Outline>().enabled = true; // Outline Ȱ��ȭ
            isIntercation = true;   //���簡��

            go = collider.transform.parent.gameObject;  //mesh ������Ʈ�� �θ� ������Ʈ �޾ƿ���
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        isIntercation = false;  //���� �Ұ���

        if (collider.CompareTag("Animal")) // ������ ���
        {
            go = null;  //��ü �ʱ�ȭ
            collider.GetComponent<Outline>().enabled = false; // Outline ��Ȱ��ȭ
        }
    }
}
