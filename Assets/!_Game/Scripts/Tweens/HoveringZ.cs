﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
namespace Mutrix.Tweens {
    public class HoveringZ : MonoBehaviour {
        [SerializeField]
        private float m_Length = 3;
        [SerializeField]
        private float m_Time = 3;

        [SerializeField]
        private Ease m_EaseType = Ease.Linear;

        // Use this for initialization
        void Start() {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.DOMoveZ(rb.position.z + m_Length, m_Time).SetLoops(-1, LoopType.Yoyo).SetEase(m_EaseType);
        }


    }
}
