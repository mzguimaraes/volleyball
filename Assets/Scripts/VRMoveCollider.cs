using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider), typeof(Rigidbody))]
public class VRMoveCollider : MonoBehaviour {
  //place on object childed to a VR-tracked object
  //object will follow tracked object
  //allowing for use in Unity-built in physics simulation

  private Transform trackedObject;
  private Rigidbody rb;

	// Use this for initialization
	void Start () {
    trackedObject = transform.parent;
    rb = GetComponent<Rigidbody>();
    rb.interpolation = RigidbodyInterpolation.Interpolate;
	}
	
	void FixedUpdate () {
    rb.MovePosition(transform.position);
    rb.MoveRotation(transform.rotation);
  }
}
