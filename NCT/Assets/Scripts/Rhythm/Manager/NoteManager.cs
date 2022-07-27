using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;     //1�д� ��Ʈ ��
    double currentTime = 0d;     //��Ʈ ������ üũ���� �ð�(float ���� ������ ���� double)

    [SerializeField] Transform tfNoteAppear = null;     //��Ʈ�� ������ ��ġ
    [SerializeField] GameObject goNote = null;                 //��Ʈ ������ 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        //60s / BPM = 1 Beat �ð�
        if(currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position ,Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            currentTime -= 60d / bpm;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            Destroy(collision.gameObject);
        }
    }
}
