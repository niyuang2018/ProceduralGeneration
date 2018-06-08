using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraPosition : EditorWindow {
    [MenuItem("Window/Camera Position")]
	// Use this for initialization
	static void OpenWindow () {
        EditorWindow.GetWindow<CameraPosition>(true);
	}

    private Transform prevTransform;

	// Update is called once per frame
	void OnGUI () {
        if (GUILayout.Button("Save Position")) {
            // prevTransform = Camera.main.transform;
            prevTransform = SceneView.lastActiveSceneView.camera.transform;
        }


        if (GUILayout.Button("Set Position"))
        {
            setCameraPosition(prevTransform);
        }
    }

    private void setCameraPosition(Transform transform) {
        Camera.main.transform.localPosition = transform.position;
        Camera.main.transform.localRotation = transform.rotation;
    }
}
