using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // 움직일 카메라

    [SerializeField] private GameObject FPS; // 1인칭 관점
    [SerializeField] private GameObject TPS; // 3인칭 관점

    [SerializeField] private float zoomSpeed; // 줌 속도
    [SerializeField] private float zoomMax;   // 줌 최대값
    [SerializeField] private float zoomMin;   // 줌 최솟값(1인칭)

    private void Update()
    {
        float mouseWheelForce = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed; // 마우스 휠 스크롤 값

        if (mouseWheelForce != 0 && mainCamera.transform.parent == TPS.transform) // 마우스 휠을 움직였고, 3인칭 관점일 때
        {
            if (mainCamera.fieldOfView < zoomMax || mouseWheelForce < 0) // 줌 최대값을 넘지 않았을 때
            {
                mainCamera.fieldOfView += mouseWheelForce * zoomSpeed; // 줌 값 추가
            }

            if (mainCamera.fieldOfView <= zoomMin) // 줌 최솟값
            {
                setFPS(); // FPS 변경
            }
        }

        else if (mainCamera.transform.parent == FPS.transform && mouseWheelForce > 0) // 마우스 휠을 뒤로 했고, 1인칭 관점일 때
        {
            setTPS(zoomMin); // TPS 변경
        }
    }

    /// <summary>
    /// Camera를 FPS 방식으로 변경하는 함수
    /// </summary>
    public void setFPS()
    {
        mainCamera.fieldOfView = 60; // fieldOfView의 기본값
        mainCamera.transform.SetParent(FPS.transform, false); // 1인칭 변경
        // mainCamera.transform.localPosition = new Vector3(0, 0, 0.5f); // 1인칭 변경
    }

    /// <summary>
    /// Camera를 TPS 방식으로 변경하는 함수
    /// </summary>
    /// <param name="fieldOfView">커스텀 fieldOfView</param>
    public void setTPS(float fieldOfView)
    {
        mainCamera.fieldOfView = fieldOfView; // 3인칭 관점에서 최소 확대값으로 변경
        mainCamera.transform.SetParent(TPS.transform, false); // 3인칭 변경
        // mainCamera.transform.localPosition = new Vector3(0, 0, -4f); // 3인칭 변경
    }
}
