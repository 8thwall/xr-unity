using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPointsRenderer : MonoBehaviour {
  public float POINT_WIDTH = 0.04267f;  // Golf ball
  public Color POINT_COLOR = new Color(5.0f/255, 106.0f/255, 50.0f/255);

  private XRController xr_;
  private List<PointWithRenderer> points_;

  private class PointWithRenderer {
    public readonly GameObject point;
    public readonly MeshRenderer renderer;
    public PointWithRenderer(float pointWidth, Color pointColor) {
      point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      renderer = point.GetComponent<MeshRenderer>();
      renderer.material.color = pointColor;
      point.transform.localScale = new Vector3(pointWidth, pointWidth, pointWidth);
    }
    public void RenderPoint(Vector3 position) {
      point.transform.position = position;
      renderer.enabled = true;
    }
  }

  void RenderPoints(List<XRWorldPoint> pts) {
    int v = 0;
    foreach (var pt in pts) {
      // Center
      if (points_.Count < v + 1) {
        points_.Add(new PointWithRenderer(POINT_WIDTH, POINT_COLOR));
      }

      points_[v].RenderPoint(pt.position);
      ++v;
    }

    for (; v < points_.Count; ++v) {
      points_[v].renderer.enabled = false;
    }
  }

  void Start() {
    xr_ = GameObject.FindWithTag("XRController").GetComponent<XRController>();
    points_ = new List<PointWithRenderer>();
  }

  void Update () {
    RenderPoints(xr_.GetWorldPoints());
  }

}
