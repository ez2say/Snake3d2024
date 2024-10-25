using UnityEngine;

public class Food : MonoBehaviour
{
    public System.Action OnFoodEaten;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ЕдаСкушана");

            OnFoodEaten?.Invoke();
            
            Destroy(gameObject);
        }
    }
}