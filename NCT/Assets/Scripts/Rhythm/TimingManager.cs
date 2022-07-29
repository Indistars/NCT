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
        #region 타이밍 박스 설정
        timingBoxes = new Vector2[timingRect.Length];   
        for (int i = 0; i < timingRect.Length; i++)
        {
            //각각의 판정 범위 =>
            //최소값 = 중심 - (이미지의 너비 / 2)
            //최대값 = 중심 + (이미지의 너비 / 2)

            timingBoxes[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                                Center.localPosition.x + timingRect[i].rect.width / 2);
            #endregion

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 리스트에 있는 노트들을 확인해서 판정 박스에 있는 노트를 찾아야함
    /// </summary>
    public void CheckTimingNote()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for (int x = 0; x < timingBoxes.Length; x++)
            {
                if (timingBoxes[x].x <= t_notePosX && t_notePosX <= timingBoxes[x].y)
                {
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);
                    Debug.Log("Hit" + x);
                    return;
                }
            }
        }

        Debug.Log("Miss");
    }
}
