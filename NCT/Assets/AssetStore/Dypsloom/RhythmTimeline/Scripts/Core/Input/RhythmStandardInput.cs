/// ---------------------------------------------
/// Rhythm Timeline
/// Copyright (c) Dyplsoom. All Rights Reserved.
/// https://www.dypsloom.com
/// ---------------------------------------------

namespace Dypsloom.RhythmTimeline.Core.Input
{
    using System;
    using Dypsloom.RhythmTimeline.Core.Managers;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Input event data tracks the input type.
    /// </summary>
    public class InputEventData
    {
        public int InputID;
        public int TrackID;
        public Vector2 Direction;

        public virtual bool Tap => InputID == 0;
        public virtual bool Release => InputID == 1;
        public virtual bool Swipe => InputID == 2;

        public InputEventData(int trackID, int inputID)
        {
            TrackID = trackID;
            InputID = inputID;
        }

    }

    /// <summary>
    /// 키 또는 단추 입력 관리하는 간단한 추상화 클래스
    /// </summary>
    [Serializable]
    public struct SimpleInput
    {
        [SerializeField] private KeyCode m_Key;
        [SerializeField] private string m_Button;

        public SimpleInput(KeyCode key, string button = null)
        {
            m_Key = key;
            m_Button = button;
        }
        
        public bool GetInputDown()
        {
            var input = false;
            
            if (m_Key != KeyCode.None) {
                input |= Input.GetKeyDown(m_Key);
            }

            if (string.IsNullOrWhiteSpace(m_Button) == false) {
                input |= Input.GetButtonDown(m_Button);
            }

            return input;
        }
        
        public bool GetInputUp()
        {
            var input = false;
            
            if (m_Key != KeyCode.None) {
                input |= Input.GetKeyUp(m_Key);
            }

            if (string.IsNullOrWhiteSpace(m_Button) == false) {
                input |= Input.GetButtonUp(m_Button);
            }

            return input;
        }
        
        public bool GetInput()
        {
            var input = false;
            
            if (m_Key != KeyCode.None) {
                input |= Input.GetKey(m_Key);
            }

            if (string.IsNullOrWhiteSpace(m_Button) == false) {
                input |= Input.GetButton(m_Button);
            }

            return input;
        }
    }

    /// <summary>
    /// Gets information from the RhythmDirector and from the input to processes notes.
    /// </summary>
    public class RhythmStandardInput : MonoBehaviour
    {
        public const int c_MouseTouchFingerID = -1;
        
        [Tooltip("The Rhythm Processor.")]
        [SerializeField] protected RhythmProcessor m_RhythmProcessor;
        [Tooltip("The Key input for each track.")]
        [SerializeField] protected SimpleInput[] m_TrackInput;
        [Tooltip("The key code to swipe left.")]
        [SerializeField] protected SimpleInput m_SwipeLeft = new SimpleInput(KeyCode.LeftArrow);
        [Tooltip("The key code to swipe right.")]
        [SerializeField] protected SimpleInput m_SwipeRight = new SimpleInput(KeyCode.RightArrow);
        [Tooltip("The key code to swipe up.")]
        [SerializeField] protected SimpleInput m_SwipeUp = new SimpleInput(KeyCode.UpArrow);
        [Tooltip("The key code to swipe down.")]
        [SerializeField] protected SimpleInput m_SwipeDown = new SimpleInput(KeyCode.DownArrow);
        [Tooltip("Use the 2D or 3D Collider of the Track Object for touch detection?")]
        [SerializeField] protected bool m_2DTouchCollider = false;
        [Tooltip("The layer mask for the touch input collider.")]
        [SerializeField] protected LayerMask m_TouchInputMask;
        [Tooltip("The threshold movement for detecting a swipe.")]
        [SerializeField] protected float m_SwipeThreshold = 20;
        [Tooltip("Check for swipes only when releasing the input?")]
        [SerializeField] protected bool m_DetectSwipeOnlyAfterRelease = false;

        protected InputEventData[] m_TrackInputEventData;
        protected Camera m_Camera;
        protected Dictionary<int, Vector2> m_TouchBeganPosition;
        protected Dictionary<int, Vector2> m_TouchEndedPosition;
        protected Dictionary<int, int> m_TouchToTrackMap;

        protected virtual void Awake()
        {
            m_Camera = Camera.main;

            m_TrackInputEventData = new InputEventData[m_TrackInput.Length];
            for (int i = 0; i < m_TrackInputEventData.Length; i++) {
                m_TrackInputEventData[i] = new InputEventData(i, -1);
            }

            m_TouchBeganPosition = new Dictionary<int, Vector2>();
            m_TouchEndedPosition = new Dictionary<int, Vector2>();
            m_TouchToTrackMap = new Dictionary<int, int>();
        }

        public virtual void Update()
        {
            TickKeyInput();

            TickTouch();

            TickMouseClick();
        }

        public virtual void TickKeyInput()
        {
            for (int i = 0; i < m_TrackInput.Length; i++) {
                var input = m_TrackInput[i];
                var trackInputEventData = m_TrackInputEventData[i];

                if (input.GetInputDown()) { TriggerInput(trackInputEventData, 0); }

                if (input.GetInputUp()) { TriggerInput(trackInputEventData, 1); }

                // Trigger swipe input if the swipe input is pressed while the track button is hold.
                if (input.GetInput()) {
                    if (m_SwipeDown.GetInputDown()) {
                        TriggerInput(trackInputEventData, 2, Vector2.down);
                    } 
                    if (m_SwipeUp.GetInputDown()) {
                        TriggerInput(trackInputEventData, 2, Vector2.up);
                    } 
                    if (m_SwipeLeft.GetInputDown()) {
                        TriggerInput(trackInputEventData, 2, Vector2.left);
                    } 
                    if (m_SwipeRight.GetInputDown()) {
                        TriggerInput(trackInputEventData, 2, Vector2.right);
                    }
                }
            }
        }

        public virtual void TickTouch()
        {
            for (int i = 0; i < Input.touches.Length; i++) {
                var touch = Input.touches[i];
                var inputPosition = touch.position;

                //previous touche can be released on another track
                if (m_TouchToTrackMap.TryGetValue(touch.fingerId, out var trackID) && trackID != -1) {

                    var previousTrackInputEventData = m_TrackInputEventData[trackID];
                        
                    //손가락이 여전히 움직이는 동안 스와이프 감지
                    if (touch.phase == TouchPhase.Moved) {
                        InputMoved(touch.fingerId, previousTrackInputEventData, touch.position);
                    }

                    if (touch.phase == TouchPhase.Ended) {
                        InputReleased(touch.fingerId, previousTrackInputEventData, touch.position);
                    }
                } else {
                    trackID = -1;
                }

                var trackInputEventData = GetTrackInputEventData(inputPosition);

                if (trackInputEventData == null) { continue; }
                
                //이 트랙에 대해 입력 이벤트가 이미 확인 되었다
                if(trackID == trackInputEventData.TrackID){ continue; }

                if (touch.phase == TouchPhase.Began) {
                    InputPressed(touch.fingerId, trackInputEventData, touch.position);
                }

                //손가락이 움직이는 동안 여전히 스와이프 감지
                if (touch.phase == TouchPhase.Moved) {
                    InputMoved(touch.fingerId, trackInputEventData, touch.position);
                }

                if (touch.phase == TouchPhase.Ended) {
                    InputReleased(touch.fingerId, trackInputEventData, touch.position);
                }
            }
        }

        void CheckSwipe(InputEventData trackInputEventData, int fingerId)
        {
            if (m_TouchEndedPosition.ContainsKey(fingerId) == false ||
                m_TouchBeganPosition.ContainsKey(fingerId) == false) {
                return;
            }
            var direction = m_TouchEndedPosition[fingerId] - m_TouchBeganPosition[fingerId];
            var sqrDistance = direction.sqrMagnitude;
            
            if (sqrDistance > m_SwipeThreshold) { TriggerInput(trackInputEventData, 2, direction); }
        }

        private void TickMouseClick()
        {

            if (Input.GetMouseButtonDown(0)) {

                var inputPosition = Input.mousePosition;

                var trackInputEventData = GetTrackInputEventData(inputPosition);

                if (trackInputEventData == null) { return; }

                InputPressed(c_MouseTouchFingerID, trackInputEventData, inputPosition);
            }
            
            if (!m_DetectSwipeOnlyAfterRelease && Input.GetMouseButton(0)) {

                var inputPosition = Input.mousePosition;

                //이전 트랙 확인
                if (m_TouchToTrackMap.TryGetValue(c_MouseTouchFingerID, out var trackID) && trackID != -1) {
                    var previousTrackInputEventData = m_TrackInputEventData[trackID];
                    InputMoved(c_MouseTouchFingerID, previousTrackInputEventData, inputPosition);
                    
                } else {
                    trackID = -1;
                }
                var trackInputEventData = GetTrackInputEventData(inputPosition);

                if (trackInputEventData == null || trackInputEventData.TrackID == trackID) { return; }
                
                InputMoved(c_MouseTouchFingerID, trackInputEventData, inputPosition);
            }

            if (Input.GetMouseButtonUp(0)) {

                var inputPosition = Input.mousePosition;

                //이전 트랙 확인
                if (m_TouchToTrackMap.TryGetValue(c_MouseTouchFingerID, out var trackID) && trackID != -1) {
                    var previousTrackInputEventData = m_TrackInputEventData[trackID];
                    InputReleased(c_MouseTouchFingerID, previousTrackInputEventData, inputPosition);
                } else {
                    trackID = -1;
                }
                var trackInputEventData = GetTrackInputEventData(inputPosition);

                if (trackInputEventData == null || trackInputEventData.TrackID == trackID) { return; }

                InputReleased(c_MouseTouchFingerID, trackInputEventData, inputPosition);
            }
        }

        protected virtual void InputPressed(int fingerID, InputEventData trackInputEventData, Vector3 inputPosition)
        {
            TriggerInput(trackInputEventData, 0);
            m_TouchBeganPosition[fingerID] = inputPosition;
            m_TouchEndedPosition[fingerID] = inputPosition;
            m_TouchToTrackMap[fingerID] = trackInputEventData.TrackID;
        }

        protected virtual void InputMoved(int fingerID, InputEventData previousTrackInputEventData, Vector3 inputPosition)
        {
            if (m_DetectSwipeOnlyAfterRelease) { return; }

            m_TouchEndedPosition[fingerID] = inputPosition;
            CheckSwipe(previousTrackInputEventData, fingerID);
        }
        
        protected virtual void InputReleased(int fingerID, InputEventData previousTrackInputEventData, Vector3 inputPosition)
        {
            TriggerInput(previousTrackInputEventData, 1);
            m_TouchEndedPosition[fingerID] = inputPosition;
            m_TouchToTrackMap[fingerID] = -1;
            CheckSwipe(previousTrackInputEventData, fingerID);
        }

        protected virtual InputEventData GetTrackInputEventData(Vector2 inputPosition)
        {
            var tackObjects = m_RhythmProcessor.RhythmDirector.TrackObjects;
            
            var ray = m_Camera.ScreenPointToRay(inputPosition);
            
            if (m_2DTouchCollider) {
                
                var hit = Physics2D.Raycast(ray.origin, ray.direction, 100, m_TouchInputMask);
                
                if (hit.collider == null) { return null; }

                for (int i = 0; i < tackObjects.Length; i++) {
                    if (hit.collider != tackObjects[i].TouchCollider2D) { continue; }

                    return m_TrackInputEventData[i];

                }

                return null;
            }
            
            if (Physics.Raycast(ray, out var hitInfo, 100, m_TouchInputMask) == false) { return null; }

            for (int i = 0; i < tackObjects.Length; i++) {
                if (hitInfo.collider != tackObjects[i].TouchCollider3D) { continue; }

                return m_TrackInputEventData[i];
            }

            return null;
        }

        protected virtual void TriggerInput(InputEventData trackInputEventData, int inputID, Vector2 direction)
        {
            trackInputEventData.InputID = inputID;
            trackInputEventData.Direction = direction;
            TriggerInput(trackInputEventData);
        }

        protected virtual void TriggerInput(InputEventData trackInputEventData, int inputID)
        {
            trackInputEventData.InputID = inputID;
            trackInputEventData.Direction = Vector2.zero;
            TriggerInput(trackInputEventData);
        }

        protected virtual void TriggerInput(InputEventData trackInputEventData)
        {
            m_RhythmProcessor.TriggerInput(trackInputEventData);
        }
    }
}