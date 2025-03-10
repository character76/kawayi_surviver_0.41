using UnityEngine;

public class Camera_controll : MonoBehaviour
{
    [SerializeField] private Transform Target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 tposition = Target.transform.position;
        tposition.z = -10;
        transform.position = tposition; 
    }
}
