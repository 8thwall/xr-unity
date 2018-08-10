using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MeshThing {
  public GameObject holder {get;set;}
  public MeshFilter filter {get;set;}
  public MeshCollider collider {get;set;}
  public MeshRenderer renderer {get;set;}
}

/**
 * Visualize the geometry of the surface as is.
 */
public class SurfaceVisualizer : MonoBehaviour {
  private XRController xr_;
  private Dictionary<long, MeshThing> idToMesh_;
  private Dictionary<long, XRSurface> idToSurface_;
  public Material horizontalSurfaceMaterial_;
  public Material verticalSurfaceMaterial_;
  public Text statusText_ = null;

  void Start() {
    xr_ = GameObject.FindWithTag("XRController").GetComponent<XRController>();
    idToMesh_ = new Dictionary<long, MeshThing>();
    idToSurface_ = new Dictionary<long, XRSurface>();
    Assert.IsNotNull(xr_);
    if (statusText_ == null) {
      Debug.Log("Status Text is null. No update will happen");
    }
  }

  void Update() {
    if (xr_.DisabledInEditor()) {
      return;
    }

    long surfaceId = xr_.GetActiveSurfaceId();
    var surface = xr_.GetSurface(surfaceId);
    if (surface != XRSurface.NO_SURFACE && !idToMesh_.ContainsKey(surfaceId)) {
      MeshThing meshThing = new MeshThing();
      meshThing.holder = new GameObject();
      meshThing.holder.transform.parent = gameObject.transform;
      meshThing.filter = meshThing.holder.AddComponent<MeshFilter>();
      meshThing.collider = meshThing.holder.AddComponent<MeshCollider>();
      meshThing.renderer = meshThing.holder.AddComponent<MeshRenderer>();
      meshThing.filter.mesh = surface.mesh;
      meshThing.collider.sharedMesh = surface.mesh;
      if (surface.type == XRSurface.Type.VERTICAL_PLANE) {
        meshThing.renderer.material = verticalSurfaceMaterial_;
      } else {
        meshThing.renderer.material = horizontalSurfaceMaterial_;
      }
      idToMesh_[surfaceId] = meshThing;

      // Save all surfaces that we have seen
      idToSurface_[surfaceId] = surface;
    }

    // Update Status Text
    if (statusText_ != null) {
      int totalSurfaceCount = idToSurface_.Count;
      int verticalSurfaceCount = 0;
      foreach(var s in idToSurface_.Values) {
        if (s.type == XRSurface.Type.VERTICAL_PLANE) {
          verticalSurfaceCount++;
        }
      }
      statusText_.text = string.Format("Surface {0} Vertical /{1} Total",
        verticalSurfaceCount, totalSurfaceCount);
    }
  }

}
