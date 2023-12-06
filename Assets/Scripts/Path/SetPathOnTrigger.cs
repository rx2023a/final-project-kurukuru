namespace Digestin.Path {
    using Cinemachine;
    using UnityEngine;

    public class SetPathOnTrigger : MonoBehaviour
    {
        [SerializeField] CinemachinePathBase pathOnTrigger;

        void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent(out DollyCartTrigger cartTrigger)) return;

            cartTrigger.StartPath(pathOnTrigger);
        }
    }
}