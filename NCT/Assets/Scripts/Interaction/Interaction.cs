using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : interactiveConversation
{
    [SerializeField] private KeyCode interactionKeyCode; // 상호작용 키 코드

    private void Update()
    {
        if (Input.GetKeyDown(interactionKeyCode) && canIntercation && !isInteracting) // 상호작용 조건 충족시 
        {
            DataExtract(interactiveObject); // 데이터 받기
            Interacting(); // 상호작용
        }
    }
}
