using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Collider for physics impact
[RequireComponent (typeof(Collider), typeof(Rigidbody))]
public class VBallHitter : MonoBehaviour {

  public float hapticFactor = 100f;

  private Rigidbody rb;

  void Awake() {
    rb = GetComponent<Rigidbody>();
  }

  void TriggerHapticFeedback(float impactMagnitude) {
    //convert magnitude to a reasonable ushort between 1..3999
    ushort mag = (ushort) Mathf.CeilToInt( Mathf.Clamp(impactMagnitude, 1000f, 2000f) );
    //find index of this controller
    int index = (int) gameObject.GetComponent<SteamVR_TrackedController>().controllerIndex;

    SteamVR_Controller.Input(index).TriggerHapticPulse(mag);
  }

  void OnCollisionEnter(Collision col) {
    Volleyball ball = col.gameObject.GetComponent<Volleyball>();
    if (ball != null) {
      //hit volleyball
      //add impact force: magnitude of this obj's velocity, direction of collision normal
      Vector3 vel = rb.velocity;
      ContactPoint[] contacts = col.contacts;

      //if length > 1 then i need to rewrite my code bc i don't understand what's happening here
      if (contacts.Length > 1) {
        Debug.LogError("More than one contact point?!?!?");
      }

      ContactPoint cPoint = contacts[0];
      col.rigidbody.AddForceAtPosition(cPoint.normal.normalized * vel.magnitude, cPoint.point);

      TriggerHapticFeedback(hapticFactor * (vel.magnitude + col.rigidbody.velocity.magnitude) );
    }
  }

}
