using UnityEngine;

public class Enemy_follow : MonoBehaviour
{
    [Header("Elements")] 
    private player player_dave;

    [Header("setting")]
    [SerializeField]private float speed;
    [SerializeField] private float destroyRadius;
    [Header("DEBUG")]
    [SerializeField] private bool showGizmos;
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
        FollowPlayer();

        TryAttack();

    }

    private void FollowPlayer()
    {
        Vector2 dir = (player_dave.transform.position - transform.position).normalized;
        
        //Debug.Log(dir);

        Vector2 targetpos = (Vector2)transform.position + speed * dir * Time.deltaTime;

        transform.position = targetpos;
    }
    private void TryAttack()
    {
        float distance = (player_dave.transform.position - transform.position).magnitude;
        //Debug.Log(player_dave.transform.position - transform.position);
        if (distance<destroyRadius)
        {
            
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        if(showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, destroyRadius);
        }
       else
        {
            return;
        }
    }
}
