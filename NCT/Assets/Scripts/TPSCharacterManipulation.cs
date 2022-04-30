using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterManipulation : MonoBehaviour
{
    [SerializeField] private Transform playerBody; // 플레이어 모델을 관리할 변수
    [SerializeField] private Transform cameraArm;  // 카메라 암 회전을 관리할 변수

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = playerBody.GetComponent<Animator>(); // 플레이어 애니메이터 할당
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // 이동 입력 값을 가져옴
        bool isMove = moveInput.magnitude != 0;
        animator.SetBool("isMove", isMove); // 이동 입력 값의 유무를 애니메이션에 적용
        if (isMove) // 움직이고 있을 때 실행되는 함수
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized; // 카메라의 정면을 평면화 시켜 저장
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized; // 카메라의 오른쪽을 평면화 시켜 저장
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; // 카메라를 통해 이동 방향 저장

            // characterBody.forward = lookForward; // 캐릭터가 이동할 때 카메라가 바라보는 방향 바라보게 하기
            playerBody.forward = moveDir; // 캐릭터가 이동할 때 이동 방향을 바라보게 하기 
            transform.position += moveDir * Time.deltaTime * 5f;
        }
    }

    /// <summary>
    /// 카메라의 회전을 담당하는 함수
    /// </summary>
    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // 프로그래밍에서 이전값과 현재값의 차를 주로 "Delta"라는 용어로 표현함
        Vector3 camAngle = cameraArm.rotation.eulerAngles; // 카메라의 암의 Rotation 값을 오일러 각으로 변환
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f) // 플레이어의 상하 움직임 제한
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z); // 카메라의 Rotation 값 변환
    }
}
