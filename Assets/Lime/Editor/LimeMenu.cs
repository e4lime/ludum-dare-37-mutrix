using UnityEngine;
using UnityEditor;

namespace Lime.EditorUtility {
    public static class LimeMenu {

    #region CreatePrsojectStructure
	    [MenuItem("limE/Project/Create project structure")]
	    static void CreateProjectStructure() {
		    CreateFolderNoDuplicate("Assets", "Materials");
		    CreateFolderNoDuplicate("Assets", "Textures");
		    CreateFolderNoDuplicate("Assets", "GUI");
		    CreateFolderNoDuplicate("Assets", "Effects");
		    CreateFolderNoDuplicate("Assets", "Meshes");
		    CreateFolderNoDuplicate("Assets/Meshes", "Actors");
		    CreateFolderNoDuplicate("Assets/Meshes", "Structures");
		    CreateFolderNoDuplicate("Assets/Meshes", "Props");
		    CreateFolderNoDuplicate("Assets", "Plugins");
		    CreateFolderNoDuplicate("Assets", "Prefabs");
		    CreateFolderNoDuplicate("Assets/Prefabs", "Actors");
		    CreateFolderNoDuplicate("Assets", "Resources");
		    CreateFolderNoDuplicate("Assets/Resources", "Actors");
		    CreateFolderNoDuplicate("Assets", "Scenes");
		    CreateFolderNoDuplicate("Assets/Scenes", "GUI");
		    CreateFolderNoDuplicate("Assets/Scenes", "Levels");
		    CreateFolderNoDuplicate("Assets/Scenes", "TestScenes");
		    CreateFolderNoDuplicate("Assets", "Scripts");
		    CreateFolderNoDuplicate("Assets/Scripts", "MyGenericScripts");
		    CreateFolderNoDuplicate("Assets/Scripts", "MyGameScripts");
		    CreateFolderNoDuplicate("Assets/Scripts/MyGameScripts", "Gameplay");
		    CreateFolderNoDuplicate("Assets/Scripts/MyGameScripts", "Tools");
		    CreateFolderNoDuplicate("Assets/Scripts", "ThirdPartyScripts");
		
		

		
	    }

	    private static void CreateFolderNoDuplicate(string parent, string name) {
		    bool folderExists = false;
		    string folderPath = parent + "/" +name;

    #if UNITY_5
		    folderExists = AssetDatabase.IsValidFolder(folderPath);
    #else 
		    folderExists = Directory.Exists(folderPath);
    #endif

		    if (!folderExists) {
			    AssetDatabase.CreateFolder(parent, name);
		    }

	    }

    #endregion

	    #region CreateSceneStructure
	    [MenuItem("limE/Scene/Create scene structure")]
	    static void CreateSceneStructure(){

		    GameObject cameras = CreateGameObject("Cameras");
		    GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		    if (camera != null) {
			    Undo.SetTransformParent(camera.transform, cameras.transform, "Set camera to cameras");
		    }
		    CreateGameObject("Dynamic Objects");
		    CreateGameObject(new string[]{"Gameplay", "Actors", "Items"});
		    CreateGameObject(new string[]{"GUI", "HUD", "PauseMenu"});
		    CreateGameObject("Management");
		    GameObject lights = CreateGameObject("Lights");
		    CreateGameObject(new string[]{"World", "Ground", "Props", "Structure"});
    #if UNITY_5
		    GameObject directionalLight = GameObject.Find("Directional Light");
		    if (directionalLight != null) {
			    Undo.SetTransformParent (directionalLight.transform, lights.transform, "Set Directional light to Lights");
		    }
    #endif

	    }

	    private static GameObject CreateGameObject(string[] names){
		    GameObject parent = CreateGameObject(names[0]);
		    for(int i = 1; i < names.Length; ++i) {
			    CreateGameObject(names[i], parent.transform);
		    }
		    return parent;
	    }

	    private static GameObject CreateGameObject(GameObject parent, string[] names){;
		    for(int i = 0; i < names.Length; ++i) {
			    CreateGameObject(names[i], parent.transform);
		    }
		    return parent;
	    }

	    private static GameObject CreateGameObject(string name){
		    return CreateGameObject(name, null);
	    }


	    private static GameObject CreateGameObject(string name, Transform parent) {
		    GameObject go = new GameObject();
		    Undo.RegisterCreatedObjectUndo(go, "Created " + name);

		    go.name = name;
		    go.transform.parent = parent;
		    return go;
	    }
	    #endregion
    }
}