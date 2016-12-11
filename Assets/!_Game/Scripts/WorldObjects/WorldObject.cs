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
            ChangeType(m_Type, false);
        }

        void Start() {
            if (IsMutrix() || IsOther()) {
                Management.MutrixGame.instance.RegisterDifferenceObject(this);
            }
        }

        public Type GetType() {
            return this.m_Type;
        }
     
        public bool IsMutrix() {
            return m_Type == Type.Mutrix;
        }
        public bool IsOther() {
            return m_Type == Type.Other;
        }
        public bool IsBoth() {
            return m_Type == Type.Both;
        }
        public bool IsThirdDimension() {
            return m_Type == Type.ThirdDimension;
        }
        public void ChangeType(Type type) {
            ChangeType(type, true);
        }

        private void ChangeType(Type type, bool updateGameManager) {
            switch (type) {
                case Type.Mutrix:
                    this.gameObject.layer = LayerMask.NameToLayer(Constants.MUTRIX_MASK);
                    break;
                case Type.Other:
                    this.gameObject.layer = LayerMask.NameToLayer(Constants.OTHER_WORLD_MASK);
                    break;
                case Type.Both:
                    if (updateGameManager) {
                        if (!IsBoth()) {
                            Management.MutrixGame.instance.DifferenceFound();
                        }
                    }
                    this.gameObject.layer = LayerMask.NameToLayer(Constants.BOTH_MASK);
                    break;
                case Type.ThirdDimension:
                    this.gameObject.layer = LayerMask.NameToLayer(Constants.THIRD_DIMENSION_MASK);
                    break;
                default:
                    Debug.LogError("Type is NOT SET!", this);
                    break;
            }
            m_Type = type;
        }
    }
}