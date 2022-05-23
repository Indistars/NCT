using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactiveConversation : InteractionDataExtraction
{ 
    [SerializeField] protected Camera mainCamera; // 움직일 카메라
    [SerializeField] protected Transform playerBody; // 플레이어 모델을 관리할 변수 

    [SerializeField] protected GameObject conversationPanel; // 대화 UI 패널

    private GameObject conversationUIPrefab; // 대화 UI 패널

    private float cameraFieldOfValue; // 카메라 확대값

    protected bool isInteracting; // 상호작용 중인지 판별할 변수

    /// <summary>
    /// 상호작용 실행 함수
    /// </summary>
    public void Interacting()
    {
        isInteracting = true; // 상호작용 중
        playerBody.parent.GetComponent<TPSCharacterManipulation>().enabled = false; // TPS 캐릭터 비활성화
        playerBody.GetComponent<Animator>().SetFloat("MoveSpeed", 0); // 애니메이션 멈춤
        cameraFieldOfValue = mainCamera.fieldOfView; // 카메라 fieldOfView 값 할당
        mainCamera.transform.SetParent(interactiveObject.transform.GetChild(2), false); // 3인칭 변경
        // playerBody.parent.GetComponent<ZoomCamera>().setFPS();
        conversationUIPrefab = Instantiate(conversationPanel); // 대화 패널 생성
        conversationUIPrefab.transform.SetParent(GameObject.Find("Canvas").transform, false); // 대화 패널 위치 할당
    }

    /// <summary>
    /// 상호작용 전으로 되돌리는 함수
    /// </summary>
    public void InteractionRestore()
    {
        isInteracting = false; // 상호작용 종료
        playerBody.parent.GetComponent<TPSCharacterManipulation>().enabled = true; // TPS 캐릭터 활성화
        playerBody.parent.GetComponent<ZoomCamera>().setTPS(cameraFieldOfValue); // TPS 방식으로 변경
        Destroy(conversationUIPrefab); // 대화 패널 제거
    }
}
