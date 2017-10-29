using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyBot : MonoBehaviour {

  public float travelDistance = .7f;
  public float travelTime = 2f;

  private float lifetime = 0f;

	// Update is called once per frame
	void Update () {
    lifetime += Time.deltaTime;
    //transform.position = new Vector3(transform.position.x, transform.position.y, travelDistance * Mathf.Sin(lifetime));
    transform.position = new Vector3(transform.position.x, transform.position.y,travelDistance * Mathf.Sin( Mathf.LerpUnclamped(-1f, 1f, lifetime / travelTime) ));
	}

  void OnCollisionEnter(Collision col) {
    //reflect ball with same force it had at moment of collision
    if (col.gameObject.GetComponent<Volleyball>()) {
      Vector3 inForce = col.impulse / Time.fixedDeltaTime;
      Debug.Log("RallyBot applying force " + (2f * inForce));
      col.rigidbody.AddForceAtPosition(2f * inForce, col.contacts[0].point);
    }
  }
}
