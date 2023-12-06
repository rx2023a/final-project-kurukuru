namespace Digestin.UI {
    using UnityEngine;

    public class MenuSpawner : MonoBehaviour
    {
        [SerializeField] string playerTag = "Player";
        [SerializeField] Transform spawnPoint;
        [SerializeField] Menu menuPrefab;
        [SerializeField] float distanceToPlayer = 10f;
        
        public void SpawnMenu() {
            Transform player = GameObject.FindGameObjectWithTag(playerTag).transform;
            if (player == null) return;

            Menu menu = Instantiate(menuPrefab, spawnPoint.position, Quaternion.identity);
            menu.SetTarget(player, distanceToPlayer);
        }
    }
}