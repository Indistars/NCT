using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileResolution : MonoBehaviour
{
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)Screen.height) / ((float)16 / 9); // (세로 / 가로)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1) // 화면의 위, 아래가 비었을 경우
        {
            rect.height = scaleheight; // height 조정
            rect.y = (1 - scaleheight) / 2f; // y 좌표 조정
        }
        else // 화면의 좌, 우가 비었을 경우
        {
            rect.width = scalewidth; // width 조정
            rect.x = (1f - scalewidth) / 2f; // x 좌표 조정
        }
        camera.rect = rect; // rect 적용
    }
}
