using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PrefabInfo {
    [XmlAttribute("Prefab Name")]
    public string prefabName;
    public string assetPath;

    public PrefabInfo() {
        prefabName = "";
        assetPath = "";
    }
    
    public PrefabInfo(string _prefabName, string _assetPath) {
        prefabName = _prefabName;
        assetPath = _assetPath;
    }
}
