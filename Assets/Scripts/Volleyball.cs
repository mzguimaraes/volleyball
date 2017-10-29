using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider), typeof(Rigidbody))]
public class Volleyball : MonoBehaviour {
  //tag class

  private TrailRenderer trail;
  private Rigidbody rb;

  public float trailThreshold = 1f;

  void Awake() {
    rb = GetComponent<Rigidbody>();
    trail = GetComponent<TrailRenderer>();
    trail.enabled = false;
  }

  void Update() {
    if (!trail.enabled && rb.velocity.magnitude > trailThreshold)
      trail.enabled = true;
    else if (trail.enabled && rb.velocity.magnitude < trailThreshold)
      trail.enabled = false;
    trail.time = (rb.velocity.magnitude - trailThreshold) / 4f;
  }

  void OnCollisionEnter(Collision col) {
    if (col.gameObject.GetComponent<VBallHitter>() || col.gameObject.GetComponent<RallyBot>())
      Debug.Log("Volleyball force: " + col.impulse / Time.fixedDeltaTime);
  }
}
