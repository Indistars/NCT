using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    //������ ��Ʈ�� ��� List => ���� ������ �ִ� ��� ��Ʈ�� ���ؾ���
    public List<GameObject> boxNoteList = new List<GameObject> ();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;     //���� ����(Perfact, Cool, Good, Bad)
    Vector2[] timingBoxes = null;                           //���������� �ּҰ�(x), �ִ밪(y)


    // Start is called before the first frame update
    void Start()
    {
        #region Ÿ�̹� �ڽ� ����
        timingBoxes = new Vector2[timingRect.Length];   
        for (int i = 0; i < timingRect.Length; i++)
        {
            //������ ���� ���� =>
            //�ּҰ� = �߽� - (�̹����� �ʺ� / 2)
            //�ִ밪 = �߽� + (�̹����� �ʺ� / 2)

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
    /// ����Ʈ�� �ִ� ��Ʈ���� Ȯ���ؼ� ���� �ڽ��� �ִ� ��Ʈ�� ã�ƾ���
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
