using UnityEngine;

namespace Lime.Hierarchy {
    /// <summary>
    /// Attach to ROOT object. If child it wont work (the root get destroyed so the child get destroyed anyway)
    /// </summary>
    public class SetDontDestroyOnLoadInAwake : MonoBehaviour {

        void Awake(){
            DontDestroyOnLoad(gameObject);
        }
    }
}
