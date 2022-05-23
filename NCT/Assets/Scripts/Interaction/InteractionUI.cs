using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Text conversationText; // ��ȭ Text
    [SerializeField] private Text conversationName; // ��ȭ �̸� Text

    private void Awake()
    {
        conversationText.text = "�ȳ��ϼ���";
        conversationName.text = "Null";
    }

    /// <summary>
    /// ���� ��ư ������ �� ����Ǵ� �Լ�
    /// </summary>
    public void OnNextButton()
    {
        GameObject.Find("Interaction").GetComponent<Interaction>().InteractionRestore();
    }
}
