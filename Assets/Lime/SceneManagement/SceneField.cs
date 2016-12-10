using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


/// <summary>
/// Gives a drag n drop field for Scenes called SceneField.
/// 
/// Usage: 
/// "public SceneField myScene;"
/// 
/// Don't place this script in a Editor folder.
/// 
/// Taken from http://answers.unity3d.com/questions/242794/inspector-field-for-scene-asset.html#answer-1204071 
/// Posted at Jun 17 at 10:10 AM by "glitchers" 
/// and combined with Halfbiscuit comment for multiscene improvement
/// 
/// Slightly edited by limE
/// </summary>
[System.Serializable]
public class SceneField {
    [SerializeField]
    private Object m_SceneAsset;
    [SerializeField]
    private string m_SceneName = "";
    public string SceneName
    {
        get { return m_SceneName; }
    }
    // makes it work with the existing Unity methods (LoadLevel/LoadScene)
    public static implicit operator string(SceneField sceneField) {
        return sceneField.SceneName;
    }
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer {
    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
        EditorGUI.BeginProperty(_position, GUIContent.none, _property);
        SerializedProperty sceneAsset = _property.FindPropertyRelative("m_SceneAsset");
        SerializedProperty sceneName = _property.FindPropertyRelative("m_SceneName");
        _position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
        if (sceneAsset != null) {
            EditorGUI.BeginChangeCheck();

            Object value = EditorGUI.ObjectField(_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
            if (EditorGUI.EndChangeCheck()) {
                sceneAsset.objectReferenceValue = value;
                if (sceneAsset.objectReferenceValue != null) {
                    sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
                }
            }

        }
        EditorGUI.EndProperty();
    }
}
#endif