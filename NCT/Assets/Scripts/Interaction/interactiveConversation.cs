using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactiveConversation : InteractionDataExtraction
{
    [SerializeField] protected Camera mainCamera; // 움직일 카메라
    [SerializeField] protected Transform playerBody; // 플레이어 모델을 관리할 변수 

    [SerializeField] protected GameObject conversationPanel; // 대화 UI 패널

    private GameObject conversationGameObject;

    private Vector3 cameraFieldOfValue; // 카메라 확대값

    protected bool isInteracting; // 상호작용 중인지 판별할 변수

    /// <summary>
    /// 상호작용 실행 함수
    /// </summary>
    public void Interacting()
    {
        isInteracting = true;
        playerBody.parent.GetComponent<TPSCharacterManipulation>().enabled = false;
        playerBody.parent.GetComponent<ZoomCamera>().setFPS();
        conversationGameObject = Instantiate(conversationPanel);
        conversationGameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }

    /// <summary>
    /// 상호작용 전으로 되돌리는 함수
    /// </summary>
    public void InteractionRestore()
    {
        isInteracting = false;
        playerBody.GetComponent<TPSCharacterManipulation>().enabled = true;
        playerBody.parent.GetComponent<ZoomCamera>().setTPS();
        Destroy(conversationGameObject);
    }
}
