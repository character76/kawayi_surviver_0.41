using UnityEngine;

public class Enemy_follow : MonoBehaviour
{
    [Header("Elements")] 
    private player player_dave;

    [Header("setting")]
    [SerializeField]private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_dave = FindFirstObjectByType<player>(); 

        if(player_dave == null)
        {
            Debug.LogWarning("noplayer found destroy");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = (player_dave.transform.position - transform.position).normalized;
        Debug.Log(player_dave.transform.position); 

        Vector2 targetpos = (Vector2)transform.position + speed * dir * Time.deltaTime;

        transform.position = targetpos; 

    }
}
