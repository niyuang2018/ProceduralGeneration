using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCombo {
    private int index;
    private List<MaterialColor> materialColorList;

    public int Index {
        get {
            return index;
        }
        set {
            index = value;
        }
    }

    public List<MaterialColor> MaterialColorList {
        get {
            return materialColorList;
        } 
        set {
            materialColorList = value;
        }
    }

    public MaterialCombo(int index, List<MaterialColor> materialColorList) {
        Index = index;
        MaterialColorList = materialColorList;
    }

    public MaterialCombo(int index)
    {
        Index = index;
        MaterialColorList = new List<MaterialColor>();
    }

    public void addCombo(MaterialColor materialColor) {
        materialColorList.Add(materialColor);
    }

    public MaterialColor[] toMaterialColorArray() {
        return materialColorList.ToArray();
    }
}
