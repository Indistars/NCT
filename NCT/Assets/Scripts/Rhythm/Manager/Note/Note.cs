using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400f;

    protected UnityEngine.UI.Image noteImage;

    private void Awake()
    {
        noteImage = GetComponent<UnityEngine.UI.Image>();
    }

    private void Update()
    {
        //localPosition으로 하지 않으면 Canvas내에서 움직임
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    } 

    public void HideNote()
    {
        noteImage.enabled = false;
    }
}
