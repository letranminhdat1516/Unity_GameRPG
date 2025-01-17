using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CameraController : MonoBehaviour
{
    
    public Transform target;
    public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 TopRightLimit;
    private float haflWidth;
    private float haflHeigh;
    // Start is called before the first frame update
    void Start()
    {
        //target = PlayerController.instance.transform;// Camera player
        target = FindAnyObjectByType<PlayerController>().transform;
        // Camera map
        haflHeigh = Camera.main.orthographicSize;
        haflWidth = haflHeigh * Camera.main.aspect;
       
        bottomLeftLimit = theMap.localBounds.min + new Vector3(haflWidth,haflHeigh,0f);
        TopRightLimit = theMap.localBounds.max+ new Vector3(-haflWidth,-haflHeigh,0f);
        // Set player dont out the map tile
        PlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y,transform.position.z);
        // keep the camera inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,bottomLeftLimit.x,TopRightLimit.x),
        Mathf.Clamp(transform.position.y,bottomLeftLimit.y,TopRightLimit.y),
        transform.position.z);
    }
}
