using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform Target { get; set; }

    private void Update()
    {
        if (!Target) return;

        transform.position = Vector3.Lerp(transform.position, Target.position + (Vector3.forward * transform.position.z), 1);
    }
}
