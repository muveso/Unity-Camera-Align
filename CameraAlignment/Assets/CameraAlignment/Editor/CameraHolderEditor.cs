using UnityEditor;
using UnityEngine;

namespace CameraAlignment.Editor
{
    [CustomEditor(typeof(CameraHolder))]
    public class CameraHolderEditor : UnityEditor.Editor
    {
        
        private static Camera _camera;
        private static CameraHolder _cameraHolder;
        private static bool _subscribedSceneGUI;
        
        private const string EnableText = "Enable Camera Alignment";
        private const string DisableText = "Disable Camera Alignment";

        private void OnEnable()
        {
            if (_subscribedSceneGUI) return;
            
            _subscribedSceneGUI = true;
            SceneView.duringSceneGui += CameraUpdate;
        }
        
        public override void OnInspectorGUI()
        {
            _cameraHolder = (CameraHolder) target;
        }
        
        private static void CameraUpdate(SceneView sceneView)
        {
            if (!_cameraHolder) return;
            DrawButton();
            UpdateTransform(sceneView);
        }

        private static void UpdateTransform(SceneView sceneView)
        {
            if (!_cameraHolder.EnableAlignment) return;
            var gameTransform = _camera.transform;
            var sceneTransform = sceneView.camera.transform;

            gameTransform.position = sceneTransform.position;
            gameTransform.rotation = sceneTransform.rotation;
        }

        private static void DrawButton()
        {
            Handles.BeginGUI();
            var oldColor = GUI.color;

            if (!_cameraHolder.EnableAlignment)
            {
                GUI.color = Color.green;
                if (GUILayout.Button(EnableText, GUILayout.Width(200), GUILayout.Height(40)))
                {
                    DisableAllScripts();

                    _camera = _cameraHolder.gameObject.GetComponent<Camera>();
                    _cameraHolder.EnableAlignment = true;
                }
            }
            else
            {
                GUI.color = Color.red;
                if (GUILayout.Button(DisableText, GUILayout.Width(200), GUILayout.Height(40)))
                {
                    _camera = null;
                    _cameraHolder.EnableAlignment = false;
                }
            }

            GUI.color = oldColor;
            EditorUtility.SetDirty(_cameraHolder.gameObject);
            Handles.EndGUI();
        }

        private static void DisableAllScripts()
        {
            var attachedCameras = FindObjectsOfType<CameraHolder>();
            foreach (var cam in attachedCameras)
                cam.EnableAlignment = false;
        }

    }
}