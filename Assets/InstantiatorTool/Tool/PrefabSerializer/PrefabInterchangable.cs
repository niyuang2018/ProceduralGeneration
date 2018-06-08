using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("PrefabInterchangable")]
public class PrefabInterchangable {
    [XmlArray("PrefabInfo")]
    [XmlArrayItem("PrefabInfo")]
    public List<PrefabInfo> prefabInterchangableList = new List<PrefabInfo>();

    public PrefabInterchangable() {
        prefabInterchangableList = new List<PrefabInfo>();
    }

    public PrefabInfo getRandomPrefabInfo() {
        return prefabInterchangableList[UnityEngine.Random.Range(0, prefabInterchangableList.Count)];
    }

    // get the alternative other than
    public PrefabInfo getRandomPrefabInfo(PrefabInfo prefabInfo) {
        prefabInterchangableList.Remove(prefabInfo);
        return prefabInterchangableList[UnityEngine.Random.Range(0, prefabInterchangableList.Count)];
    }

    public PrefabInfo getRandomPrefabInfo(string prefabName)
    {
        for (int i = 0; i < prefabInterchangableList.Count; i++) {
            if (prefabInterchangableList[i].prefabName.Equals(prefabName)) {
                prefabInterchangableList.Remove(prefabInterchangableList[i]);
            }
        }

        return prefabInterchangableList[UnityEngine.Random.Range(0, prefabInterchangableList.Count)];
    }
}
