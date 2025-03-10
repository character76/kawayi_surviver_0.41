using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Dace_move : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private MobileJoystick playerJoystick;
    [SerializeField] private float joystickSensitivity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rig.linearVelocity = playerJoystick.GetMoveVector() * joystickSensitivity * Time.deltaTime;
    }
}
