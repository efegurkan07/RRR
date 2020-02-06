
public class AutoIncrementVersionCodeInCloudBuild : UnityEngine.MonoBehaviour
{
#if UNITY_CLOUD_BUILD
    public static void PreExport(UnityEngine.CloudBuild.BuildManifestObject manifest)
    {
        var buildNumber = manifest.GetValue<string>("buildNumber");
        UnityEngine.Debug.Log("Setting build number to " + buildNumber);

        if (buildNumber == null || buildNumber.Equals("") || buildNumber.Equals("0"))
        {
            UnityEngine.Debug.LogError("Could not retrieve a build number from the manifest.");
            UnityEngine.Debug.LogError(manifest.ToString());
            
            throw new RuntimeException("Could not retrieve a build number from the manifest.");
        }
        
        UnityEditor.PlayerSettings.Android.bundleVersionCode = int.Parse(buildNumber);
        UnityEditor.PlayerSettings.iOS.buildNumber = buildNumber;
    }
#endif
}
