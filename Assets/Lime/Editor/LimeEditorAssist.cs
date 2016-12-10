using UnityEngine;
using UnityEditor;

namespace Lime.EditorUtility {

    /// The author disclaims copyright to this source code. In place of a 
    /// legal notice, here's a blessing:
    /// 
    ///  May you do good and not evil.
    ///  May you find forgiveness for yourself and forgive others.
    ///  May you share freely, never taking more than you give.
    ///  
    /// Emil Lindberg (emil at emillindberg.com)
    /// 
    /// <summary>
    /// 
    /// </summary>
    public static class LimeEditorAssist {

  

        /// <summary>
        /// Creates a go attached to a parent (can be null) and registers to undo if requested
        /// </summary>
        /// <param name="name">Name of go</param>
        /// <param name="parent">Parent of go, can be null</param>
        /// <param name="useUndo">Register created GO to Undo</param>
        /// <returns>Transform of the created go</returns>
        public static Transform CreateGO(string name, Transform parent, bool useUndo) {
            GameObject go = new GameObject(name);
            go.transform.parent = parent;
            go.transform.localPosition = Vector3.zero;
            if (useUndo) {
                Undo.RegisterCreatedObjectUndo(go, "Created " + name + " under " + parent);
                //EditorUtility.SetDirty(parent.gameObject);
            }

            return go.transform;
        }


        /// Moves data from size and center on a boxcollider to a transform. 
        /// 
        public static void MoveBoxColliderDataToTransform(BoxCollider coll, Transform trans, bool useUndo) {
            if (coll == null || trans == null) {
                throw new System.ArgumentNullException("coll or trans is null");
            }
            if (useUndo) { 
                Undo.RecordObject(coll, "Swap transform and boxcollider values");
                Undo.RecordObject(trans, "Swap transform and boxcollider values");
            }
            trans.transform.localPosition = coll.center;
            trans.transform.localScale = coll.size;
            coll.center = Vector3.zero;
            coll.size = Vector3.one;

        }

   
    }
}