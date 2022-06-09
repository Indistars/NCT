using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Text interactionName; // �̸� Text
    [SerializeField] private Text interactionText; // ��ȭ Text

    [SerializeField] private GameObject orderFormUIPanel; // �ֹ��� UI �г�

    private void Awake()
    {
        interactionName.text = DataBaseManager.Instance.tdAnimalDict[10001].Name; // tdAnimal �ҷ�����
        interactionText.text = "���� ��ư�� ���� �ֹ��� �޾��ּ���."; // �ӽ� ��ȭ
    }

    /// <summary>
    /// ���� ��ư ������ �� ����Ǵ� �Լ�
    /// </summary>
    public void OnNextButton()
    { 
        // DB���� ��ȭ ������ �޾ƿ��� �ʿ�.
        SetOrderFormUIPanel(); // ��ȭ�� �������� �ֹ��� UI �г� ���� 
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
