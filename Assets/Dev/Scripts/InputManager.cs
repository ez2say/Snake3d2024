using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _swipeStartPosition;

    private Vector3 _swipeEndPosition;

    private bool _isSwiping = false;

    public bool IsActiveControl { get; set; }

    public Vector3 GetInputDirection()
    {
        Vector3 direction = HandleKeyboardInput();

        if (direction == Vector3.zero)
        {
            direction = HandleTouchInput();
        }

        return direction;
    }

    private Vector3 HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            return Vector3.forward;

        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            return Vector3.left;

        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            return Vector3.back;

        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            return Vector3.right;

        return Vector3.zero;
    }

    private Vector3 HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartSwipe(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended && _isSwiping)
            {
                return EndSwipe(touch.position);
            }
        }

        return Vector3.zero;
    }

    private void StartSwipe(Vector2 position)
    {
        _swipeStartPosition = position;

        _isSwiping = true;
    }

    private Vector3 EndSwipe(Vector2 position)
    {
        _swipeEndPosition = position;
        
        _isSwiping = false;

        Vector3 swipeDirection = (_swipeEndPosition - _swipeStartPosition).normalized;

        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            return swipeDirection.x > 0 ? Vector3.right : Vector3.left;
        }
        else
        {
            return swipeDirection.y > 0 ? Vector3.forward : Vector3.back;
        }
    }
}    