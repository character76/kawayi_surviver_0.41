using UnityEngine;

public class Camera_controll : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector2 minMaxXY;

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
        tposition.x = Mathf.Clamp(tposition.x, -minMaxXY.x, minMaxXY.x);
        tposition.y = Mathf.Clamp(tposition.y, -minMaxXY.y, minMaxXY.y);

        transform.position = tposition; 
    }
}
