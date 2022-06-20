using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Text interactionName; // �̸� Text
    [SerializeField] private Text interactionText; // ��ȭ Text

    [SerializeField] private GameObject orderFormUIPanel; // �ֹ� UI �г�

    private int interactionIndex = 0; // ��ȭ index ���� ����

    TDAnimal tdAnimal;

    private void Awake()
    {
        tdAnimal = DataBaseManager.Instance.tdAnimalDict[10001]; // ���� �ʱ�ȭ �� �߰� ���� ��
        interactionName.text = tdAnimal.Name; // tdAnimal �ҷ�����
        interactionText.text = tdAnimal.Order_Comment[interactionIndex]; // ù �ֹ� ���� �ҷ�����
    }

    /// <summary>
    /// ���� ��ư ������ �� ����Ǵ� �Լ�
    /// </summary>
    public void OnNextButton()
    {
        if (tdAnimal.Order_Comment.Count > 1 && ++interactionIndex < tdAnimal.Order_Comment.Count) // ���� ��ȭ�� ������ ��ȭ�� �ƴҰ��
        {
            interactionText.text = tdAnimal.Order_Comment[interactionIndex]; // ���� �ֹ� ���� �ҷ�����

        }
        else // ���� ��ȭ�� ������ ��ȭ�� ���
        {
            StartOrder(); // ��ȭ�� �������� �ֹ� ����
        }
    }

    /// <summary>
    /// �ֹ��� �����ϴ� �Լ�
    /// </summary>
    public void StartOrder()
    {
        interactionIndex = 0; // ��ȭ index �ʱ�ȭ
        // InteractionTrigger.Instance.InteractionRestore(); // ��ȣ�ۿ� ����
        gameObject.SetActive(false); // ��ȭ �г� ��Ȱ��ȭ
        orderFormUIPanel.SetActive(true); // �ֹ� UI ���� �� �߰� ���� ��
    }
}