//using UnityEditor;
//using UnityEditor.Callbacks;
//using UnityEngine;
//#if UNITY_IOS
//using UnityEditor.iOS.Xcode;
//using System.IO;
//#endif

//public class EntitlementsAndPlistPostProcess : ScriptableObject
//{
//    public DefaultAsset entitlementsFile;

//    // For Firebase Dynamic Links
//    [PostProcessBuild]
//    public static void OnPostProcess(BuildTarget buildTarget, string buildPath)
//    {
//        if (buildTarget != BuildTarget.iOS)
//            return;

//#if UNITY_IOS
//        var plistPath = buildPath + "/Info.plist";
//        var plistDocument = new PlistDocument();
//        plistDocument.ReadFromString(File.ReadAllText(plistPath));
//        var rootDict = plistDocument.root;
//        rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);
//        File.WriteAllText(plistPath, plistDocument.WriteToString());
//#endif
//    }
//}