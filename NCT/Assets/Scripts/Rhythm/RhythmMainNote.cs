using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RhythmTrack {

    [SerializeField] string name;
    public Transform[] trans;

}


public class RhythmMainNote : MonoBehaviour
{
    public RhythmTrack[] Tracks;

    RhythmTrack m_rhythmTrack;
    int m_index;

    [SerializeField] float speed;
    bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        StartSong();        //�ӽ� ����
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
            PlaySong();
    }

    void StartSong()
    {
        transform.position = m_rhythmTrack.trans[m_index].position;
        isStart = true;
    }

    public void PlaySong()
    {
        transform.position = Vector2.MoveTowards    //���� ��Ʈ�� �̵�
            (transform.position, m_rhythmTrack.trans[m_index].transform.position, speed * Time.deltaTime);

        if (transform.position == m_rhythmTrack.trans[m_index].transform.position)  //���� ��Ʈ�� ���� ��Ʈ�� �̵� ���� ��
            m_index++;

    }
}
