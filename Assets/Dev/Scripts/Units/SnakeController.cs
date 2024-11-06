using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SnakeController : MonoBehaviour
{
    public static SnakeController Instance { get; private set; }

    [SerializeField] private List<Transform> _tails;

    [SerializeField] private AudioSource _chompSound;

    [SerializeField] private AudioSource _obstacleDeathSound;

    [SerializeField] private AudioSource _toTheDepthSound;

    [SerializeField] private float _bonesDistance;

    [SerializeField] private GameObject _bonePrefab;

    [Range(0, 4), SerializeField] private float _moveSpeed;

    [Range(0, 4), SerializeField] private float _rotateSpeed;

    [Header("UI Elements")]

    [SerializeField] private GameObject _recordPopupPanel;

    [SerializeField] private TextMeshProUGUI _recordPopupText;

    private Vector3 _direction = Vector3.forward;

    private Vector3 _previousPosition;

    private Quaternion _previousRotation;

    private InputManager _inputManager;

    private List<Vector3> _PositionHistory = new List<Vector3>();

    private List<Quaternion> _RotationHistory = new List<Quaternion>();

    private bool _isDead;

    private void Start()
    {
        Instance = this;

        _inputManager = new GameObject("InputManager").AddComponent<InputManager>();

        if (_recordPopupPanel != null)
        {
            _recordPopupPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("RecordPopupPanel не найден в сцене!");
        }

        SavePreviousState();

        _isDead = false;
    }

    private void Update()
    {
        if (_isDead) return;

        HandleInput();

        MoveHead(_moveSpeed);

        MoveTail();

        CheckFrontCollision();
    }

    private void HandleInput()
    {
        Vector3 newDirection = _inputManager.GetInputDirection();
        
        if (newDirection != Vector3.zero && newDirection != -_direction)
            _direction = newDirection;
    }

    private void MoveHead(float speed)
    {
        SavePreviousState();

        UpdatePosition(speed);

        UpdateRotation();
    }

    private void SavePreviousState()
    {
        _PositionHistory.Insert(0, transform.position);
        _RotationHistory.Insert(0, transform.rotation);
    }

    private void UpdatePosition(float speed)
    {
        transform.position += _direction * speed * Time.deltaTime;
    }

    private void UpdateRotation()
    {
        transform.rotation = Quaternion.LookRotation(_direction);
    }

    private void MoveTail()
    {
        int index = 0;
        foreach (var bone in _tails)
        {
            Vector3 point = _PositionHistory[Mathf.Min(index * (int)_bonesDistance, _PositionHistory.Count-1)];

            Quaternion rotation = _RotationHistory[Mathf.Min(index * (int)_bonesDistance, _RotationHistory.Count - 1)];

            bone.transform.position = point;

            bone.transform.rotation = rotation;

            index++;
        }
    }

    private void CheckFrontCollision()
    {
        RaycastHit hit;
        
        Vector3 rayStartPosition = transform.position + _direction * (_bonesDistance / 2f);

        if (Physics.Raycast(rayStartPosition, _direction, out hit, _bonesDistance) && hit.collider.CompareTag("Segment"))
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDead) return;

        if (other.CompareTag("Food"))
        {
            _chompSound.Play();

            Destroy(other.gameObject);

            AddNewSegment();

            UpdateHUD();
        }
        else if (other.CompareTag("Obstacle") || other.CompareTag("Water"))
        {
            if (other.CompareTag("Obstacle"))
            {
                _obstacleDeathSound.Play();
            }
            else if (other.CompareTag("Water"))
            {
                _toTheDepthSound.Play();
            }

            StopMovement();

            Invoke("Die", 1f);
        }
    }

    private void StopMovement()
    {
        _isDead = true;

        _direction = Vector3.zero;
    }

    private void AddNewSegment()
    {
        Vector3 newSegmentPosition = transform.position - _direction * 25f;
        GameObject bone = Instantiate(_bonePrefab, newSegmentPosition, Quaternion.identity);

        _tails.Add(bone.transform);
    }

    private void UpdateHUD()
    {
        if (HUDManager.Instance != null)
        {
            HUDManager.Instance.IncreaseFoodEatenCount();

            int currentScore = HUDManager.Instance.GetFoodEatenCount();

            string recordMessage;

            if (LeaderboardManager.Instance.CheckRecord(currentScore, out recordMessage))
            {
                ShowRecordPopup(recordMessage);
            }
        }
        else
        {
            Debug.LogError("HUDManager не найден в сцене!");
        }
    }

    private void Die()
    {
        _isDead = true;
        Debug.Log("Змейка умерла!");
        if (DeathScreenManager.Instance != null)
        {
            DeathScreenManager.Instance.ShowDeathScreen(HUDManager.Instance.GetFoodEatenCount());
        }
        else
        {
            Debug.LogError("DeathScreenManager не найден в сцене!");
        }
    }

    private void ShowRecordPopup(string message)
    {
        if (_recordPopupText != null)
        {
            _recordPopupText.text = message;
        }
        else
        {
            Debug.LogError("RecordPopupText не найден в сцене!");
        }

        if (_recordPopupPanel != null)
        {
            _recordPopupPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("RecordPopupPanel не найден в сцене!");
        }

        Invoke(nameof(HideRecordPopup), 3f);
    }

    private void HideRecordPopup()
    {
        if (_recordPopupPanel != null)
        {
            _recordPopupPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("RecordPopupPanel не найден в сцене!");
        }
    }

    public void ResetSnake()
    {
        transform.position = Vector3.zero;

        _direction = Vector3.forward;

        transform.rotation = Quaternion.LookRotation(-_direction);

        foreach (var bone in _tails)
        {
            Destroy(bone.gameObject);
        }

        _tails.Clear();

        SavePreviousState();

        _isDead = false;

        Time.timeScale = 1f;//new
    }
}