using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ����� ��ũ��Ʈ
/// </summary>
public class FeatureTest : MonoBehaviour
{
    string log;


    [Header("Test Button List")]
    public List<TestButton> Buttons;


    [Serializable]
    public class TestButton
    {
        public string text;
        public UnityEvent onClick;
    }


    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * 3);


        foreach (var button in Buttons)
        {
            if (GUILayout.Button(button.text))
            {
                button.onClick?.Invoke();
            }
        }


        GUILayout.Label(log);
    }
}