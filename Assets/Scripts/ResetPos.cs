using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FRL.IO;

public class ResetPos : MonoBehaviour, IGlobalTouchpadPressDownHandler {

  public SteamVR_TrackedController leftController;
  public SteamVR_TrackedController rightController;

  Rigidbody rb;

  private Vector3 originalPos;


  private void OnEnable() {
    //leftController.Gripped += HandleGripped;
    //rightController.Gripped += HandleGripped;
    leftController.PadClicked += HandlePadClicked;
    rightController.PadClicked += HandlePadClicked;
  }

  private void OnDisable() {
    //leftController.Gripped -= HandleGripped;
    //rightController.Gripped -= HandleGripped;
    leftController.PadClicked -= HandlePadClicked;
    rightController.PadClicked -= HandlePadClicked;
  }

	// Use this for initialization
	void Start () {
    originalPos = transform.position;
    rb = GetComponent<Rigidbody>();
  }
	
  void Reset() {
    transform.position = originalPos;
    transform.rotation = Quaternion.identity;

    //attempt to reset velocity
    if (rb != null) {
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
    } else {
      Debug.Log("no rb");
    }
  }

	// Update is called once per frame
	void Update () {
    if (Input.GetKeyDown(KeyCode.Space)) {
      Reset();
    }
	}

  private void HandlePadClicked(object sender, ClickedEventArgs e) {
    Reset();
  }

  private void HandleGripped(object sender, ClickedEventArgs e) {
    Reset();
  }

  public void OnGlobalTouchpadPressDown(VREventData eventData) {
    Reset();
  }
}
