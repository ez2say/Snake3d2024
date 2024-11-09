using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [Header("UI Elements")]

    [SerializeField] private TextMeshProUGUI _foodCounter;

    [Header("Game Data")]

    private int _foodEatenCount;
    
    [SerializeField] private GameObject _view;

    public void Construct()
    {
        Instance = this;
        
        InitializeFoodEatenCount();
        
        UpdateFoodCounterText();
    }

    public void Open()
    {
        _view.SetActive(true);
    }

    public void Close()
    {
        _view.SetActive(false);
    }

    private void InitializeFoodEatenCount()
    {
        _foodEatenCount = 0;
    }

    public void IncreaseFoodEatenCount()
    {
        Debug.Log("+1 к еде скушанной");

        _foodEatenCount++;

        UpdateFoodCounterText();
    }

    private void UpdateFoodCounterText()
    {
        if (_foodCounter != null)
        {
            _foodCounter.text = "Food Eaten: " + _foodEatenCount;
        }
        else
        {
            Debug.LogError("FoodCounter не назначен в инспекторе!");
        }
    }

    public int GetFoodEatenCount()
    {
        return _foodEatenCount;
    }
}