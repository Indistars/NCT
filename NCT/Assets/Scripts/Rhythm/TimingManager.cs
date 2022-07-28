using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    //생성된 노트를 담는 List => 판정 범위에 있는 모든 노트를 비교해야함
    public List<GameObject> boxNoteList = new List<GameObject> ();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;     //판정 범위(Perfact, Cool, Good, Bad)
    Vector2[] timingBoxes = null;                           //판정범위의 최소값(x), 최대값(y)

    // Start is called before the first frame update
    void Start()
    {
        timingBoxes = new Vector2[timingRect.Length];   
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxes[i].Set(Center.localPosition.x,
                                Center.localPosition.x)
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
