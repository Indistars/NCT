using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;     //1분당 비트 수
    double currentTime = 0d;     //노트 생성을 체크해줄 시간(float 보다 오차가 적은 double)

    [SerializeField] Transform tfNoteAppear = null;     //노트가 생성될 위치
    [SerializeField] GameObject goNote = null;                 //노트 프리팹 

    TimingManager timingManager;

    // Start is called before the first frame update
    void Awake()
    {
        timingManager = GetComponent<TimingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        //60s / BPM = 1 Beat 시간
        if(currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position ,Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            t_note.transform.localScale = Vector3.one;
            timingManager.boxNoteList.Add(t_note);
            currentTime -= 60d / bpm;        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            timingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
