using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Text conversationText; // 대화 Text
    [SerializeField] private Text conversationName; // 대화 이름 Text

    private void Awake()
    {
        conversationText.text = "안녕하세요";
        conversationName.text = "Null";
    }

    /// <summary>
    /// 다음 버튼 눌렀을 때 실행되는 함수
    /// </summary>
    public void OnNextButton()
    {
        GameObject.Find("Interaction").GetComponent<Interaction>().InteractionRestore();
    }
}
