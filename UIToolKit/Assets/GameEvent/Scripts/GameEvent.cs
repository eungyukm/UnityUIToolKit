using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BKK.GameEvent
{
    [CreateAssetMenu(menuName = "콘텐츠개발팀/게임 이벤트/게임 이벤트 에셋", fileName = "New Game Event",order = 0)]
    public class GameEvent : ScriptableObject
    {
        private readonly HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

#if UNITY_EDITOR
        [HideInInspector]
        public string description;
#endif

        /// <summary>
        /// 게임 이벤트에 등록된 모든 게임 이벤트 리스너의 유니티 이벤트들을 호출합니다.
        /// </summary>
        public void Invoke()
        {
            foreach (var globalEventListener in listeners)
            {
                globalEventListener.RaiseEvent();
#if UNITY_EDITOR
                Debug.Log($"{this.name} 이벤트가 실행되었습니다.\n경로: {globalEventListener.GetPath()}");
#endif
            }
        }

        public void Cancel()
        {
            foreach (var globalEventListener in listeners)
            {
                globalEventListener.StopEvent();
#if UNITY_EDITOR
                Debug.Log($"{this.name} 이벤트가 취소되었습니다.\n경로: {globalEventListener.GetPath()}");
#endif
            }
        }

        /// <summary>
        /// 게임 이벤트 리스너를 등록합니다.
        /// </summary>
        /// <param name="listener">등록할 게임 이벤트 리스너</param>
        public void Register(GameEventListener listener) => listeners.Add(listener);

        /// <summary>
        /// 게임 이벤트 리스너를 해지합니다.
        /// </summary>
        /// <param name="listener">해지할 게임 이벤트 리스너</param>
        public void Deregister(GameEventListener listener) => listeners.Remove(listener);

        /// <summary>
        /// 게임 이벤트에 등록된 게임 이벤트 리스너가 있는지 체크합니다.
        /// </summary>
        /// <returns></returns>
        public bool HasListeners()
        {
            return listeners.Count > 0;
        }
    }
}
