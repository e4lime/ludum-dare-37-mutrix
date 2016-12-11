using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Mutrix.Tweens {
    public class HoveringUpDownRotate : MonoBehaviour {
        [SerializeField]
        private float m_Height = 1;
        [SerializeField]
        private float m_TimeY = 5;

        [SerializeField]
        private float m_TimeRotate = 5;
        [SerializeField]
        private Vector3 m_Rotate = new Vector3(0, 45, 0);
        [SerializeField]
        private Ease m_Ease = Ease.InOutCubic;


        void Start() {
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.DOMoveY(rb.position.y + m_Height, m_TimeY).SetEase(m_Ease).SetLoops(-1, LoopType.Yoyo);
            rb.DORotate(m_Rotate, m_TimeRotate, RotateMode.WorldAxisAdd).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);
            /*
            Sequence s = DOTween.Sequence();

            s.Append(rb.DOMoveY(rb.position.y + m_Height, m_Time)).SetEase(m_Ease);
            s.SetLoops(-1, LoopType.Yoyo);
            s.Join(rb.DORotate(m_Rotate, m_Time, RotateMode.LocalAxisAdd));
           */

        }
    }
}
