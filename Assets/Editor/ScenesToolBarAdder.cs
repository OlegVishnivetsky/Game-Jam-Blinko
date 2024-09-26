using UnityEditor;
using UnityEditor.SceneManagement;
using Utils;

public class ScenesToolBarAdder : Editor
{
    [MenuItem("Scenes/Bootstrap")]
    private static void LoadBootstrap()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene($"Assets/Project/Scenes/{Scenes.BOOT}.unity");
    }

    [MenuItem("Scenes/Empty")]
    private static void LoadEmpty()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene($"Assets/Project/Scenes/{Scenes.EMPTY}.unity");
    }

    [MenuItem("Scenes/GRPT")]
    private static void LoadGRPT()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene($"Assets/Project/Scenes/{Scenes.GRPT}.unity");
    }

    [MenuItem("Scenes/Testing")]
    private static void LoadTesting()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene($"Assets/Project/Scenes/{Scenes.TESTING}.unity");
    }

    [MenuItem("Scenes/Main Menu")]
    private static void LoadMainMenu()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene($"Assets/Project/Scenes/{Scenes.MAIN_MENU}.unity");
    }

    [MenuItem("Scenes/Gameplay")]
    private static void LoadPlayMode()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene($"Assets/Project/Scenes/{Scenes.GAMEPLAY}.unity");
    }
}
