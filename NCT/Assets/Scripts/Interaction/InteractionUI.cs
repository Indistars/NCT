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

    private void Awake()
    {
        interactionName.text = DataBaseManager.Instance.tdAnimalDict[10001].Name; // tdAnimal �ҷ�����
        interactionText.text = DataBaseManager.Instance.tdAnimalDict[10001].Order_Comment[0]; // ù �ֹ� ���� �ҷ�����
    }

    /// <summary>
    /// ���� ��ư ������ �� ����Ǵ� �Լ�
    /// </summary>
    public void OnNextButton()
    {
        if (interactionIndex != DataBaseManager.Instance.tdAnimalDict[10001].Order_Comment.Count) // ���� ��ȭ�� ������ ��ȭ�� �ƴҰ��
        {
            interactionText.text = DataBaseManager.Instance.tdAnimalDict[10001].Order_Comment[++interactionIndex]; // ���� �ֹ� ���� �ҷ�����

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
        InteractionTrigger.Instance.InteractionRestore(); // ��ȣ�ۿ� ����
        // orderFormUIPanel.SetActive(true); // �ֹ� UI ���� �� �߰� ���� ��
    }
}