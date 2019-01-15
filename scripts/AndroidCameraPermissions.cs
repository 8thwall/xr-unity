using UnityEngine;
#if UNITY_2018_3_OR_NEWER && PLATFORM_ANDROID
using UnityEngine.Android;
#endif

/* Unity made a change in 2018.3 where Android apps no longer automatically request camera permissions.
 * Android permissions (camera, microphone, location, etc) are now controlled by a new API.
 * 
 * More information can be found here:
 * https://docs.unity3d.com/2018.3/Documentation/Manual/android-RequestingPermissions.html
 * 
 * This is a very basic example that checks if your app has camera permissions, and
 * if not, requests them.  Attach this to a gameObject in your scene.
*/

public class AndroidCameraPermissions : MonoBehaviour {

  void Awake() {
    #if UNITY_2018_3_OR_NEWER && PLATFORM_ANDROID
    if (!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
      Debug.Log("Requesting Android Camera Permissions");
      Permission.RequestUserPermission(Permission.Camera);
    }
    #endif
  }
}
