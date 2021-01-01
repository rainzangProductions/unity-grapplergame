using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class GrapplingGun : MonoBehaviour {

	private LineRenderer lr;
	private Vector3 grapplePoint;
	public LayerMask grappleable;
	public Transform gunTip, mainCam, player;
	public int maxDistance = 75;
	public float jointSpring = 4.5f;
	public float jointDamper = 7f;
	private SpringJoint joint;
	public AudioClip hook;
	
	void Awake() {
		lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			StartGrapple();
		}else if (Input.GetButtonUp("Fire1")) {
			StopGrapple();
		}
	}

	void LateUpdate() {
		DrawRope();
	}

	void StartGrapple() {
		RaycastHit hit;
		if(Physics.Raycast(mainCam.position, mainCam.forward, out hit, maxDistance, grappleable)) {
			//controller.enabled = false;
			grapplePoint = hit.point;
			joint = player.gameObject.AddComponent<SpringJoint>();
			joint.autoConfigureConnectedAnchor = false;
			joint.connectedAnchor = grapplePoint;

			float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

			//The distance grapple will try to keep from the grappling point
			joint.maxDistance = distanceFromPoint * 0.08f;
			joint.minDistance = distanceFromPoint * 0.25f;

			//Adjust this to fit ur game broski
			joint.spring = jointSpring;
			joint.damper = jointDamper;
			//joint.massScale = 4.5;
			lr.positionCount = 2;
			AudioSource.PlayClipAtPoint(hook, transform.position);
		}

	}

	void StopGrapple() {
		lr.positionCount = 0;
		Destroy(joint);
	}

	void DrawRope() {
		if(!joint) return;
		lr.SetPosition(0, gunTip.position);
		lr.SetPosition(1, grapplePoint);
	}

	public bool IsGrappling() {
		return joint != null;
	}
	
	public Vector3 GetGrapplePoint() {
		return grapplePoint;
	}
}