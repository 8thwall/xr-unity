using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAlongSurface : MonoBehaviour {
  private bool holding;

  void Start () {
    holding = false;
  }

  void Update() {
    if (holding) {
      Move();
    }

    // One finger
    if (Input.touchCount == 1) {

      // Tap on Object
      if (Input.GetTouch(0).phase == TouchPhase.Began ) {
        Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit, 100f)) {
          if (hit.transform == transform) {
            holding = true;
          }
        }
      }

      // Release 
      if (Input.GetTouch(0).phase == TouchPhase.Ended) {
        holding = false;
      }
    }
  }

  void Move() {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
    // The GameObject with an XRSurfaceController attached should be on layer "Surface"
    if(Physics.Raycast(ray, out hit, 30.0f, LayerMask.GetMask("Surface"))) {
      transform.position = new Vector3(hit.point.x,
                                      transform.position.y,
                                      hit.point.z);
    }
  }
}
