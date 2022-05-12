using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // ������ ī�޶�

    [SerializeField] private float zoomSpeed; // �� �ӵ�
    [SerializeField] private float zoomMax;   // �� �ִ밪
    [SerializeField] private float zoomMin;   // �� �ּڰ�(1��Ī)

    private void Update()
    {
        float mouseWheelForce = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed; // ���콺 �� ��ũ�� ��

        if (mouseWheelForce != 0 && mainCamera.transform.localPosition.z == -4) // ���콺 ���� ��������, 3��Ī ������ ��
        {
            if (mainCamera.fieldOfView < zoomMax || mouseWheelForce < 0) // �� �ִ밪�� ���� �ʾ��� ��
            {
                mainCamera.fieldOfView += mouseWheelForce * zoomSpeed; // �� �� �߰�
            }

            if (mainCamera.fieldOfView <= zoomMin) // �� �ּڰ�
            {
                mainCamera.fieldOfView = 60; // fieldOfView�� �⺻��
                mainCamera.transform.localPosition = new Vector3(0, 0, 0.5f); // 1��Ī ����
            }
        }

        else if (mainCamera.transform.localPosition.z == 0.5f && mouseWheelForce > 0) // ���콺 ���� �ڷ� �߰�, 1��Ī ������ ��
        {
            mainCamera.fieldOfView = zoomMin; // 3��Ī �������� �ּ� Ȯ�밪���� ����
            mainCamera.transform.localPosition = new Vector3(0, 0, -4f); // 3��Ī ����
        }
    }
}
