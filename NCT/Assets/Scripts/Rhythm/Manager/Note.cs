using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400f;

    private void Update()
    {
        //localPosition���� ���� ������ Canvas������ ������
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    } 
}
