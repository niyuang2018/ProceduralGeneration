using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class PropInstantiater : MonoBehaviour {
    public Transform ParentTransform;

    public bool loadAndReplacePrefab;

    private List<GameObject> buildingGroup;
    
    private MaterialColorContainer currentMaterialColorSerializer;
    // private PrefabInterchangable currentPrefabInterchangable;
    private BaseVerticeContainer currentBaseVerticeContainer;
      
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        initialize();
    }
    
    public void Start()
    {
        StartCoroutine(assignMaterialColorCoroutine());
    }

    IEnumerator assignMaterialColorCoroutine() {
        disableEnableMeshRenderAll(buildingGroup, false);

        yield return new WaitForSeconds(0.1f);
        if (loadAndReplacePrefab) {
            // replacePrefabInterchangable(buildingGroup);
        }

        yield return new WaitForSeconds(0.1f);
        buildingGroup = resetGroup(buildingGroup);

        yield return new WaitForSeconds(0.1f);
        assignMaterialColor(buildingGroup);

        yield return new WaitForSeconds(0.1f);

        disableEnableMeshRenderAll(buildingGroup, true);
        yield return null;
    }

    private List<GameObject> resetGroup(List<GameObject> buildingGroup)
    {
        buildingGroup.Clear();
        for (int i = 0; i < ParentTransform.childCount; i++) {
            buildingGroup.Add(ParentTransform.GetChild(i).gameObject);
        }
        return buildingGroup;
    }

    private void initialize()
    {
        buildingGroup = new List<GameObject>();

        for (int i = 0; i < ParentTransform.childCount; i++) {
            buildingGroup.Add(ParentTransform.GetChild(i).gameObject);
        }
        
        // Load Color Data
        currentMaterialColorSerializer = loadColorData();

        // Load Prefab Data
        // currentPrefabInterchangable = loadPrefabInterchangableData();

        // Load Base Vertice Data
        currentBaseVerticeContainer = loadBaseVerticeData();

    }

    private MaterialColorContainer loadColorData() {
        XmlSerializer serializer = new XmlSerializer(typeof(MaterialColorContainer));

        using (FileStream stream = new FileStream(Path.Combine(Application.dataPath, "MaterialColorInfo.xml"), FileMode.Open))
        {
            return serializer.Deserialize(stream) as MaterialColorContainer;
        }
    }

    private BaseVerticeContainer loadBaseVerticeData() {
        XmlSerializer serializer = new XmlSerializer(typeof(BaseVerticeContainer));


        using (FileStream stream = new FileStream(Path.Combine(Application.dataPath, "BaseVertice.xml"), FileMode.Open))
        {
            return serializer.Deserialize(stream) as BaseVerticeContainer;
        }
    }

    /*
    private PrefabInterchangable loadPrefabInterchangableData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PrefabInterchangable));

        using (FileStream stream = new FileStream(Path.Combine(Application.dataPath, "PrefabInterchangable.xml"), FileMode.Open))
        {
            return serializer.Deserialize(stream) as PrefabInterchangable;
        }
    }

    private void replacePrefabInterchangable(List<GameObject> gameObjectList)
    {
        foreach (GameObject go in gameObjectList)
        {
            setTransformInfo(go.transform, replacePrefabInterchangablePerObject(go).transform);

            Destroy(go);
        }
    }
    
    private GameObject replacePrefabInterchangablePerObject(GameObject gameObject)
    {
        PrefabInfo prefabInfo = currentPrefabInterchangable.getRandomPrefabInfo(InstantiatorUtil.ParsePrefabInfoFromGameObject(gameObject));
        
        return Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(prefabInfo.assetPath, typeof(GameObject)), gameObject.transform.parent);
    }

    private void setTransformInfo(Transform prevTransform, Transform newTransform) {
        newTransform.rotation = prevTransform.rotation;
        newTransform.position = prevTransform.position;
        newTransform.localScale = prevTransform.localScale;
    }*/

    private void assignMaterialColor(List<GameObject> gameObjectList)
    {
        List<MaterialCombo> materialCombo = currentMaterialColorSerializer.getSplittedMaterialComboListByPrefixNumber(currentMaterialColorSerializer.materialColorList);
        foreach (GameObject go in gameObjectList) {
            // Instance new material
            currentMaterialColorSerializer.applyRandomMaterialCombo(go.GetComponent<MeshRenderer>().materials);
        }
    } 

    private void assignBaseVerticeAndInstantiate() {

    }

    private void disableEnableMeshRenderAll(List<GameObject> gameObjectList, bool flag) {
        foreach (GameObject go in gameObjectList) {
            if (go.GetComponent<MeshRenderer>() != null) {
                go.GetComponent<MeshRenderer>().enabled = flag;
            }
        }
    }
}
