using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Text interactionName; // 이름 Text
    [SerializeField] private Text interactionText; // 대화 Text

    [SerializeField] private GameObject orderFormUIPanel; // 주문서 UI 패널

    private void Awake()
    {
        interactionName.text = DataBaseManager.Instance.tdAnimalDict[10001].Name; // tdAnimal 불러오기
        interactionText.text = "다음 버튼을 눌러 주문을 받아주세요."; // 임시 대화
    }

    /// <summary>
    /// 다음 버튼 눌렀을 때 실행되는 함수
    /// </summary>
    public void OnNextButton()
    { 
        // DB에서 대화 데이터 받아오기 필요.
        SetOrderFormUIPanel(); // 대화를 다했으면 주문서 UI 패널 생성 
    } 

    /// <summary>
    /// 
    /// </summary>
    public void SetOrderFormUIPanel()
    {
        InteractionTrigger.Instance.InteractionRestore();
        // orderFormUIPanel.SetActive(true);
    }
}
