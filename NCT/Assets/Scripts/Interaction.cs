using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    private void OnTriggerStay(Collider collider)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward); // 캐릭터의 앞 방향 벡터
        if (Physics.Raycast(transform.position, forward, 10) && collider.CompareTag("Animal")) // 캐릭터가 바라보고 있고, 동물일 경우
        {
            collider.GetComponent<Outline>().enabled = true; // Outline 활성화
            print("동물이 감지 되었습니다.");
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
