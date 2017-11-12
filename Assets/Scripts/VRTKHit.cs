using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VRTK;

public class VRTKHit : MonoBehaviour {

  public float impactScalar = 120f;
  public float upForce = 100f;

  private VRTK_ControllerReference controller;

	// Use this for initialization
	void Start () {
    VRTK_ControllerReference.GetControllerReference(gameObject);
	}
	
	void OnCollisionEnter(Collision col) {
    Volleyball ball = col.gameObject.GetComponent<Volleyball>();
    if (ball != null) {
      float handForce = VRTK_DeviceFinder.GetControllerVelocity(controller).magnitude * impactScalar;
      ContactPoint point = col.contacts[0];
      col.rigidbody.AddForceAtPosition( (-point.normal.normalized * handForce) , point.point);
      col.rigidbody.AddForce(upForce * Vector3.up);
    }
  }
}
