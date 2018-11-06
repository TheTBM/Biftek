﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.right);
        gameObject.transform.Rotate(Vector3.forward * 90);
    }
}