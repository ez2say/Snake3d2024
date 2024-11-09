using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    public bool IsActive { get; private set; }

    [SerializeField] private Sprite _turnOn;

    [SerializeField] private Sprite _turnOff;
    
    private Image _render;
    
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _render = GetComponent<Image>();
    }

    private void OnEnable() 
        => _button.onClick.AddListener(ChangeState);

    private void OnDisable() 
        => _button.onClick.RemoveAllListeners();

    public void ChangeState() 
        => UpdateStatus(!IsActive);

    public void UpdateStatus(bool value)
    {
        Debug.Log("Update status");

        IsActive = value;
        
        if (value)
        {
            _render.sprite = _turnOn;
            Debug.Log("Is Active: " + IsActive);
        }
        else
        {
            Debug.Log("Not is Active: " + IsActive);
            _render.sprite = _turnOff;
        }
    }
}