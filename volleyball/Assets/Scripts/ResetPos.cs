using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FRL.IO;

public class ResetPos : MonoBehaviour, IGlobalTouchpadPressDownHandler {

  public SteamVR_TrackedController leftController;
  public SteamVR_TrackedController rightController;

  private Vector3 originalPos;


  private void OnEnable() {
    leftController.Gripped += HandleGripped;
    rightController.Gripped += HandleGripped;
  }

  private void OnDisable() {
    leftController.Gripped -= HandleGripped;
    rightController.Gripped -= HandleGripped;
  }

	// Use this for initialization
	void Start () {
    originalPos = transform.position;
    Debug.Log("original pos: " + originalPos.ToString());

    //string[] joysticks = Input.GetJoystickNames();
    //foreach (string stick in joysticks) {
    //  Debug.Log(stick);
    //}
  }
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKeyDown(KeyCode.Space)) {
      Debug.Log("Resetting to " + originalPos.ToString());
      transform.position = originalPos;
    }
	}

  private void HandleGripped(object sender, ClickedEventArgs e) {
    transform.position = originalPos;
  }

  public void OnGlobalTouchpadPressDown(VREventData eventData) {
    transform.position = originalPos;
  }
}
