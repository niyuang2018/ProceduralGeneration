using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplacePrefab : EditorWindow
{
    private GameObject prefabObject;
    private GameObject sceneObject;

    private Transform groupTransform;


    [MenuItem("Window/Prefab Replacer")]
    static void OpenWindow()
    {
        GetWindow<ReplacePrefab>(true);
    }

    private void OnGUI()
    {
        sceneObject = (GameObject)EditorGUILayout.ObjectField(sceneObject, typeof(GameObject), true);
        prefabObject = (GameObject)EditorGUILayout.ObjectField(prefabObject, typeof(GameObject), true);
        groupTransform = (Transform)EditorGUILayout.ObjectField(groupTransform, typeof(Transform), true);

        if (GUILayout.Button("Replace"))
        {
            setTransformInfo(sceneObject, prefabObject);
        }
    }

    private GameObject instantiateNewSceneObject(Transform groupTransform)
    {
        GameObject instanced = (GameObject)Instantiate(prefabObject, groupTransform);
        return instanced;
    }

    private void setTransformInfo(GameObject sceneObject, GameObject prefabObject)
    {
        if (groupTransform != null)
        {
            GameObject instanced = instantiateNewSceneObject(groupTransform);

            instanced.transform.localScale = sceneObject.transform.localScale;
            instanced.transform.rotation = sceneObject.transform.rotation;
            instanced.transform.position = sceneObject.transform.position;
        }
    }
}

