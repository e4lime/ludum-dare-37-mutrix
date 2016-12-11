using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Mutrix.Tweens {
    public class HoveringX : MonoBehaviour {
        [SerializeField]
        private float m_Length = 5;
        [SerializeField]
        private float m_Time = 5;
        [SerializeField]
        private Ease m_EaseType = Ease.Linear;

        // Use this for initialization
        void Start() {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.DOMoveX(rb.position.x + m_Length, m_Time).SetLoops(-1, LoopType.Yoyo).SetEase(m_EaseType);
        }
    }
}
