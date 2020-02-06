public class AutoIncrementVersionCodeInCloudBuild : UnityEngine.MonoBehaviour
{
#if UNITY_CLOUD_BUILD
    public static void PreExport(UnityEngine.CloudBuild.BuildManifestObject manifest)
    {
        string buildNumber = manifest.GetValue("buildNumber", "0");
        UnityEngine.Debug.LogWarning("Setting build number to " + buildNumber);
        UnityEditor.PlayerSettings.Android.bundleVersionCode = int.Parse(buildNumber);
        UnityEditor.PlayerSettings.iOS.buildNumber = buildNumber;
    }
#endif
}
