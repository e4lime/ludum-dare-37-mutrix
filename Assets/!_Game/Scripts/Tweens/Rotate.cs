using UnityEngine;
using System.Collections;

using DG.Tweening;
namespace Mutrix.Tweens {
    public class Rotate : MonoBehaviour {
        [SerializeField]
        private float m_TimeRotate = 5;
        [SerializeField]
        private Vector3 m_Rotate = new Vector3(0, 360, 0);

        [SerializeField]
        private RotateMode m_RotateMode = RotateMode.WorldAxisAdd;

        [SerializeField]
        private LoopType m_LoopType = LoopType.Incremental;

        [SerializeField]
        private Ease m_Ease = Ease.Linear;

        // Use this for initialization
        void Start() {

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.DORotate(m_Rotate, m_TimeRotate, m_RotateMode).SetLoops(-1, m_LoopType).SetEase(m_Ease);
        }
    }
}