namespace Digestin.Path {
    using System.Collections;
    using System.Collections.Generic;
    using Cinemachine;
    using UnityEngine;

    public class DollyCartTrigger : MonoBehaviour
    {
        [SerializeField] CinemachinePathBase startingPath;

        CinemachineDollyCart dollyCart;
        float pathLength = 0f;

        void Awake() {
            dollyCart = GetComponent<CinemachineDollyCart>();
            StartPath(startingPath);
        }

        void Update() {
            if (dollyCart.m_Position >= pathLength) {
                dollyCart.m_Path = null;
            }
        }

        public void StartPath(CinemachinePathBase newPath) {
            if (IsRunning()) return;

            dollyCart.m_Path = newPath;
            dollyCart.m_Position = 0;
            pathLength = newPath.PathLength;
        }

        bool IsRunning() {
            return dollyCart.m_Path != null;
        }
    }
}