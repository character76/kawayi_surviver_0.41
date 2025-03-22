using UnityEngine;

public class SpirtSorter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sprite.sortingOrder = -(int)(transform.position.y * 10);
    }
}
