using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private Camera cam;

    public Transform target = null;
    public float damp = 20; //how fast do we want the camera to catch up
    public float zdist = -10; //the constant that allows for layering via zdist


	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 3f;
	}
	
	// Update is called once per frame
	void Update () {
        followTarget();
	}

    void followTarget() {
        if (target != null) {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, zdist);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * damp);
        }
    }

}
