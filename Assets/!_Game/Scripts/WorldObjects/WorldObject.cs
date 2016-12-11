using UnityEngine;
using System.Collections;
namespace Mutrix.WorldObjects {
    public class WorldObject : MonoBehaviour {

        [SerializeField]
        private Type m_Type;

        public enum Type {
            NOT_SET,
            Mutrix,
            Other,
            Both,
            ThirdDimension
        }

        // Use this for initialization
        void Awake() {
            ChangeType(m_Type);
        }

        // Update is called once per frame
        void Update() {

        }

        private void ChangeType(Type type) {
            switch (type) {
                case Type.Mutrix:
                    this.gameObject.layer = LayerMask.NameToLayer(Constants.MUTRIX_MASK);
                    break;
                case Type.Other:
                    this.gameObject.layer = LayerMask.NameToLayer(Constants.OTHER_WORLD_MASK);
                    break;
                case Type.Both:
                    this.gameObject.layer = LayerMask.NameToLayer(Constants.BOTH_MASK);
                    break;
                case Type.ThirdDimension:
                    this.gameObject.layer = LayerMask.NameToLayer(Constants.THIRD_DIMENSION_MASK);
                    break;
                default:
                    Debug.LogError("Type is NOT SET!", this);
                    break;
            }
        }
    }
}