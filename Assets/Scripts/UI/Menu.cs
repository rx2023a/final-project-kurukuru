namespace Digestin.UI {
    using UnityEngine;

    public class Menu : MonoBehaviour {
        [SerializeField] GameObject startingPanel;

        Transform target;
        float distance;
        GameObject currentPanel;

        void Start() {
            SetPanel(startingPanel);
        }

        void Update() {
            if (target == null) return;

            transform.LookAt(target);
            transform.forward *= -1;
        }

        public void SetTarget(Transform target, float distance) {
            this.target = target;
            this.distance = distance;
        }       

        public void SetPanel(GameObject newPanel) {
            foreach(Transform child in transform) {
                child.gameObject.SetActive(false);
            }
            newPanel.SetActive(true);
        }

        public void Close() {
            Destroy(gameObject);
        }
    }
}