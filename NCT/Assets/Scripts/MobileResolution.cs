using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileResolution : MonoBehaviour
{
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)Screen.height) / ((float)16 / 9); // (���� / ����)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1) // ȭ���� ��, �Ʒ��� ����� ���
        {
            rect.height = scaleheight; // height ����
            rect.y = (1 - scaleheight) / 2f; // y ��ǥ ����
        }
        else // ȭ���� ��, �찡 ����� ���
        {
            rect.width = scalewidth; // width ����
            rect.x = (1f - scalewidth) / 2f; // x ��ǥ ����
        }
        camera.rect = rect; // rect ����
    }
}
