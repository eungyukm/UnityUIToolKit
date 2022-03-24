using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BKK.GameEvent
{
    [CreateAssetMenu(menuName = "콘텐츠개발팀/게임 이벤트/게임 이벤트 설명 옵션", fileName = "GameEventDescriptionOption", order = 1)]
    public class GameEventDescriptionOption : ScriptableObject
    {
        public int fontSize = 15;

        public bool locked = true;

        public int typingAreaHeight = 12;

        [MenuItem("콘텐츠개발팀/게임 이벤트/게임 이벤트 설명 옵션", priority = 1)]
        public static void ShowAsset()
        {
            var assetGUIDList = AssetDatabase.FindAssets("t:GameEventDescriptionOption", null);

            if (assetGUIDList.Length > 0)
            {
                Debug.Log("설명 옵션이 이미 존재합니다.");
                var path = AssetDatabase.GUIDToAssetPath(assetGUIDList[0]);
                var asset = AssetDatabase.LoadMainAssetAtPath(path);
                EditorGUIUtility.PingObject(asset);
                AssetDatabase.OpenAsset(asset);
            }
            else
            {
                CreateAsset();
            }
        }

        public static void CreateAsset()
        {
            var path = EditorUtility.SaveFilePanelInProject("설명 옵션 파일 저장", "GameEventDescriptionOption", "asset", "");
            if (path.Equals(string.Empty)) return;

            GameEventDescriptionOption asset = ScriptableObject.CreateInstance<GameEventDescriptionOption>();
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            EditorGUIUtility.PingObject(asset);
            AssetDatabase.OpenAsset(asset);
        }
    }
}
