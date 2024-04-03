using Invector;
using Invector.Utils;
using Invector.vEventSystems;
using UnityEditor;

public class vMenuComponent
{
    public const string path = "Invector/Utils/";

    [MenuItem(path + "SimpleTrigger")]
    public static void AddSimpleTrigger()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)
            currentObject.AddComponent<vSimpleTrigger>();
    }

    [MenuItem(path + "AnimatorEventReceiver")]
    public static void AddAnimatorEventReceiver()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)        
            currentObject.AddComponent<vAnimatorEventReceiver>();        
    }

    [MenuItem(path + "MessageReceiver")]
    public static void AddMessageReceiver()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)        
            currentObject.AddComponent<vMessageReceiver>();        
    }

    [MenuItem(path + "MessageSender")]
    public static void AddMessageSender()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)        
            currentObject.AddComponent<vMessageSender>();        
    }

    [MenuItem(path + "EventWithDelay")]
    public static void AddEventWithDelay()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)        
            currentObject.AddComponent<vEventWithDelay>();        
    }

    [MenuItem(path + "DestroyGameObject")]
    public static void AddDestroyGameObject()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)        
            currentObject.AddComponent<vDestroyGameObject>();        
    }

    [MenuItem(path + "DestroyOnTrigger")]
    public static void AddDestroyOnTrigger()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)
            currentObject.AddComponent<vDestroyOnTrigger>();
    }

    [MenuItem(path + "PlayRandomClip")]
    public static void AddPlayRandomClip()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)
            currentObject.AddComponent<vPlayRandomClip>();
    }

    [MenuItem(path + "RotateObject")]
    public static void AddRotateObject()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)
            currentObject.AddComponent<vRotateObject>();
    }

    [MenuItem(path + "LookAtCamera")]
    public static void AddLookAtCamera()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)
            currentObject.AddComponent<vLookAtCamera>();
    }

    [MenuItem(path + "Instantiate")]
    public static void AddInstantiate()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)
            currentObject.AddComponent<vInstantiate>();
    }

    [MenuItem(path + "SetParent")]
    public static void AddSetParent()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)
            currentObject.AddComponent<vSetParent>();
    }

    [MenuItem(path + "ResetTransform")]
    public static void AddResetTransform()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)
            currentObject.AddComponent<vResetTransform>();
    }

    [MenuItem(path + "DestroyChildrens")]
    public static void AddDestroyChildrens()
    {
        var currentObject = Selection.activeGameObject;
        if (currentObject)
            currentObject.AddComponent<vDestroyChildrens>();
    }
}