using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Text interactionName; // 이름 Text
    [SerializeField] private Text interactionText; // 대화 Text

    [SerializeField] private GameObject orderFormUIPanel; // 주문 UI 패널

    private int interactionIndex = 0; // 대화 index 저장 변수

    private void Awake()
    {
        interactionName.text = DataBaseManager.Instance.tdAnimalDict[10001].Name; // tdAnimal 불러오기
        interactionText.text = DataBaseManager.Instance.tdAnimalDict[10001].Order_Comment[0]; // 첫 주문 내용 불러오기
    }

    /// <summary>
    /// 다음 버튼 눌렀을 때 실행되는 함수
    /// </summary>
    public void OnNextButton()
    {
        if (interactionIndex != DataBaseManager.Instance.tdAnimalDict[10001].Order_Comment.Count) // 현재 대화가 마지막 대화가 아닐경우
        {
            interactionText.text = DataBaseManager.Instance.tdAnimalDict[10001].Order_Comment[++interactionIndex]; // 다음 주문 내용 불러오기

        }
        else // 현재 대화가 마지막 대화일 경우
        {
            StartOrder(); // 대화를 다했으면 주문 시작
        }
    }

    /// <summary>
    /// 주문을 시작하는 함수
    /// </summary>
    public void StartOrder()
    {
        InteractionTrigger.Instance.InteractionRestore(); // 상호작용 복구
        // orderFormUIPanel.SetActive(true); // 주문 UI 시작 ※ 추가 예정 ※
    }
}