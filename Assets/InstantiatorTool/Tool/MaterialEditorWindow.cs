using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class MaterialEditorWindow : EditorWindow {
    private MaterialColorContainer currentMaterialColorSerializer;
    private GameObject gameObject;
    private Material tempMaterial;
    private string tempMaterialName;

    private MaterialColor tempMaterialColor;

    [MenuItem("Window/Material Editor Window")]
	// Use this for initialization
	static void OpenWindow () {
        EditorWindow.GetWindow<MaterialEditorWindow>(true);
    }

    public void Save(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(MaterialColorContainer));

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, currentMaterialColorSerializer);
        }
    }

    public MaterialColorContainer Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(MaterialColorContainer));

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as MaterialColorContainer;
        }
    }

    private List<Material> getMaterialsGameObject(GameObject gameObject) {
        List<Material> materialList = new List<Material>();

        if (gameObject.GetComponent<MeshRenderer>() != null) {
            foreach (Material material in gameObject.GetComponent<MeshRenderer>().sharedMaterials) {
                materialList.Add(material); 
            }
        }

        return materialList;
    }
    
    void Awake()
    {
        currentMaterialColorSerializer = new MaterialColorContainer();    
    }

    // Update is called once per frame
    void OnGUI ()
    {
        tempMaterial = (Material)EditorGUILayout.ObjectField(tempMaterial, typeof(Material), true);
        gameObject = (GameObject)EditorGUILayout.ObjectField(gameObject, typeof(GameObject), true);


        if (GUILayout.Button("Get All Materials From GameObject"))
        {
            if (gameObject != null) {
                foreach (Material material in getMaterialsGameObject(gameObject)) {
                    currentMaterialColorSerializer.materialColorList.Add(
                        new MaterialColor(material));
                }
            }
        }

        if (GUILayout.Button("Add New Material Color Set"))
        {
            currentMaterialColorSerializer.materialColorList.Add(
                new MaterialColor(tempMaterial));
        }

        if (GUILayout.Button("Create A New Material Color Set"))
        {
            tempMaterialColor = new MaterialColor();
            currentMaterialColorSerializer.materialColorList.Add(
                tempMaterialColor);
        }

        if (GUILayout.Button("Save All"))
        {
            Save(Path.Combine(Application.dataPath, "MaterialColorInfo.xml") );
        }

        if (GUILayout.Button("Clear"))
        {
            // Save(defaultPath);
            tempMaterial = null;
        }

        if (GUILayout.Button("Load All"))
        {
            currentMaterialColorSerializer = Load(Path.Combine(Application.dataPath, "MaterialColorInfo.xml") );
        }

        if (currentMaterialColorSerializer.materialColorList != null) {
            foreach (MaterialColor mc in currentMaterialColorSerializer.materialColorList)
            {
                EditorGUILayout.BeginHorizontal();
                mc.materialName = EditorGUILayout.TextField(mc.materialName);
                // EditorGUILayout.TextField(mc.property_1_Name + " " + mc.property_1_Value);
                mc.property_2_Value = EditorGUILayout.ColorField(mc.property_2_Name, mc.property_2_Value);
                mc.property_3_Value = EditorGUILayout.ColorField(mc.property_3_Name, mc.property_3_Value);
                // EditorGUILayout.ColorField(mc.property_4_Name, mc.property_4_Value);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
