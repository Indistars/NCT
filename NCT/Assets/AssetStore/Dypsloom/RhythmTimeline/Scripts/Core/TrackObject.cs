/// ---------------------------------------------
/// Rhythm Timeline
/// Copyright (c) Dyplsoom. All Rights Reserved.
/// https://www.dypsloom.com
/// ---------------------------------------------

namespace Dypsloom.RhythmTimeline.Core
{
    using Dypsloom.RhythmTimeline.Core.Notes;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class TrackObject : MonoBehaviour
    {
        [Tooltip("노트를 생성할 시작 지점")]
        [SerializeField] protected Transform m_StartPoint;
        [Tooltip("The end point where the notes should be when they are perfect for the beat.")]
        [SerializeField] protected Transform m_EndPoint;
        [Tooltip("The 3D touch collider.")]
        [SerializeField] protected Collider m_TouchCollider3D;
        [Tooltip("The 2D touch collider.")]
        [SerializeField] protected Collider2D m_TouchCollider2D;
        [Tooltip("하나 이상의 노트가 활성화 된 이벤트")]
        [SerializeField] protected UnityEvent m_OnNoteActivate;
        [Tooltip("트랙에서 더이상 활성화 된 노트가 없는 이벤트")]
        [SerializeField] protected UnityEvent m_OnNoteDeactivate;

        public Transform StartPoint => m_StartPoint;
        public Transform EndPoint => m_EndPoint;

        protected List<Note> m_Notes;

        public Note CurrentNote => m_Notes == null || m_Notes.Count == 0 ? null : m_Notes[0];
        public Collider2D TouchCollider2D => m_TouchCollider2D;
        public Collider TouchCollider3D => m_TouchCollider3D;

        public void SetActiveNote(Note note)
        {
            if(m_Notes == null){ m_Notes = new List<Note>(); }
            m_Notes.Add(note);

            if (m_Notes.Count == 1) {
                m_OnNoteActivate.Invoke();
            }
        }

        public void RemoveActiveNote(Note note)
        {
            if(m_Notes == null || m_Notes.Contains(note) == false){return;}
            
            m_Notes.Remove(note);
            
            if (m_Notes.Count == 0) {
                m_OnNoteDeactivate.Invoke();
            }
        }

        /// <summary>
        /// Get the note direction at a specific normalized time 0->end, 1->start.
        /// </summary>
        /// <param name="t">The normalized time.</param>
        /// <returns></returns>
        public virtual Vector3 GetNoteDirection(float t)
        {
            return (EndPoint.position - StartPoint.position).normalized;
        }
    }
}