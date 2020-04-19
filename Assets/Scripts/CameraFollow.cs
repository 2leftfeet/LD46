using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
 [SerializeField] Transform player;
 float trackingSpeed = 2.0f;
 float zoomSpeed = 5.0f;

 Camera camera;
 
 void Start () {

    camera = Camera.main;
 
 if (player == null)
     player = GameObject.Find("Player").transform;
 }
 
 void Update () 
 {
    Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 600f);
    transform.position = Vector3.Lerp(transform.position, newPos, trackingSpeed * Time.deltaTime);
 }
}
