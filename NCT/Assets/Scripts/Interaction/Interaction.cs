using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : interactiveConversation
{
    [SerializeField] private KeyCode interactionKeyCode; // 상호작용 키 코드

    private void Update()
    {
        if (Input.GetKeyDown(interactionKeyCode) && canIntercation && !isInteracting)
        {
            DataExtract(interactiveObject);
            Interacting();
        }
    }
}
