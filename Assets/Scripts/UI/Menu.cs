namespace Digestin.UI {
    using UnityEngine;

    public class Menu : MonoBehaviour {
        Transform target;
        float distance;

        void Update() {
            if (target == null) return;

            transform.LookAt(target);
            transform.forward *= -1;
        }

        public void SetTarget(Transform target, float distance) {
            this.target = target;
            this.distance = distance;
        }       
    }
}