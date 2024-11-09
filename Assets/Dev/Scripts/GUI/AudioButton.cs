using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private Sprite _turnOn;

    [SerializeField] private Sprite _turnOff;

    private bool _isActive;
    
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
        => UpdateStatus(!_isActive);

    public void UpdateStatus(bool value)
    {
        Debug.Log("Update status");

        _isActive = value;
        
        if (value)
        {
            _render.sprite = _turnOn;
            Debug.Log("Is Active: " + _isActive);
        }
        else
        {
            Debug.Log("Not is Active: " + _isActive);
            _render.sprite = _turnOff;
        }
    }
}