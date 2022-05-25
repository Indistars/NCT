/// ---------------------------------------------
/// Rhythm Timeline
/// Copyright (c) Dyplsoom. All Rights Reserved.
/// https://www.dypsloom.com
/// ---------------------------------------------

namespace Dypsloom.RhythmTimeline.Core.Notes
{
	using Dypsloom.RhythmTimeline.Core.Input;
	using Dypsloom.RhythmTimeline.Core.Playables;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;

	/// <summary>
	/// 트리거 이벤트 정보를 저장하는데 사용
	/// </summary>
	public class NoteTriggerEventData
	{
		public Note Note;
		public InputEventData InputEventData;

		public bool Miss;
	
		public double TriggerDspTime;
		public double DspTimeDifference;
		public float DspTimeDifferencePercentage;

		public void SetTriggerData(InputEventData eventData, double dspTimeDiff, float dspTimeDiffPerc)
		{
			InputEventData = eventData;
			TriggerDspTime = DspTime.AdaptiveTime;
			DspTimeDifference = dspTimeDiff;
			DspTimeDifferencePercentage = dspTimeDiffPerc;
			Miss = false;
		}

		public void SetMiss()
		{
			InputEventData = null;
			TriggerDspTime = DspTime.AdaptiveTime;
			DspTimeDifference = 0;
			DspTimeDifferencePercentage = 100;
			Miss = true;
		}
	}

	/// <summary>
	/// 노트를 위한 상태
	/// </summary>
	public enum ActiveState
	{
		Disabled,	// 노트가 아직 초기화 되지않은 경우
		PreActive,	// 초기화 중인 노트와 활성 상태 사이
		Active,		// 노트가 활성 상태
		PostActive	// 노트가 비활성화되어 있지만 다시 초기화 되어있지 않은 상태
	}

	/// <summary>
	/// 노트 구성 요소의 기본 클래스
	/// </summary>
	public abstract class Note : MonoBehaviour
	{
		public event Action<NoteTriggerEventData> OnNoteTriggerEvent;
		public event Action<Note> OnInitialize;
		public event Action<Note> OnReset;
		public event Action<Note> OnActivate;
		public event Action<Note> OnDeactivate;

		[Tooltip("false로 설정하면 타임라인의 현재 시간 대신 리듬 디랙터 시작 DSP(디지털 신호 처리) 시간을 단일 트루로 사용하여 노트가 업데이트 됩니다.")]
		[SerializeField] protected bool m_UpdateWithTimeline = true;    //true로 해둘 것
		[Tooltip("false로 설정하면 노트가 시간과 함께 활성화되어 더 정확한 정보를 제공합니다.")]
		[SerializeField] protected bool m_ActivateWithClip = false;
		[Tooltip("노트의 회전 방향을 트랙 회전과 일치하도록 조정하겠습니까?")]
		[SerializeField] protected bool m_OrientToTrack = true;
		[Tooltip("클립이 실행될동안 활성화될 게임 개체")]
		[SerializeField] protected GameObject m_SetActiveWhileClipActive;
	
		protected RhythmClipData m_RhythmClipData;

		protected ActiveState m_ActiveState;
		protected bool m_Deactivated;
		protected bool m_IsTriggered;
		protected double m_ActualInitializeTime;
		protected double m_ActualActivateTime;

		protected NoteTriggerEventData m_NoteTriggerEventData;
	
		public ActiveState ActiveState => m_ActiveState;
		public bool IsTriggered => m_IsTriggered;
		public double TrueInitializeTime => m_RhythmClipData.ClipStart - RhythmClipData.RhythmDirector.SpawnTimeRange.x;
		public double TrueActivateTime => m_RhythmClipData.ClipStart;

		public double ActualInitializeTime => m_ActualInitializeTime;
		public double ActualActivateTime => m_ActualActivateTime;
		public double TimeFromActivate => CurrentTime - m_RhythmClipData.ClipStart;
		public double TimeFromDeactivate => CurrentTime - m_RhythmClipData.ClipEnd;

		public RhythmClipData RhythmClipData => m_RhythmClipData;

		public double CurrentTime => 
			m_UpdateWithTimeline || Application.isPlaying == false
				? m_RhythmClipData.RhythmDirector.PlayableDirector.time
				: DspTime.AdaptiveTime - m_RhythmClipData.RhythmDirector.DspSongStartTime;

		/// <summary>
		/// Create a cache the event data on awake.
		/// </summary>
		protected virtual void Awake()
		{
			m_NoteTriggerEventData = new NoteTriggerEventData();
			m_NoteTriggerEventData.Note = this;
		}

		protected virtual void Start()
		{ }

		/// <summary>
		/// 리듬클립데이터를 사용하여 노트를 초기화 합니다
		/// </summary>
		/// <param name="rhythmClipData">리듬클립데이터</param>
		public virtual void Initialize(RhythmClipData rhythmClipData)
		{
			if (rhythmClipData.TrackObject == null) {
				Debug.LogWarning("The Track Object cannot be null",gameObject);
				return;
			}
			
			m_RhythmClipData = rhythmClipData;

			if (m_OrientToTrack) {
				transform.rotation = rhythmClipData.TrackObject.StartPoint.rotation;
			} else {
				transform.right = Vector3.left;
			}

			m_IsTriggered = false;
			m_ActiveState = ActiveState.PreActive;

			m_ActualInitializeTime = CurrentTime;
			m_ActualActivateTime =  -1;
			
			InvokeOnInitialize();
		}

		/// <summary>
		/// Invoke the On Initialize event.
		/// </summary>
		protected virtual void InvokeOnInitialize()
		{
			OnInitialize?.Invoke(this);
		}

		/// <summary>
		/// 노트를 pool로 반환할때 재설정
		/// </summary>
		public virtual void Reset()
		{
			m_ActiveState = ActiveState.Disabled;
			InvokeOnReset();
		}

		/// <summary>
		/// Invoked the on reset event.
		/// </summary>
		protected virtual void InvokeOnReset()
		{
			OnReset?.Invoke(this);
		}

		/// <summary>
		/// The clip started.
		/// </summary>
		public virtual void OnClipStart()
		{
			if (m_ActivateWithClip) {
				ActivateNote();
			}
		}

		/// <summary>
		/// The clip stopped.
		/// </summary>
		public virtual void OnClipStop()
		{
			if (m_ActivateWithClip) {
				DeactivateNote();
			}
		}

		/// <summary>
		/// 노트는 트리거 될 수 있는 범위 내에 있으므로 활성화해야 합니다.
		/// 보통 클립이 시작될 때 발생합니다. 
		/// </summary>
		protected virtual void ActivateNote()
		{
			m_ActiveState = ActiveState.Active;
			RhythmClipData.TrackObject.SetActiveNote(this);
			if (m_SetActiveWhileClipActive != null) {
				m_SetActiveWhileClipActive.SetActive(true);
			}
			m_ActualActivateTime = CurrentTime;
			InvokeOnActivate();
		}
		
		/// <summary>
		/// Invoke the on activate event.
		/// </summary>
		protected virtual void InvokeOnActivate()
		{
			OnActivate?.Invoke(this);
		}

		/// <summary>
		/// 노트가 비활성화 되었다.
		/// </summary>
		protected virtual void DeactivateNote()
		{
			m_ActiveState = ActiveState.PostActive;
			RhythmClipData.TrackObject.RemoveActiveNote(this);
			if (m_SetActiveWhileClipActive != null) {
				m_SetActiveWhileClipActive.SetActive(false);
			}

			InvokeOnDeactivate();
		}
		
		/// <summary>
		/// Invoke the on deactivate event.
		/// </summary>
		protected virtual void InvokeOnDeactivate()
		{
			OnDeactivate?.Invoke(this);
		}

		/// <summary>
		/// Trigger an input on the note.
		/// </summary>
		/// <param name="inputEventData">The input event data.</param>
		public abstract void OnTriggerInput(InputEventData inputEventData);

		/// <summary>
		/// Invoke note trigger event.
		/// </summary>
		protected virtual void InvokeNoteTriggerEvent()
		{
			OnNoteTriggerEvent?.Invoke(m_NoteTriggerEventData);
		}
	
		/// <summary>
		/// Invoke the note trigger missed event.
		/// </summary>
		protected virtual void InvokeNoteTriggerEventMiss()
		{
			m_NoteTriggerEventData.SetMiss();
			InvokeNoteTriggerEvent();
		}
	
		/// <summary>
		/// Trigger a note trigger event with the offset.
		/// </summary>
		/// <param name="eventData">The input event data.</param>
		/// <param name="dspTimeDiff">The offset from a perfect hit.</param>
		/// <param name="dspTimeDiffPerc">The offset from a perfect hit in percentage.</param>
		protected virtual void InvokeNoteTriggerEvent(InputEventData eventData, double dspTimeDiff, float dspTimeDiffPerc)
		{
			m_NoteTriggerEventData.SetTriggerData(eventData,dspTimeDiff,dspTimeDiffPerc);
			InvokeNoteTriggerEvent();
		}

		/// <summary>
		/// 타임라인을 업데이트, 모든 프레임 및 편집 모드도 업데이트
		/// </summary>
		/// <param name="globalClipStartTime">The offset to the clip start time.</param>
		/// <param name="globalClipEndTime">The offset to the clip stop time</param>
		public virtual void TimelineUpdate(double globalClipStartTime, double globalClipEndTime)
		{
			if(m_UpdateWithTimeline == false && Application.isPlaying == false){return;}

			if (m_ActivateWithClip == false) {
				if (m_ActiveState == ActiveState.Active && globalClipEndTime >= 0) {
					DeactivateNote();
				} else if(m_ActiveState == ActiveState.PreActive && globalClipStartTime >= 0) {
					ActivateNote();
				}
			}
		
			HybridUpdate( globalClipStartTime, globalClipEndTime );
		}
	
		/// <summary>
		/// 시간 표시줄 업데이트 대신 기본 업데이트를 사용하여 DSP 적응 시간과 동기화 할 수 있습니다.
		/// </summary>
		protected virtual void Update()
		{
			if(m_UpdateWithTimeline){return;}
		
			//Debug.Log("time line update dsp"+m_DspGlobalStartTime +" end "+m_DspGlobalEndTime);
			if (m_ActivateWithClip == false) {
				if (m_ActiveState == ActiveState.Active && TimeFromDeactivate >= 0) {
					DeactivateNote();
				} else if(m_ActiveState == ActiveState.PreActive && TimeFromActivate >= 0) {
					ActivateNote();
				}
			}
		
			HybridUpdate(TimeFromActivate, TimeFromDeactivate);
		}

		/// <summary>
		/// 하이브리드 업데이트는 재생모드와 편집 모드에서 모두 작동합니다.
		/// </summary>
		/// <param name="timeFromStart">The offset before the start.</param>
		/// <param name="timeFromEnd">The offset before the end.</param>
		protected abstract void HybridUpdate(double timeFromStart, double timeFromEnd);

	}
}