using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Text interactionName; // 이름 Text
    [SerializeField] private Text interactionText; // 대화 Text

    [SerializeField] private GameObject orderFormUIPanel; // 주문 UI 패널

    private int interactionIndex = 0; // 대화 index 저장 변수

    TDAnimal tdAnimal;

    enum CommentState // 대화 상태
    {
        InteractComment, // 상호작용 상태
        SuccessComment,  // 주문 수락 상태
        FaildComment,    // 주문 거절 상태
    }

    CommentState commentState;

    private void Awake()
    {
        tdAnimal = DataBaseManager.Instance.tdAnimalDict[10001]; // 동물 초기화 ※ 추가 예정 ※
        commentState = CommentState.InteractComment; // 주문 대화
    }

    private void OnEnable()
    {
        interactionName.text = tdAnimal.Name; // tdAnimal 불러오기
        interactionText.text = tdAnimal.Order_Comment[interactionIndex]; // 첫 주문 내용 불러오기
    }

    /// <summary>
    /// 다음 버튼 눌렀을 때 실행되는 함수
    /// </summary>
    public void OnNextButton()
    {
        if (commentState == CommentState.SuccessComment) // 주문을 수락했을 때
        {
            if (tdAnimal.Success_Comment.Count > 1 && ++interactionIndex < tdAnimal.Success_Comment.Count) // 현재 대화가 마지막 대화가 아닐경우
                interactionText.text = tdAnimal.Success_Comment[interactionIndex]; // 다음 주문 내용 불러오기
            else // 현재 대화가 마지막일 경우
            {
                StartCook(); // 요리 시작
                Init(); // 초기화
            }
        }

        else if (commentState == CommentState.FaildComment) // 주문을 거절했을 때 
        {
            if (tdAnimal.Failed_Comment.Count > 1 && ++interactionIndex < tdAnimal.Failed_Comment.Count) // 현재 대화가 마지막 대화가 아닐경우
                interactionText.text = tdAnimal.Failed_Comment[interactionIndex]; // 다음 주문 내용 불러오기
            else // 현재 대화가 마지막일 경우
            { 
                Init(); // 초기화
                InteractionTrigger.Instance.InteractionRestore(); // 상호작용 복구
            }
        }

        else if (commentState == CommentState.InteractComment) // 상호작용일 때
        {
            if (tdAnimal.Order_Comment.Count > 1 && ++interactionIndex < tdAnimal.Order_Comment.Count) // 현재 대화가 마지막 대화가 아닐경우
                interactionText.text = tdAnimal.Order_Comment[interactionIndex]; // 다음 주문 내용 불러오기
            else // 현재 대화가 마지막일 경우
                StartOrder(); // 대화를 다했으면 주문 시작
        }
    }

    /// <summary>
    /// UI 초기화 함수
    /// </summary>
    private void Init()
    {
        interactionIndex = 0; // 대화 index 초기화
        gameObject.SetActive(false); // 대화 패널 비활성화
        commentState = CommentState.InteractComment; // 상호작용
    }

    /// <summary>
    /// 주문을 시작하는 함수
    /// </summary>
    public void StartOrder()
    {
        Init();
        orderFormUIPanel.SetActive(true); // 주문 UI 시작 ※ 추가 예정 ※
    }

    /// <summary>
    /// 요리를 시작하는 함수
    /// </summary>
    public void StartCook()
    {
        Init();
        SceneManager.LoadScene("RhythmTestScene"); // 리듬게임 씬 로딩
    }

    /// <summary>
    /// 주문 거절 함수
    /// </summary>
    public void SetFaildOrder()
    {
        gameObject.SetActive(true);
        interactionText.text = tdAnimal.Failed_Comment[interactionIndex];
        commentState = CommentState.FaildComment; // 
    }

    /// <summary>
    /// 주문 수락 함수
    /// </summary>
    public void SetSuccessOrder()
    {
        gameObject.SetActive(true);
        interactionText.text = tdAnimal.Success_Comment[interactionIndex];
        commentState = CommentState.SuccessComment;
    }
}