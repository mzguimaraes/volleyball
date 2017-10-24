using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Collider for physics impact
[RequireComponent (typeof(Collider), typeof(Rigidbody))]
public class VBallHitter : MonoBehaviour {

  private SteamVR_TrackedController controller;

  public float hapticFactor = 100f;
  public float impactScalar = 10f;

  private Rigidbody rb;
  private Vector3 prevPos; //tracked using Update's time step -- would use FixedUpdate if i could achieve a LateFixedUpdate effect
  private Vector3 vel; //velocity


  void Awake() {
    controller = GetComponent<SteamVR_TrackedController>();
    rb = GetComponent<Rigidbody>();
    prevPos = transform.position;
  }

  Vector3 UpdateVelocity() {
    //returns new Velocity
    Vector3 dx = transform.position - prevPos;
    vel = dx / Time.deltaTime;

    return vel;
  }

  void Update() {
    UpdateVelocity();
  }

  void LateUpdate() {
    //want this to happen as late as possible, after all other game logic
    prevPos = transform.position;
  }

  void TriggerHapticFeedback(float impactMagnitude) {
    //convert magnitude to a reasonable ushort between 1..3999
    ushort mag = (ushort) Mathf.CeilToInt( Mathf.Clamp(impactMagnitude, 1000f, 2000f) );
    //find index of this controller
    
    if (controller != null) {
      int index = (int)controller.controllerIndex;

      //If impacted head object, vibrate both controllers (TODO, should probably move this logic to a separate script)
      //else vibrate impacted controller

      //SteamVR_Controller.Input(index).TriggerHapticPulse(mag);
      SteamVR_Controller.Input(index).TriggerHapticPulse(500);
    }

  }

  void OnCollisionEnter(Collision col) {
    Volleyball ball = col.gameObject.GetComponent<Volleyball>();
    if (ball != null) {
      //hit volleyball
      //add impact force: magnitude of this obj's velocity, direction of collision normal
      ContactPoint[] contacts = col.contacts;

      //if length > 1 then i need to rewrite my code bc i don't understand what's happening here
      if (contacts.Length > 1) {
        Debug.LogError("More than one contact point?!?!?");
      }

      ContactPoint cPoint = contacts[0];

      //FT = MV
      //F = MV/T
      //where V = vel.magnitude
      col.rigidbody.AddForceAtPosition(-cPoint.normal.normalized * vel.magnitude * (1f / Time.deltaTime) * impactScalar, cPoint.point);

      TriggerHapticFeedback(hapticFactor * (vel.magnitude + col.rigidbody.velocity.magnitude) );
    }
  }

}
