using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    public GameObject followObject;
    public float scrollSpeed = 1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var objects = GameObject.FindObjectsOfType<NBodyObject>();
        var newFollow = objects[0];
        foreach ( var o in objects )
        {
            if (o.mass > newFollow.mass)
                newFollow = o;
        }
        followObject = newFollow.gameObject;
        if (followObject != null)
        {
            transform.position = followObject.transform.position - Vector3.back*-200;
        }

        //Zoom:
        //Camera.main.orthographicSize += Input.mouseScrollDelta.y * scrollSpeed;
    }
}
