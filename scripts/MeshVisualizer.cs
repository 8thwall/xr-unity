using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
NOTE: Make sure you add "Particles/Alpha Blended Premultiply" to the list of
Always Included Shaders under Edit -> Project Settings -> Graphics !!
*/

public class MeshVisualizer : MonoBehaviour {

  private MeshRenderer meshRenderer;
  private MeshFilter meshFilter;
  private static readonly Color PURPLE = new Color(0.541f, 0.0f, 0.796f, 0.5f);
  private static readonly float LINE_WIDTH = 0.007f;
  private static readonly float POINT_WIDTH = 0.015f;

  private class LineWithRenderer {
    public readonly GameObject line;
    public readonly LineRenderer renderer;
    
    public LineWithRenderer() {
      line = new GameObject();
      renderer = line.AddComponent<LineRenderer>();
      renderer.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
      renderer.startColor = PURPLE;
      renderer.endColor = PURPLE;
      renderer.startWidth = LINE_WIDTH;
      renderer.endWidth = LINE_WIDTH;
    }

    public void RenderLine(Vector3 v0, Vector3 v1) {
      renderer.SetPosition(0, v0);
      renderer.SetPosition(1, v1);
      renderer.enabled = true;
    }
  }

  private class PointWithRenderer {
    public readonly GameObject point;
    public readonly MeshRenderer renderer;
    public PointWithRenderer() {
      point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      point.transform.localScale = new Vector3(POINT_WIDTH, POINT_WIDTH, POINT_WIDTH);
      renderer = point.GetComponent<MeshRenderer>();
      renderer.material.color = PURPLE;
    }
    public void RenderPoint(Vector3 v0) {
      point.transform.position = v0;
      renderer.enabled = true;
    }
  }

  private List<LineWithRenderer> meshLines;
  private List<PointWithRenderer> meshPoints;

  void RenderMesh(Mesh mesh) {
    // Add each edge to a set so that we can deduplicate edges and only render each edge once.
    HashSet<KeyValuePair<int, int>> lines = new HashSet<KeyValuePair<int, int>>();
    for (int t = 0; t < mesh.triangles.Length; t += 3) {
      List<int> vs = new List<int>();
      vs.Add(mesh.triangles[t]);
      vs.Add(mesh.triangles[t + 1]);
      vs.Add(mesh.triangles[t + 2]);
      vs.Sort();
      lines.Add(new KeyValuePair<int, int>(vs[0], vs[1]));
      lines.Add(new KeyValuePair<int, int>(vs[0], vs[2]));
      lines.Add(new KeyValuePair<int, int>(vs[1], vs[2]));
    }

    int l = 0;
    foreach (KeyValuePair<int, int> line in lines) {
      if (meshLines.Count < l + 1) {
        meshLines.Add(new LineWithRenderer());
      }
      meshLines[l].RenderLine(
        mesh.vertices[line.Key] + transform.position,
        mesh.vertices[line.Value] + transform.position);

      l++;
    }

    for (; l < meshLines.Count; ++l) {
      meshLines[l].renderer.enabled = false;
    }

    int v = 0;
    for (; v < mesh.vertices.Length; ++v) {
      if (meshPoints.Count < v + 1) {
        meshPoints.Add(new PointWithRenderer());
      }
      meshPoints[v].RenderPoint(mesh.vertices[v] + transform.position);
    }

    for (; v < meshPoints.Count; ++v) {
      meshPoints[v].renderer.enabled = false;
    }
  }

  void Start() {
    meshLines = new List<LineWithRenderer>();
    meshPoints = new List<PointWithRenderer>();

    meshFilter = gameObject.GetComponent<MeshFilter>();
    if (meshFilter == null) {
      meshFilter = gameObject.AddComponent<MeshFilter>() as MeshFilter;
    }
    meshRenderer = gameObject.GetComponent<MeshRenderer>();
    if (meshRenderer == null) {
      meshRenderer = gameObject.AddComponent<MeshRenderer>() as MeshRenderer;
    }

    meshRenderer.enabled = false;
  }

  void Update () {
    if (meshFilter.mesh == null || meshFilter.mesh.vertices.Length == 0) {
      return;
    }
    RenderMesh(meshFilter.mesh);
  }
}
