using UnityEngine;
[RequireComponent(typeof(Player))]
public class PlayerDetection : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField] private Collider2D daveCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Åö×²¼ì²â·¢ÉúÔÚ£º" + collision.gameObject.name );
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            if (!collision.IsTouching(daveCollider))
                return;
            //Debug.Log("Collected" + candy.name);
            collectable.Collect(GetComponent<Player>());
            //Destroy(candy.gameObject);
        }
        
    }
}
