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
