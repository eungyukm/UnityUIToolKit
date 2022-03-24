using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BKK.GameEvent
{
    /// <summary>
    /// Input값을 스크립터블 오브젝트로 관리하려는 목적으로 작성중입니다. - 20220118 변고경
    /// </summary>
    //[CreateAssetMenu(menuName = "Input Event", fileName = "New Input Event")]
    [Serializable]
    public class InputEvent : ScriptableObject
    {
        [Header("Character Input Values")] public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        private void OnEnable()
        {
            Reset();
        }

        private void Reset()
        {
            move = Vector2.zero;
            look = Vector2.zero;
            jump = false;
            sprint = false;
        }

        [Header("Movement Settings")] public bool analogMovement;

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }
    }
}
