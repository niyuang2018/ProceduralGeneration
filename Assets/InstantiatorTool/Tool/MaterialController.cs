using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour {
    Material BaseMaterial;
    public List<Color> presetColors;
    public Transform groupTransform;

	// Use this for initialization
	void Start () {
        setAllObjectTransform();
    }

    private void setAllObjectTransform() {
        if (groupTransform != null && groupTransform.transform.childCount > 0) {
            for (int i = 0; i < groupTransform.transform.childCount; i++) {
                setMaterialRandomColor(groupTransform.transform.GetChild(i).gameObject);
            }
        }
    }

    private void setMaterialRandomColor(GameObject go) {
        if (go.GetComponent<MeshRenderer>() != null) {
            for(int i = 0; i < go.GetComponent<MeshRenderer>().materials.Length; i++) {
                Material m = go.GetComponent<MeshRenderer>().materials[i];
                if (m.HasProperty("_Diffuse"))
                {
                    Material instancedMaterial = new Material(m);

                    Color randomColor = presetColors[Random.Range(0, presetColors.Count)];
                    
                    go.GetComponent<MeshRenderer>().materials[i] = instancedMaterial;
                    
                    go.GetComponent<MeshRenderer>().materials[i].SetColor("_Diffuse", randomColor);
                }
            }
        }
    }
}
