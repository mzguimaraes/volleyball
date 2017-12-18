using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VRTK;

public class VRTKHit : MonoBehaviour {

  public static float RESTITUTION_COEF = .5f;
  public static int collisionCount = 0;

  public float impactScalar = 120f;
  public float upForce = 100f;

  private VRTK_ControllerReference controller;
  private Rigidbody rb;

  //velocity approximation
  private Vector3 lastPos;
  private Vector3 velocity;

  void FixedUpdate() {
    velocity = (transform.position - lastPos) / Time.fixedDeltaTime;
    lastPos = transform.position;
  }

	// Use this for initialization
	void Start () {
    lastPos = Vector3.zero;
    velocity = Vector3.zero;

   controller =  VRTK_ControllerReference.GetControllerReference(gameObject);
    rb = GetComponent<Rigidbody>();
	}
	
	void OnCollisionEnter(Collision col) {
    Volleyball ball = col.gameObject.GetComponent<Volleyball>();
    if (ball != null) {
      //float handForce = VRTK_DeviceFinder.GetControllerVelocity(controller).magnitude * impactScalar;
      //ContactPoint point = col.contacts[0];
      //col.rigidbody.AddForceAtPosition( (-point.normal.normalized * handForce) , point.point);
      //col.rigidbody.AddForce(upForce * Vector3.up);

      //taken from Physics for Game Developers by Bourg
      ContactPoint point = col.contacts[0];
      //Debug.Log("Controller Velocity: " + VRTK_DeviceFinder.GetControllerVelocity(controller));
      //Vector3 colRelVelocity = VRTK_DeviceFinder.GetControllerVelocity(controller) - col.rigidbody.velocity;
      Debug.Log("--------------Collision #" + ++collisionCount + "------------------");
      Debug.Log("Controller Velocity: " + velocity);
      Vector3 colRelVelocity = velocity - col.rigidbody.velocity;
      Debug.Log("Relative velocity: " + colRelVelocity);
      float j = (-(1 + RESTITUTION_COEF) * (Vector3.Dot(colRelVelocity, point.normal)) /
                (Vector3.Dot(point.normal, point.normal) * 
                ((1/rb.mass) + (1/col.rigidbody.mass)) ));
      //float j = (-(1 + RestitutionCoef) * (Vector3.Cross(col.relativeVelocity, col.contacts[0].normal)));
      //col.rigidbody.AddForce(Vector3.Cross(col.impulse, col.contacts[0].normal) / col.rigidbody.mass);
      Debug.Log("Impulse: " + j);
      Debug.Log("Ball velocity before hit: " + col.rigidbody.velocity);
      col.rigidbody.velocity += (j * point.normal) / col.rigidbody.mass;
      Debug.Log("Ball velocity after hit: " + col.rigidbody.velocity);

    }
  }
}
