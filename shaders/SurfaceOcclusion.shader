Shader "8thWall/DepthMask" {
  // NOTE: Add this shader to the list of "Always Included Shaders"
  // under Edit -> Project Settings -> Graphics !!

  SubShader {
    // Render the mask after regular geometry, but before masked geometry and
    // transparent things.
    Tags {"Queue" = "Geometry" }

    // Don't draw in the RGBA channels; just the depth buffer
    ZWrite On
    ZTest LEqual
    ColorMask 0

    // Do nothing specific in the pass:
    Pass {}
  }
}
