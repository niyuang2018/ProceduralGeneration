using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

public class PrefabInterchangableEditorWindow : EditorWindow { 
    private PrefabInterchangable prefabInterchangableSerializer;
    private GameObject tempObject;
    private string setName;
    private bool initialized;

    [MenuItem("Window/Prefab Interchangable Editor Window")]
    static void OpenWindow() {
        EditorWindow.GetWindow<PrefabInterchangableEditorWindow>(true);
    }

    void Awake()
    {
        prefabInterchangableSerializer = new PrefabInterchangable();
        initialized = true;
    }

    public PrefabInterchangable Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PrefabInterchangable));

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as PrefabInterchangable;
        }
    }

    public void Save(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PrefabInterchangable));

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, prefabInterchangableSerializer);
        }
    }

    private void ClearObjectField() {
        tempObject = null;
    }

    private Object collectGameObjectPrefabDependency(GameObject gameObject) {
        return PrefabUtility.GetPrefabObject(gameObject);
    }

    void OnGUI()
    {
        tempObject = (GameObject)EditorGUILayout.ObjectField(tempObject, typeof(GameObject), true);

        if (GUILayout.Button("Create a new interchangable in current List"))
        {   
            prefabInterchangableSerializer.prefabInterchangableList.Add(InstantiatorUtil.ParsePrefabInfoFromGameObject(collectGameObjectPrefabDependency(tempObject)));

            ClearObjectField();
        }

        if (GUILayout.Button("Load"))
        {
            prefabInterchangableSerializer = Load(Path.Combine(Application.dataPath, "PrefabInterChangable.xml"));
        }

        if (GUILayout.Button("Save"))
        {

            Save(Path.Combine(Application.dataPath, "PrefabInterChangable.xml"));
        }

        if (initialized)
        {
            foreach (PrefabInfo pi in prefabInterchangableSerializer.prefabInterchangableList)
            {
                EditorGUILayout.BeginHorizontal();
                pi.prefabName = EditorGUILayout.TextField(pi.prefabName);
                pi.assetPath = EditorGUILayout.TextField(pi.assetPath);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
