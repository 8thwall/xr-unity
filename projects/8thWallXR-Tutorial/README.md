# 8th Wall XR Tutorial App

# Overview

This sample application contains a simple scene consisting of a TV sitting on a table.  With a few simple steps you will:

* Control the position and rotation of the camera in the scene as you move your device in the real world by adding an **XRCameraController** to the camera.
* Capture camera input and use it as the **background** of the scene by adding an **XRVideoController** to the camera.
* Capture camera input and use it as a "live" texture on the TV Screen by adding an **XRVideoTextureController** to the TV Screen.
* Adjust the intensity of the scene light based on the lighting conditions in the world around you by adding an **XRLightController** to the light.
* Place the table onto a detected surface by adding an **XRSurfaceController** to the table.

## Download Unity

If you don't already have Unity installed, please download it from <a href='https://www.unity3d.com' target='_blank'>www.unity3d.com</a>

Note: During installation, make sure you install **BOTH** Android & iOS build support packages, even if you only plan to develop for one:

* Android Build Support
* iOS Build Support

![Unity Component Selection](images/unity-component-selection.png)

## Start a New Unity Project

Open Unity and on the welcome screen, click "New" to create a new project.  Give it a name and click "Create Project"

![Create Unity Project](images/getting-started-new-project.png)

## Download the sample Unity app

[Download](https://github.com/8thwall/xr-unity/raw/master/projects/8thWallXR-Tutorial/8thWallXR-Tutorial.unitypackage) the sample Unity project to augment with 8th Wall XR.

Double click on the .unitypackage file downloaded, and import all of the objects into the Unity project.  This sample application contains a simple scene (called "Main") consisting of a TV sitting on a table::

![Base Project](images/getting-started-base-scene.png)

## Download 8th Wall XR for Unity

If you don't already have an 8th Wall account, sign up for one.  It's free. Once logged in, the 8th Wall XR Unity package is available here:

<a href='https://console.8thwall.com/downloads' target='_blank'>https://console.8thwall.com/downloads</a>

## Install XR

Add 8th Wall XR to your Unity project.  Locate the xr-<version>.unityplugin file you just downloaded and simply double-click on it.  A progress bar will appear as it's loaded.

Once finished, a window will display the contents of the XR package.  Leave all of the boxes checked and click "Import".

![import-xr-unity-package](images/getting-started-import-xr-unity-package.png)

## Generate App Key

1. To create an app key, go to [https://console.8thwall.com](https://console.8thwall.com) and login. Select "Applications" from the left navigation and then click the **"+ Create a new App Key"** button:

![CreateAppKey1](images/console-app-create1.png)

2. Enter the bundle identifier of the application you’ll be creating, then click **"+ Create"**

**IMPORTANT**: The bundle identifier entered will here needs to be identical to the bundle identifier in your Unity project.

![CreateAppKey2](images/console-app-create2.png)

Note: A bundle identifier is typically written in [reverse domain name notation](https://en.wikipedia.org/wiki/Reverse_domain_name_notation#Examples) and identifies your application in the app store. It should be unique.

## Install App Key

1. Select "Applications" from the left navigation.
2. In the "Your App Keys" table, locate the row containing your App and its associated App Key. 
3. Click the "Copy" button.

![AddAppKey1](images/console-app-copy1.png)

4. In Unity, select **Assets / XR / XRAppSettings**.  Paste your key into the **App Key** field.
5. **IMPORTANT**: Go to File -> Save Project to save these settings.

**IMPORTANT**: Make sure that the Bundle Identifier matches the bundle identifier you entered in Step #2.

![AddAppKey2](images/console-app-copy2.png)

## Create an XRController

Create an XRController object in your scene. You can do this in the Hierarchy panel via Create -> XRController or GameObject menu -> XRController:

![create-xrcontroller](images/getting-started-create-xrcontroller.png)

## XRCameraController

XRCameraController attaches to your Main Camera.  It's primary function is to control the position and rotation of the camera in your scene as you move your device in the real world.

Select the Camera in your scene and perform one of the following actions:

* Drag the XRCameraController.cs script (default location: Assets/XR/Scripts/XRCameraController.cs) onto to the inspector panel for your camera
* Click "Add Component" in the inspector panel and search for "XR Camera Controller"

![XRCameraController Setup](images/xr-camera-controller.png)

## XRVideoController

XRVideoController script also attaches to your camera.  It captures camera input and sets it as the background of your scene.

Select the Camera in your scene and perform one of the following actions:

* Drag the XRVideoController.cs script (default location: Assets/XR/Scripts/XRVideoController.cs) onto to the inspector panel for your camera
* Click the "Add Component" button in the inspector panel and search for "XR Video Controller"

![XRVideoController Setup](images/xr-video-controller.png)

## XRLightController

XRLightController scipt attaches to the light in your scene.  It adjusts the intensity of your scene light based on the lighting conditions in the world around you.

Select the light in your scene and perform one of the following actions:

* Drag the XRLightController.cs script (default location: Assets/XR/Scripts/XRLightController.cs) onto to the inspector panel for your light
* Click the "Add Component" button in the inspector panel and search for "XR Light Controller"

![XRLightController Setup](images/xr-light-controller.png)

## XRSurfaceController

XRSurfaceController attaches to a game object in your scene.  In this example, attach it to the Table.  When XR detects a surface, the table will be placed on that surface.

Select the Table in your scene and perform one of the following actions:

* Drag the XRSurfaceController.cs script (default location: Assets/XR/Scripts/XRSurfaceController.cs) onto to the inspector panel for your camera
* Click "Add Component" in the inspector panel and search for "XR Surface Controller"

![XRSurfaceController Setup](images/xr-surface-controller.png)

## XRVideoTextureController

XRVideoTextureController captures camera input and sets it as the main texture on a game object.  In this sample application, use it to display the what the camera captures on the TV screen.

Select the "Screen" object in your scene and perform one of the following actions:

* Drag the XRVideoTextureController.cs script (default location: Assets/XR/Scripts/XRVideoTextureController.cs) onto to the inspector panel for your camera
* Click the "Add Component" button in the inspector panel and search for "XR Video Texture Controller"

![XRVideoTextureController Setup](images/xr-video-texture-controller.png)

## Unity 2018 and Android Camera Permissions

**IMPORTANT**: Unity made a change in 2018.3 where Android apps no longer automatically request camera permissions. Android permissions (camera, microphone, location, etc) are now controlled by a new API.

More information can be found here:
https://docs.unity3d.com/2018.3/Documentation/Manual/android-RequestingPermissions.html

If you are building for Android and using Unity 2018.3 (or newer) you'll need to add a script that checks if your app has camera permissions, and
if not, requests them.  

Inside the "Scripts/" directory, you'll see a script called **AndroidCameraPermissions.cs**. In this screenshot we attach it to the Main Camera, but any game object should work.

![AndroidCameraPermissions](images/android-camera-permissions.png)

## Adjust Player Settings

Before you build the app, make sure to set a few things:

Go to **File -> Build Settings** and click "Add Open Scenes".

![Build Settings](images/build-settings.png)

## Build Application

In this example we will be building for iOS.  Click "Build" and in the pop up window give the build a name.  Click Save to have Unity generate the XCode project.

![Unity Build](images/unity-build.png)

Once complete, a window will open up with the location of the code project.  Open it and double click "Unity-iPhone.xcodeproj" to open the XCode project.

![XCode Project](images/xcode-project.png)

Inside XCode, make sure to set Team, then click Play to compile, install and run the app on your phone!

![XCode Settings](images/xcode-settings.png)


