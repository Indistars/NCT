using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RhythmTrack {

    [SerializeField] string name;
    public Transform[] transforms;

}


public class RhythmMainNote : MonoBehaviour
{
    public RhythmTrack[] Tracks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
