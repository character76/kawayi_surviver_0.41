using UnityEngine;


[RequireComponent(typeof(PlayerHealth))]
public class player : MonoBehaviour
{
    [Header("Components")]
    private PlayerHealth playerhealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerhealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        playerhealth.TakeDamge(damage);
    }

}
