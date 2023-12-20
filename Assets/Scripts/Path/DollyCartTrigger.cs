namespace Digestin.Path {
    using System.Collections;
    using System.Collections.Generic;
    using Cinemachine;
    using UnityEngine;

    public class DollyCartTrigger : MonoBehaviour
    {
        [SerializeField] CinemachinePathBase startingPath;

        CinemachineDollyCart dollyCart;
        FlyTrigger flyTrigger;
        float pathLength = 0f;
        float dollyCartSpeed = 0f;

        void Awake() {
            dollyCart = GetComponent<CinemachineDollyCart>();
            flyTrigger = GetComponent<FlyTrigger>();
            StartPath(startingPath);
        }

        void Start() {
            dollyCartSpeed = dollyCart.m_Speed;
        }

        void Update() {
            if (!IsRunning()) return;

            if (dollyCart.m_Position >= pathLength) {
                dollyCart.m_Path = null;
                flyTrigger.enabled = true;

            }
            if (flyTrigger.DetectTargets()) {
                dollyCart.m_Speed = dollyCartSpeed;
            }
            else {
                dollyCart.m_Speed = 0f;
            }
        }

        public void StartPath(CinemachinePathBase newPath) {
            if (IsRunning()) return;

            dollyCart.m_Path = newPath;
            dollyCart.m_Position = 0;
            pathLength = newPath.PathLength;
            flyTrigger.enabled = false;
        }

        bool IsRunning() {
            return dollyCart.m_Path != null;
        }
    }
}