using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // ������ ī�޶�

    [SerializeField] private GameObject FPS; // 1��Ī ����
    [SerializeField] private GameObject TPS; // 3��Ī ����

    [SerializeField] private float zoomSpeed; // �� �ӵ�
    [SerializeField] private float zoomMax;   // �� �ִ밪
    [SerializeField] private float zoomMin;   // �� �ּڰ�(1��Ī)

    private void Update()
    {
        float mouseWheelForce = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed; // ���콺 �� ��ũ�� ��

        if (mouseWheelForce != 0 && mainCamera.transform.parent == TPS.transform) // ���콺 ���� ��������, 3��Ī ������ ��
        {
            if (mainCamera.fieldOfView < zoomMax || mouseWheelForce < 0) // �� �ִ밪�� ���� �ʾ��� ��
            {
                mainCamera.fieldOfView += mouseWheelForce * zoomSpeed; // �� �� �߰�
            }

            if (mainCamera.fieldOfView <= zoomMin) // �� �ּڰ�
            {
                setFPS(); // FPS ����
            }
        }

        else if (mainCamera.transform.parent == FPS.transform && mouseWheelForce > 0) // ���콺 ���� �ڷ� �߰�, 1��Ī ������ ��
        {
            setTPS(zoomMin); // TPS ����
        }
    }

    /// <summary>
    /// Camera�� FPS ������� �����ϴ� �Լ�
    /// </summary>
    public void setFPS()
    {
        mainCamera.fieldOfView = 60; // fieldOfView�� �⺻��
        mainCamera.transform.SetParent(FPS.transform, false); // 1��Ī ����
        // mainCamera.transform.localPosition = new Vector3(0, 0, 0.5f); // 1��Ī ����
    }

    /// <summary>
    /// Camera�� TPS ������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="fieldOfView">Ŀ���� fieldOfView</param>
    public void setTPS(float fieldOfView)
    {
        mainCamera.fieldOfView = fieldOfView; // 3��Ī �������� �ּ� Ȯ�밪���� ����
        mainCamera.transform.SetParent(TPS.transform, false); // 3��Ī ����
        // mainCamera.transform.localPosition = new Vector3(0, 0, -4f); // 3��Ī ����
    }
}
