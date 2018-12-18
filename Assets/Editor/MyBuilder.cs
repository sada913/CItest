using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class MyBuilder {
	// ビルド実行でAndroidのapkを作成する例
	[UnityEditor.MenuItem("Tools/Build Project AllScene Android")]
	public static void BuildProjectAllSceneAndroid() {
		EditorUserBuildSettings.SwitchActiveBuildTarget( BuildTarget.Android );
		List<string> allScene = new List<string>();
		foreach( EditorBuildSettingsScene scene in EditorBuildSettings.scenes ){
			if (scene.enabled) {
				allScene.Add (scene.path);
			}
		}
		PlayerSettings.applicationIdentifier = "com.yourcompany.newgame";
        	PlayerSettings.statusBarHidden = true;
        	BuildPipeline.BuildPlayer( 
            		allScene.ToArray(),
            		"newgame.apk",
            		BuildTarget.Android,
            		BuildOptions.None
        	);
	}
	
	// ビルド実行でiOS用のXcodeプロジェクトを作成する例
	[UnityEditor.MenuItem("Tools/Build Project AllScene iOS")]
	public static void BuildProjectAllSceneiOS() {	
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS,BuildTarget.iOS);
		List<string> allScene = new List<string>();
		foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (scene.enabled) {
				allScene.Add (scene.path);
			}
		}

		BuildOptions opt = BuildOptions.SymlinkLibraries |
		                   BuildOptions.AllowDebugging |
		                   BuildOptions.ConnectWithProfiler |
		                   BuildOptions.Development;

		//BUILD for Device
		PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
		PlayerSettings.applicationIdentifier = "com.sdatoworks.newgame";
		PlayerSettings.statusBarHidden = true;
		var errorMsg_Device = BuildPipeline.BuildPlayer (
						allScene.ToArray(),
						"iOS",
						BuildTarget.iOS,opt
		                         );

		if (errorMsg_Device.summary.result == UnityEditor.Build.Reporting.BuildResult.Failed) {
		} else {
			// エラー処理
		}
	}

    [UnityEditor.MenuItem("Tools/Build Project AllScene Win")]
    public static void BuildProjectAllSceneWin()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        List<string> allScene = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                allScene.Add(scene.path);
            }
        }

        BuildOptions opt = BuildOptions.SymlinkLibraries |
                           BuildOptions.AllowDebugging |
                           BuildOptions.ConnectWithProfiler |
                           BuildOptions.Development;

        //BUILD for Device
        PlayerSettings.applicationIdentifier = "com.sdatoworks.newgame";
        var errorMsg_Device = BuildPipeline.BuildPlayer(
                        allScene.ToArray(),
                        "build/build.exe",
                        BuildTarget.StandaloneWindows64, opt
                                 );

        if (errorMsg_Device.summary.result == UnityEditor.Build.Reporting.BuildResult.Failed)
        {
        }
        else
        {
            // エラー処理
        }
    }
}
