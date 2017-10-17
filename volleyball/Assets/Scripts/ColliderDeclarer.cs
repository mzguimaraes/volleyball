using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDeclarer : MonoBehaviour {

  void OnCollisionExit(Collision col) {
    Debug.Log(col.gameObject.name);
  }
}
