using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public GrapplingGun grappler;
	private Quaternion desiredRot;
	public float rotationSpeed = 5f;
	
	void Update () {
		if(!grappler.IsGrappling()) {
			desiredRot = transform.parent.rotation;
		}else {
			desiredRot = Quaternion.LookRotation(grappler.GetGrapplePoint() - transform.position);
		}
		transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, Time.deltaTime * rotationSpeed);
	}
}