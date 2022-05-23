using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactiveConversation : InteractionDataExtraction
{ 
    [SerializeField] protected Camera mainCamera; // ������ ī�޶�
    [SerializeField] protected Transform playerBody; // �÷��̾� ���� ������ ���� 

    [SerializeField] protected GameObject conversationPanel; // ��ȭ UI �г�

    private GameObject conversationUIPrefab; // ��ȭ UI �г�

    private float cameraFieldOfValue; // ī�޶� Ȯ�밪

    protected bool isInteracting; // ��ȣ�ۿ� ������ �Ǻ��� ����

    /// <summary>
    /// ��ȣ�ۿ� ���� �Լ�
    /// </summary>
    public void Interacting()
    {
        isInteracting = true; // ��ȣ�ۿ� ��
        playerBody.parent.GetComponent<TPSCharacterManipulation>().enabled = false; // TPS ĳ���� ��Ȱ��ȭ
        playerBody.GetComponent<Animator>().SetFloat("MoveSpeed", 0); // �ִϸ��̼� ����
        cameraFieldOfValue = mainCamera.fieldOfView; // ī�޶� fieldOfView �� �Ҵ�
        mainCamera.transform.SetParent(interactiveObject.transform.GetChild(2), false); // 3��Ī ����
        // playerBody.parent.GetComponent<ZoomCamera>().setFPS();
        conversationUIPrefab = Instantiate(conversationPanel); // ��ȭ �г� ����
        conversationUIPrefab.transform.SetParent(GameObject.Find("Canvas").transform, false); // ��ȭ �г� ��ġ �Ҵ�
    }

    /// <summary>
    /// ��ȣ�ۿ� ������ �ǵ����� �Լ�
    /// </summary>
    public void InteractionRestore()
    {
        isInteracting = false; // ��ȣ�ۿ� ����
        playerBody.parent.GetComponent<TPSCharacterManipulation>().enabled = true; // TPS ĳ���� Ȱ��ȭ
        playerBody.parent.GetComponent<ZoomCamera>().setTPS(cameraFieldOfValue); // TPS ������� ����
        Destroy(conversationUIPrefab); // ��ȭ �г� ����
    }
}
