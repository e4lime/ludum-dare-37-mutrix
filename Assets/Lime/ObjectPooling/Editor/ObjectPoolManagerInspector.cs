using UnityEngine;
using UnityEditor;

namespace Lime.ObjectPooling {
    [CustomEditor(typeof(ObjectPoolManager))]
    public class ObjectPoolManagerInspector : Editor {

        public override void OnInspectorGUI() {
            //serializedObject.Update();
            // DrawScriptLink(serializedObject);
            DrawDefaultInspector();
            EditorGUILayout.Space();
            if (GUILayout.Button("Build Pool")) {
                BuildPool();
            }
            //serializedObject.ApplyModifiedProperties();
        }


        private void BuildPool() {
            ObjectPoolManager poolManager = (ObjectPoolManager)target;

        }
    }
}
