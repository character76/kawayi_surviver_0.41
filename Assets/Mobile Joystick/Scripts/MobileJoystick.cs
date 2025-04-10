//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MobileJoystick : MonoBehaviour
//{
//    [Header(" Elements ")]
//    [SerializeField] private RectTransform joystickOutline;
//    [SerializeField] private RectTransform joystickKnob;

//    [Header(" Settings ")]
//    [SerializeField] private float moveFactor;
//    private Vector3 clickedPosition;
//    private Vector3 move;
//    private bool canControl;


//    void Start()
//    {
//        HideJoystick();
//    }

//    private void OnDisable()
//    {
//        HideJoystick();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(canControl)
//            ControlJoystick();
//    }

//    public void ClickedOnJoystickZoneCallback()
//    {
//        clickedPosition = Input.mousePosition;
//        joystickOutline.position = clickedPosition;

//        ShowJoystick();
//    }

//    private void ShowJoystick()
//    {
//        joystickOutline.gameObject.SetActive(true);
//        canControl = true;
//    }

//    private void HideJoystick()
//    {
//        joystickOutline.gameObject.SetActive(false);
//        canControl = false;

//        move = Vector3.zero;
//    }

//    private void ControlJoystick()
//    {
//        //Vector3 currentPosition = Input.GetTouch(0).position;
//        Vector3 direction = currentPosition - clickedPosition;

//        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;

//        float moveMagnitude = direction.magnitude * moveFactor * canvasScale;

//        float absoluteWidth = joystickOutline.rect.width / 2;
//        float realWidth = absoluteWidth * canvasScale;

//        moveMagnitude = Mathf.Min(moveMagnitude, realWidth);

//        move = direction.normalized * moveMagnitude;

//        Vector3 targetPosition = clickedPosition + move;

//        joystickKnob.position = targetPosition;

//        if (Input.GetMouseButtonUp(0))
//            HideJoystick();
//    }

//    public Vector3 GetMoveVector()
//    {
//        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
//        return move / canvasScale;
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;

    [Header(" Settings ")]
    [SerializeField] private float moveFactor = 1f;

    private Vector2 clickedPosition;
    private Vector2 move;
    private bool canControl = false;
    private int joystickFingerId = -1; // 当前控制摇杆的手指 ID

    void Start()
    {
        HideJoystick();
    }

    private void OnDisable()
    {
        HideJoystick();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (!canControl)
                        {
                            clickedPosition = touch.position;
                            joystickOutline.position = clickedPosition;
                            joystickFingerId = touch.fingerId;
                            ShowJoystick();
                        }
                        break;

                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        if (canControl && touch.fingerId == joystickFingerId)
                        {
                            ControlJoystick(touch.position);
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if (canControl && touch.fingerId == joystickFingerId)
                        {
                            HideJoystick();
                        }
                        break;
                }
            }
        }
    }

    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControl = true;
    }

    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        joystickFingerId = -1;
        canControl = false;
        move = Vector2.zero;
    }

    private void ControlJoystick(Vector2 currentPosition)
    {
        Vector2 direction = currentPosition - clickedPosition;

        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        float moveMagnitude = direction.magnitude * moveFactor * canvasScale;

        float maxRange = (joystickOutline.rect.width / 2) * canvasScale;
        moveMagnitude = Mathf.Min(moveMagnitude, maxRange);

        move = direction.normalized * moveMagnitude;

        Vector2 targetPosition = clickedPosition + move;
        joystickKnob.position = targetPosition;
    }

    public Vector2 GetMoveVector()
    {
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        return move / canvasScale;
    }
}
