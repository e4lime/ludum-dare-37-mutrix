using UnityEngine;
using System.Collections.Generic;

namespace Lime.ObjectPooling{
    public class ObjectPoolManager : MonoBehaviour {
        [SerializeField]
        private Dictionary<string, Transform> m_PoolParents;

        /// <summary>
        /// Fake view for Dictionary in inspector
        /// </summary>
        //[SerializeField]
        //private PoolEntity[] m_PoolForInspector;

        private int m_LastObjectToPoolCount;

	    #region cache
		private Transform m_Transform;
		#endregion

        void Awake(){
			m_Transform = this.transform;
        }

     
        void Start() {
            int objectsToPool = m_Transform.childCount;
            m_PoolParents = new Dictionary<string, Transform>(objectsToPool);
            m_LastObjectToPoolCount = objectsToPool;
        }


        void Update() {


        }

        void BuildPool() {

        }


        // TODO Disable all objects in pool on scene build
        //void OnSceneChanged() {
        //  ...
        //}


        /*
        [System.Serializable]
        public struct PoolEntity {
            public string prefabName;
            public Transform prefab;
            public int amount;
        }
        */
    }
}
