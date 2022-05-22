using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Text conversationText;
    [SerializeField] private Text conversationName;

    private void Awake()
    {
        conversationText.text = "æ»≥Á«œººø‰";
        conversationName.text = "Null";
    }
}
