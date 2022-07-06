using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Text interactionName; // �̸� Text
    [SerializeField] private Text interactionText; // ��ȭ Text

    [SerializeField] private GameObject orderFormUIPanel; // �ֹ� UI �г�

    private int interactionIndex = 0; // ��ȭ index ���� ����

    TDAnimal tdAnimal;

    enum CommentState // ��ȭ ����
    {
        InteractComment, // ��ȣ�ۿ� ����
        SuccessComment,  // �ֹ� ���� ����
        FaildComment,    // �ֹ� ���� ����
    }

    CommentState commentState;

    private void Awake()
    {
        tdAnimal = DataBaseManager.Instance.tdAnimalDict[10001]; // ���� �ʱ�ȭ �� �߰� ���� ��
        commentState = CommentState.InteractComment; // �ֹ� ��ȭ
    }

    private void OnEnable()
    {
        interactionName.text = tdAnimal.Name; // tdAnimal �ҷ�����
        interactionText.text = tdAnimal.Order_Comment[interactionIndex]; // ù �ֹ� ���� �ҷ�����
    }

    /// <summary>
    /// ���� ��ư ������ �� ����Ǵ� �Լ�
    /// </summary>
    public void OnNextButton()
    {
        if (commentState == CommentState.SuccessComment) // �ֹ��� �������� ��
        {
            if (tdAnimal.Success_Comment.Count > 1 && ++interactionIndex < tdAnimal.Success_Comment.Count) // ���� ��ȭ�� ������ ��ȭ�� �ƴҰ��
                interactionText.text = tdAnimal.Success_Comment[interactionIndex]; // ���� �ֹ� ���� �ҷ�����
            else // ���� ��ȭ�� �������� ���
            {
                StartCook(); // �丮 ����
                Init(); // �ʱ�ȭ
            }
        }

        else if (commentState == CommentState.FaildComment) // �ֹ��� �������� �� 
        {
            if (tdAnimal.Failed_Comment.Count > 1 && ++interactionIndex < tdAnimal.Failed_Comment.Count) // ���� ��ȭ�� ������ ��ȭ�� �ƴҰ��
                interactionText.text = tdAnimal.Failed_Comment[interactionIndex]; // ���� �ֹ� ���� �ҷ�����
            else // ���� ��ȭ�� �������� ���
            { 
                Init(); // �ʱ�ȭ
                InteractionTrigger.Instance.InteractionRestore(); // ��ȣ�ۿ� ����
            }
        }

        else if (commentState == CommentState.InteractComment) // ��ȣ�ۿ��� ��
        {
            if (tdAnimal.Order_Comment.Count > 1 && ++interactionIndex < tdAnimal.Order_Comment.Count) // ���� ��ȭ�� ������ ��ȭ�� �ƴҰ��
                interactionText.text = tdAnimal.Order_Comment[interactionIndex]; // ���� �ֹ� ���� �ҷ�����
            else // ���� ��ȭ�� �������� ���
                StartOrder(); // ��ȭ�� �������� �ֹ� ����
        }
    }

    /// <summary>
    /// UI �ʱ�ȭ �Լ�
    /// </summary>
    private void Init()
    {
        interactionIndex = 0; // ��ȭ index �ʱ�ȭ
        gameObject.SetActive(false); // ��ȭ �г� ��Ȱ��ȭ
        commentState = CommentState.InteractComment; // ��ȣ�ۿ�
    }

    /// <summary>
    /// �ֹ��� �����ϴ� �Լ�
    /// </summary>
    public void StartOrder()
    {
        Init();
        orderFormUIPanel.SetActive(true); // �ֹ� UI ���� �� �߰� ���� ��
    }

    /// <summary>
    /// �丮�� �����ϴ� �Լ�
    /// </summary>
    public void StartCook()
    {
        Init();
        SceneManager.LoadScene("RhythmTestScene"); // ������� �� �ε�
    }

    /// <summary>
    /// �ֹ� ���� �Լ�
    /// </summary>
    public void SetFaildOrder()
    {
        gameObject.SetActive(true);
        interactionText.text = tdAnimal.Failed_Comment[interactionIndex];
        commentState = CommentState.FaildComment; // 
    }

    /// <summary>
    /// �ֹ� ���� �Լ�
    /// </summary>
    public void SetSuccessOrder()
    {
        gameObject.SetActive(true);
        interactionText.text = tdAnimal.Success_Comment[interactionIndex];
        commentState = CommentState.SuccessComment;
    }
}