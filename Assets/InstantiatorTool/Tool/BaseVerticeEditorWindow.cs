using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class BaseVerticeEditorWindow : EditorWindow{
    private BaseVerticeContainer currentBaseVerticeSerializer;
    
    [MenuItem("Window/Base Vertice Editor Window")]
    // Use this for initialization
    static void OpenWindow()
    {
        EditorWindow.GetWindow<BaseVerticeEditorWindow>(true);
    }

    public void Save(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BaseVerticeContainer));

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, currentBaseVerticeSerializer);
        }
    }

    public BaseVerticeContainer Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BaseVerticeContainer));

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as BaseVerticeContainer;
        }
    }


    void Awake()
    {
        currentBaseVerticeSerializer = new BaseVerticeContainer();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Add Base Vertice Set"))
        {
            currentBaseVerticeSerializer.BaseVerticeList.Add(
                new BaseVertice());
        }


        if (GUILayout.Button("Sort Clockwise"))
        {
            currentBaseVerticeSerializer.sortClockwise();
        }
       
        if (GUILayout.Button("Save All"))
        {
            Save(Path.Combine(Application.dataPath, "BaseVerticeSet.xml"));
        }

        if (GUILayout.Button("Clear"))
        {
            // Save(defaultPath);
            // tempMaterial = null;
        }

        if (GUILayout.Button("Load All"))
        {
            currentBaseVerticeSerializer = Load(Path.Combine(Application.dataPath, "BaseVerticeSet.xml"));
        }

        if (currentBaseVerticeSerializer.BaseVerticeList != null)
        {
            foreach (BaseVertice bv in currentBaseVerticeSerializer.BaseVerticeList)
            {
                EditorGUILayout.BeginHorizontal();
                bv.RootObjectName = EditorGUILayout.TextField(bv.RootObjectName );
                bv.Position = EditorGUILayout.Vector2Field("Position" + bv.VerticeIndex, bv.Position);
                // mc.property_3_Value = EditorGUILayout.ColorField(mc.property_3_Name, mc.property_3_Value);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
