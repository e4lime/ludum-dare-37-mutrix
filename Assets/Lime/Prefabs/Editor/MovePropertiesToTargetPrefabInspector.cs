using UnityEngine;
using UnityEditor;
using Lime.Prefabs;
using System.IO;

namespace Lime.Prefabs {
    [CustomEditor(typeof(MovePropertiesToTargetPrefab), true)]
    public class MovePropertiesToTargetPrefabInspector : Editor {

        /// <summary>
        /// Location of converted prefabs
        /// </summary>
        public const string CONVERTED_FOLDER_NAME = "converted";

        public override void OnInspectorGUI() {
            //serializedObject.Update();
           // DrawScriptLink(serializedObject);
            DrawDefaultInspector();
            EditorGUILayout.Space();
            if (GUILayout.Button("Copy Data to new/update Prefab")) {
               MoveDataProcess();
            }
            //serializedObject.ApplyModifiedProperties();
        }

        public static void DrawScriptLink(SerializedObject serializedObject) {
            EditorGUI.BeginDisabledGroup(true);
            SerializedProperty prop = serializedObject.FindProperty("m_Script");
            EditorGUILayout.PropertyField(prop, true, new GUILayoutOption[0]);
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// Instansiates the prefab, converts it and saves it as a new prefab under PATH_TO_CONVERTED folder
        /// </summary>
        private void MoveDataProcess() {
            MovePropertiesToTargetPrefab selectedDataAsset = (MovePropertiesToTargetPrefab)target;
            string pathToDataAsset = AssetDatabase.GetAssetPath(selectedDataAsset);
            MovePropertiesToTargetPrefab instanceOfSelected = PrefabUtility.InstantiatePrefab(selectedDataAsset) as MovePropertiesToTargetPrefab;
            PrefabUtility.DisconnectPrefabInstance(instanceOfSelected);
            GameObject convertedInstance = instanceOfSelected.MovePropertiesToTarget();

            CreateConvertedFolderIfNeeded();
            string pathForNewPrefab = Path.GetDirectoryName(pathToDataAsset) + "/" + CONVERTED_FOLDER_NAME + "/" + convertedInstance.name + ".prefab";
            PrefabUtility.CreatePrefab(pathForNewPrefab, convertedInstance);
            DestroyImmediate(convertedInstance);
        }
        
        private void CreateConvertedFolderIfNeeded() {
            MovePropertiesToTargetPrefab selectedDataAsset = (MovePropertiesToTargetPrefab)target;
            string pathToDataAssetFolder = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedDataAsset));
            if (!AssetDatabase.IsValidFolder(pathToDataAssetFolder + "/" + CONVERTED_FOLDER_NAME)) {
                AssetDatabase.CreateFolder(pathToDataAssetFolder, CONVERTED_FOLDER_NAME);
            }
        }
   
    }
}
