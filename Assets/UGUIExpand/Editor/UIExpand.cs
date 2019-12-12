using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

namespace UGUIExpand
{
    public class UIExpand
    {
        [MenuItem("GameObject/UI Expand/SwitchUI",false,1)]
        public static void CreateSwitchUI()
        {
            SetCanvasSon(CreateModule.GetInstance().CreateSwitchUI());
        }

        private static void SetCanvasSon(GameObject son)
        {
            son.transform.SetParent(CreateCanvas().transform);
            son.transform.localPosition = Vector3.zero;
            son.transform.localScale = Vector3.one;
        }
        private static GameObject CreateCanvas()
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas == null || canvas.GetComponent<Canvas>() == null)
            {
                canvas = new GameObject("Canvas");
                canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.AddComponent<CanvasScaler>();
                canvas.AddComponent<GraphicRaycaster>();
            }
            GameObject eventSystem = GameObject.Find("EventSystem");
            if (eventSystem == null || eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>() == null || eventSystem.GetComponent<StandaloneInputModule>() == null)
            {
                eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
            }
            return canvas;
        }
        
    }
}