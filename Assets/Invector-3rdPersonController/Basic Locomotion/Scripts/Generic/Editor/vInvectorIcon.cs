﻿using System.Collections.Generic;
using Gameplay;
using Invector.vCamera;
using Invector.vCharacterController;
using UnityEditor;
using UnityEngine;

namespace Invector
{
    [InitializeOnLoad]
    public class vInvectorIcon
    {
        static Texture2D texturePanel;
        static List<int> markedObjects;
        static vInvectorIcon()
        {
            EditorApplication.hierarchyWindowItemOnGUI += ThirdPersonControllerIcon;
            EditorApplication.hierarchyWindowItemOnGUI += ThirPersonCameraIcon;
        }
        static void ThirPersonCameraIcon(int instanceId, Rect selectionRect)
        {
            GameObject go = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (go == null) return;

            var tpCamera = go.GetComponent<vThirdPersonCamera>();
            if (tpCamera != null) DrawIcon("tp_camera", selectionRect);
        }

        static void ThirdPersonControllerIcon(int instanceId, Rect selectionRect)
        {
            GameObject go = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (go == null) return;

            var controller = go.GetComponent<OUThirdPersonController>();
            if (controller != null) DrawIcon("controllerIcon", selectionRect);
        }


        private static void DrawIcon(string texName, Rect rect)
        {
            Rect r = new Rect(rect.x + rect.width - 16f, rect.y, 16f, 16f);
            GUI.DrawTexture(r, GetTex(texName));
        }

        private static Texture2D GetTex(string name)
        {
            return (Texture2D)Resources.Load(name);
        }
    }
}