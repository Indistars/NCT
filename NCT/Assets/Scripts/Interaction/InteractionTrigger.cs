using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTrigger : Singleton<InteractionTrigger>
{
    [SerializeField] private Camera mainCamera; // 움직일 카메라

    [SerializeField] private Transform playerBody; // 플레이어 모델 위치를 관리할 변수 

    [SerializeField] private GameObject interactionUIPanel; // 대화 UI 패널

    [SerializeField] private KeyCode interactionKeyCode; // 상호작용 키 코드(기본키 e)

    private GameObject interactiveObject; // 상호작용할 오브젝트를 넣어두는 임시 변수

    private float cameraFieldOfValue; // 카메라 확대값

    private TPSCharacterManipulation tpsCharacterManipulation; // tps 움직임 컴포넌트
    private ZoomCamera zoomCamera; // 줌 카메라 컴포넌트
    private Animator animator; // 플레이어 애니메이터 컴포넌트

    override protected void Awake()
    {
        base.Awake();
        tpsCharacterManipulation = playerBody.parent.GetComponent<TPSCharacterManipulation>(); // 컴포넌트 받아오기
        zoomCamera = playerBody.parent.GetComponent<ZoomCamera>(); // 컴포넌트 받아오기
        animator =  playerBody.GetComponent<Animator>(); // 컴포넌트 받아오기
    }

    private void Start()
    {
        DataBaseManager.Instance.LoadAnimalTable(); /* tdAnimal 불러오기, 파싱 ※나중에 게임 로딩창에서 사용 예정※ */
    }

    /// <summary>
    /// 상호작용을 시작하는 함수
    /// </summary>
    /// <param name="interactiveObject">상호작용할 오브젝트</param>
    public void StartInteraction(GameObject interactiveObject)
    {
        tpsCharacterManipulation.enabled = false; // TPS 캐릭터 비활성화
        animator.SetFloat("MoveSpeed", 0); // 플레이어 애니메이션 멈춤
        cameraFieldOfValue = mainCamera.fieldOfView; // 카메라 fieldOfView 값 할당
        mainCamera.transform.SetParent(interactiveObject.transform.GetChild(2), false); // 3인칭 변경
        interactionUIPanel.SetActive(true); // 대화 패널 활성화
    }

    /// <summary>
    /// 상호작용 전으로 복구하는 함수
    /// </summary>
    public void InteractionRestore()
    {
        tpsCharacterManipulation.enabled = true; // TPS 캐릭터 활성화
        zoomCamera.setTPS(cameraFieldOfValue); // TPS 방식으로 변경
    }

    private void OnTriggerStay(Collider collider)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward); // 캐릭터의 앞 방향 벡터
        // if (Physics.Raycast(transform.position, forward, 10) && collider.CompareTag("Animal")) // 캐릭터가 바라보고 있고, 동물일 경우
        if (collider.CompareTag("Animal")) // 동물일 때
        {
            collider.GetComponent<Outline>().enabled = true; // Outline 활성화

            if (Input.GetKeyDown(interactionKeyCode)) // 상호작용 키를 눌렀을 때
            {
                interactiveObject = collider.transform.parent.gameObject;  // 동물 모델 받아오기
                StartInteraction(interactiveObject); // 상호작용 시작
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Animal")) // 동물일 경우
        {
            collider.GetComponent<Outline>().enabled = false; // Outline 비활성화
            interactiveObject = null;  // 객체 초기화
        }
    }
}
