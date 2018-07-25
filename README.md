# 8th Wall XR

![Banner](images/banner.png)

8th Wall XR for Unity enables developers to quickly and easily build AR apps that run on all Android or iOS devices. It provides easy-to use APIs for lighting, surfaces, textures, and 3D tracking.

- [Resources](#resources)
- [Projects](#sample-unity-projects)

- - -

# Resources

* [Download 8th Wall XR for Unity](https://console.8thwall.com/downloads)
* [Documentation](https://docs.8thwall.com/)
* [8th Wall Website](https://www.8thwall.com)

# Sample Unity Projects

* [8th Wall XR - Tutorial](https://github.com/8thwall/xr-unity/tree/master/projects/8thWallXR-Tutorial) - A simple unity scene containing a table with a TV on top of it. Use as a base project to learn how to AR-enable after downloading the 8th Wall XR unity package.
* [8th Wall XR - Demo](https://github.com/8thwall/xr-unity/tree/master/projects/8thWallXR-Demo) - The same scene as above, but preconfigured with all of the 8th Wall XR controllers added to scene objects. Just install the 8th Wall XR unity package and build!

# Scripts

* [DragAlongSurface.cs](https://github.com/8thwall/xr-unity/tree/master/scripts/DragAlongSurface.cs) - Tap on an object and drag to move it along a surface
* [MeshVisualizer.cs](https://github.com/8thwall/xr-unity/blob/master/scripts/MeshVisualizer.cs) - Displays the mesh of an object.  Attach to the same object that has an XRSurfaceController, and enable "Deform To Surface" to visualize the shape of the detected surface"
* [PlaceObject.cs](https://github.com/8thwall/xr-unity/tree/master/scripts/PlaceObject.cs) - Tap to place an object in your scene.  If tap hits a surface, object will be placed on surface.  If not, it will be placed at touch point at a specified distance in front of the camera.
* [SunlightController.cs](https://github.com/8thwall/xr-unity/tree/master/scripts/SunlightController.cs) - A script that controls the rotation of a Directional Light in your scene based on the position of the sun. Makes AR shadows look more realistic by aligning their direction with real world shadows outside.
* [WorldPointsRenderer.cs](https://github.com/8thwall/xr-unity/tree/master/scripts/WorldPointsRenderer.cs) - Visualize detected WorldPoints in your scene. Attach to any game object.
* [XRImageDetectionController.cs](https://github.com/8thwall/xr-unity/blob/master/scripts/XRImageDetectionController.cs) - Script that makes it easy to define a set of image targets to detect.
* [XRImageDetectionTargetController.cs](https://github.com/8thwall/xr-unity/blob/master/scripts/XRImageDetectionTargetController.cs) - Attach this to the game object you want to be anchored/triggered when a tracked image it detected.
