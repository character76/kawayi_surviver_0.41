using UnityEngine;


[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    private PlayerHealth playerhealth;
    [SerializeField] private CircleCollider2D collidersd;
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
    public Vector2 GetCenter()
    {
        return (Vector2)transform.position+ collidersd.offset;
    }

}
