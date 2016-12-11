using UnityEngine;
using System.Collections;

namespace Mutrix {
    public class CameraInCamera : MonoBehaviour {

        [SerializeField]
        private Camera m_CenterCamera;


        [SerializeField, Header("x width, y height. From 0 to 1")]
        private Vector2 m_CenterCameraSize;

        [SerializeField]
        private bool m_CreateFrustum;


        public void Awake() {
            CropCameras();
           
            if (m_CreateFrustum) {
                Debug.Log("awake");
                MeshFilter filter = m_CenterCamera.GetComponent<MeshFilter>();
                if (filter == null) {
                    filter = m_CenterCamera.gameObject.AddComponent<MeshFilter>();


                    MeshRenderer meshRenderer = m_CenterCamera.GetComponent<MeshRenderer>();
                    if (meshRenderer == null) {
                        m_CenterCamera.gameObject.AddComponent<MeshRenderer>();
                    }

                   
                }
                filter.mesh = m_CenterCamera.GenerateFrustumMesh(true);
            }
        }


        private void CropCameras() {
            float w = m_CenterCameraSize.x;
            float h = m_CenterCameraSize.y;
            Rect rect = new Rect();
            rect.x = (1 - w) / 2;
            rect.y = (1 - h) / 2;
            rect.width = w;
            rect.height = h;

            CameraUtility.SetScissorRect(m_CenterCamera, rect);
        }

       
    }
}