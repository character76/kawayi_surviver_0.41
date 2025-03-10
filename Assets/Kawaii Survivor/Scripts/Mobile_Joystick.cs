using UnityEngine;

public class Mobile_Joystick : MonoBehaviour
{
    [SerializeField] private RectTransform joystickZone;
    [SerializeField] private RectTransform joystickKnob;

    private bool Can_control;
    private Vector3 clickpos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide_Joystick();
    }

    // Update is called once per frame
    void Update()
    {
        if(Can_control)
        {
            Controll_Joy();
        }
    }
    public void ClickonCallback()
    {
        clickpos = Input.mousePosition;
        joystickZone.position = clickpos;
        Show_Joystick();
        Debug.Log("click");
    }
    private void Hide_Joystick()
    {
        joystickZone.gameObject.SetActive(false);
        Can_control = false;
    }
    private void Show_Joystick()
    {
        joystickZone.gameObject.SetActive(true);
        Can_control = true;
    }
    private void Controll_Joy()
    {
        Vector3 holdpos = Input.mousePosition;
        Vector3 Yoke = holdpos - clickpos;
        if(Yoke.magnitude<joystickZone.rect.width/2)
        {
            joystickKnob.position = clickpos + Yoke;
        }
        else
        {
            joystickKnob.position = clickpos + Yoke*((joystickZone.rect.width / 2)/ Yoke.magnitude);
        }
        
    }
}
