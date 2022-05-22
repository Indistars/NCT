using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactiveConversation : InteractionDataExtraction
{
    [SerializeField] protected Camera mainCamera; // ������ ī�޶�
    [SerializeField] protected Transform playerBody; // �÷��̾� ���� ������ ���� 

    [SerializeField] protected GameObject conversationPanel; // ��ȭ UI �г�

    private GameObject conversationGameObject;

    private Vector3 cameraFieldOfValue; // ī�޶� Ȯ�밪

    protected bool isInteracting; // ��ȣ�ۿ� ������ �Ǻ��� ����

    /// <summary>
    /// ��ȣ�ۿ� ���� �Լ�
    /// </summary>
    public void Interacting()
    {
        isInteracting = true;
        playerBody.parent.GetComponent<TPSCharacterManipulation>().enabled = false;
        playerBody.parent.GetComponent<ZoomCamera>().setFPS();
        conversationGameObject = Instantiate(conversationPanel);
        conversationGameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }

    /// <summary>
    /// ��ȣ�ۿ� ������ �ǵ����� �Լ�
    /// </summary>
    public void InteractionRestore()
    {
        isInteracting = false;
        playerBody.GetComponent<TPSCharacterManipulation>().enabled = true;
        playerBody.parent.GetComponent<ZoomCamera>().setTPS();
        Destroy(conversationGameObject);
    }
}
