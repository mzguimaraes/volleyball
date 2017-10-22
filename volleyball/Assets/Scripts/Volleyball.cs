using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider), typeof(Rigidbody))]
public class Volleyball : MonoBehaviour {
  //tag class

  //debug
  //private Rigidbody rb;
  //private Vector3 prevPos;

  //void Start() {
  //  rb = GetComponent<Rigidbody>();
  //  prevPos = transform.position;
  //}

  //void FixedUpdate() {
  //  Debug.Log(rb.velocity + ", " + ( (transform.position - prevPos) ) / Time.fixedDeltaTime );
  //  prevPos = transform.position;
  //}
}
