using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    TimingManager timingManager;

    private void Awake()
    {
        timingManager = FindObjectOfType<TimingManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //판정 체크
            timingManager.CheckTimingNote();
        }
    }
}
