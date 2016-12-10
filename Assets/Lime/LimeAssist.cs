using UnityEngine;

namespace Lime {
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
    public static class LimeAssist {

        public static GameObject GetChildGameObject(GameObject parent, string name) {
            if (parent.name == name) {
                return parent;
            }
            foreach (Transform transform in parent.GetComponentsInChildren<Transform>()) {
                if (transform.name == name) {
                    return transform.gameObject;
                }

            }

            return null;
        }

        /// <summary>
        /// Random constant rotation.
        /// GameObjects requires a ConstantForce component
        /// </summary>
        public static void SetRandomRotationConstant(Transform toRotate, float minRot, float maxRot, bool randomStartingRotation) {
            if (randomStartingRotation) {
                toRotate.rotation = Random.rotation;
            }

            Vector3 torqueToSet = new Vector3(Random.Range(minRot, maxRot),
                                                Random.Range(minRot, maxRot),
                                                Random.Range(minRot, maxRot));
            toRotate.GetComponent<ConstantForce>().torque = torqueToSet;

        }

        /// <summary>
        /// If we have known inactive objects somewhere beneath the root and do want to activate or deactivate all of them. 
        /// </summary>
        public static void SetActiveRecursively(GameObject rootObject, bool active) {
            rootObject.SetActive(active);

            foreach (Transform childTransform in rootObject.transform) {
                SetActiveRecursively(childTransform.gameObject, active);
            }
        }

        public static float Remap(float value, float inputStart, float inputEnd, float outputStart, float outputEnd) {

            return outputStart + ((outputEnd - outputStart) / (inputEnd - inputStart)) * (value - inputStart);
           
        }
    }
}