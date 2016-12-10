using UnityEngine;
using System.Collections.Generic;
using System;

namespace Lime.Prefabs {
    /// <summary>
    /// Template for setting up data/properties on a target prefab at Start. Useful when you have a family prefabs that shares components
    /// but has different data and properties for each prefab. The target should be a prefab that contains the shared components and the properties/data plus children
    /// will get copied to an instance of the targeted prefab. 
    /// 
    /// MovePropertiesToTarget will be called on build time on GO IN the scene, won't work if referenced from asset folder.
    /// 
    /// If referenced from asset folder, open context menu and click "Move Properties Now"
    /// 
    /// Wont work if prefab isnt in scene
    /// 
    /// Use m_TargetInstance to setup data
    /// </summary>

    public abstract class MovePropertiesToTargetPrefab : MonoBehaviour {

        [SerializeField, Tooltip("A prefab that the properties will be moved onto")]
        protected Transform m_TargetPrefab;

        /// <summary>
        /// Instance of m_TargetPrefab
        /// </summary>
        protected Transform m_TargetInstance;

        #region Cache
        protected Transform m_Transform;
        #endregion


        public Transform TargetPrefab
        {
            get
            {
                return m_TargetPrefab;
            }
            set
            {
                m_TargetPrefab = value;
            }
        }

        public virtual GameObject MovePropertiesToTarget() {

            if (!m_TargetPrefab) {
                Debug.LogError("No target prefab set.", this);
            }
            m_Transform = this.transform;
            m_TargetInstance = GameObject.Instantiate(m_TargetPrefab, this.m_Transform.position, this.m_Transform.rotation) as Transform;

            SetupData();

            m_TargetInstance.name += " with " + this.name;
            m_TargetInstance.parent = m_Transform.parent;
            MoveChildrenToTargetEntity();
            SetupChildrenDependentData();

#if UNITY_EDITOR
            // Seems to still be able to return
            DestroyImmediate(this.gameObject);
#else
            Destroy(this.gameObject);
#endif
            return m_TargetInstance.gameObject;
        }



        private void MoveChildrenToTargetEntity() {
            List<Transform> childrenToMove = new List<Transform>(m_Transform.childCount);
            foreach (Transform child in this.m_Transform) {
                childrenToMove.Add(child);
            }

            foreach (Transform child in childrenToMove) {
                child.parent = m_TargetInstance;
            }

        }

        protected abstract void SetupData();
        protected abstract void SetupChildrenDependentData();

    
    }
}