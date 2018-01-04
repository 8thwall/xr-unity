using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour {
	
  // If tap doesn't hit a surface, how far object should be placed in front of camera
  public float distanceFromCamera = 1.0f;

  // Adjust this if the transform isn't at the bottom edge of the object
  public float heightAdjustment = 0.0f;

  // Prefab to instantiate.  If null, the script will instantiate a Cube
  public GameObject prefab;

  // Scale factor for instantiated GameObject
  public float objectScale = 1.0f;
  
  private GameObject myObj;

  void Update() {
    // Tap to place
    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began ) {

      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
      // The "Surface" GameObject with an XRSurfaceController attached should be on layer "Surface"
      // If tap hits surface, place object on surface
      if(Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Surface"))) {
        CreateObject(new Vector3(hit.point.x, hit.point.y + heightAdjustment, hit.point.z));
      } else {
      // It tap doesn't hit surface, place in "air" at touch point in front of camera at a distance of distanceFromCamera
        Vector3 touchPos = Input.GetTouch (0).position;
        touchPos.y = touchPos.y + heightAdjustment;
        touchPos.z = distanceFromCamera;

        Vector3 objPos = Camera.main.ScreenToWorldPoint (touchPos);

        CreateObject(objPos);
      }
    }
  }

  void CreateObject(Vector3 v) {
    // If prefab is specified, Instantiate() it, otherwise, place a Cube
    if (prefab) {
      myObj = GameObject.Instantiate(prefab);
    } else {
      myObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
    myObj.transform.position = v;
    myObj.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
  }
}
