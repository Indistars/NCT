using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTrigger : Singleton<InteractionTrigger>
{
    [SerializeField] private Camera mainCamera; // ������ ī�޶�

    [SerializeField] private Transform playerBody; // �÷��̾� �� ��ġ�� ������ ���� 

    [SerializeField] private GameObject interactionUIPanel; // ��ȭ UI �г�

    [SerializeField] private KeyCode interactionKeyCode; // ��ȣ�ۿ� Ű �ڵ�(�⺻Ű e)

    private GameObject interactiveObject; // ��ȣ�ۿ��� ������Ʈ�� �־�δ� �ӽ� ����

    private float cameraFieldOfValue; // ī�޶� Ȯ�밪

    private void Start()
    {
        DataBaseManager.Instance.LoadAnimalTable(); /* tdAnimal �ҷ�����, �Ľ� �س��߿� ���� �ε�â���� ��� ������ */
    }

    /// <summary>
    /// ��ȣ�ۿ��� �����ϴ� �Լ�
    /// </summary>
    /// <param name="interactiveObject">��ȣ�ۿ��� ������Ʈ</param>
    public void StartInteraction(GameObject interactiveObject)
    {
        TPSCharacterManipulation.Instance.enabled = false; // TPS ĳ���� ��Ȱ��ȭ
        playerBody.GetComponent<Animator>().SetFloat("MoveSpeed", 0); // �÷��̾� �ִϸ��̼� ����
        cameraFieldOfValue = mainCamera.fieldOfView; // ī�޶� fieldOfView �� �Ҵ�
        mainCamera.transform.SetParent(interactiveObject.transform.GetChild(2), false); // 3��Ī ����
        interactionUIPanel.SetActive(true); // ��ȭ �г� Ȱ��ȭ
    }

    /// <summary>
    /// ��ȣ�ۿ� ������ �����ϴ� �Լ�
    /// </summary>
    public void InteractionRestore()
    {
        TPSCharacterManipulation.Instance.enabled = true; // TPS ĳ���� Ȱ��ȭ
        playerBody.parent.GetComponent<ZoomCamera>().setTPS(cameraFieldOfValue); // TPS ������� ����
        interactionUIPanel.SetActive(false); // ��ȭ �г� ��Ȱ��ȭ
    }

    private void OnTriggerStay(Collider collider)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward); // ĳ������ �� ���� ����
        if (Physics.Raycast(transform.position, forward, 10) && collider.CompareTag("Animal")) // ĳ���Ͱ� �ٶ󺸰� �ְ�, ������ ���
        {
            collider.GetComponent<Outline>().enabled = true; // Outline Ȱ��ȭ

            if (Input.GetKeyDown(interactionKeyCode)) // ��ȣ�ۿ� Ű�� ������ ��
            {
                interactiveObject = collider.transform.parent.gameObject;  // ���� �� �޾ƿ���
                StartInteraction(interactiveObject); // ��ȣ�ۿ� ����
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Animal")) // ������ ���
        {
            collider.GetComponent<Outline>().enabled = false; // Outline ��Ȱ��ȭ
            interactiveObject = null;  // ��ü �ʱ�ȭ
        }
    }
}
