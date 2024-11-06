using UnityEngine;

public class ObstacleCollisionHandler : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}