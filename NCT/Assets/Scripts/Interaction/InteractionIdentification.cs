using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIdentification : MonoBehaviour
{
    protected bool canIntercation; // 상호작용이 가능한지 나타내는 bool
    protected GameObject interactiveObject; // 상호작용할 오브젝트를 넣어두는 임시 변수

    protected void OnTriggerStay(Collider collider)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward); // 캐릭터의 앞 방향 벡터
        if (Physics.Raycast(transform.position, forward, 10) && collider.CompareTag("Animal")) // 캐릭터가 바라보고 있고, 동물일 경우
        {
            collider.GetComponent<Outline>().enabled = true; // Outline 활성화
            canIntercation = true;   // 조사 가능

            interactiveObject = collider.transform.parent.gameObject;  // mesh 오브젝트의 부모 오브젝트 받아오기
        }
    }

    protected void OnTriggerExit(Collider collider)
    {
        canIntercation = false;  // 조사 불가능

        if (collider.CompareTag("Animal")) // 동물일 경우
        {
            interactiveObject = null;  // 객체 초기화
            collider.GetComponent<Outline>().enabled = false; // Outline 비활성화
        }
    }
}
