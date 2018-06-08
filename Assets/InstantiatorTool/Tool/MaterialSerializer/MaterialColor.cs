using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class MaterialColor {
    [XmlAttribute("Material Name")]
    public string materialName;

    // Diffuse Color
    public string property_1_Name = "_NormalIntensity";
    public float property_1_Value;

    // Rim Color
    public string property_2_Name = "_Diffuse";
    public Color property_2_Value ;

    // Rim1 Color
    public string property_3_Name = "_RimColor";
    public Color property_3_Value;

    public string property_4_Name = "_ShaderName";
    public string property_4_Value;

    public MaterialColor() {
        property_1_Value = 0.0f;
        property_2_Value = Color.black;
        property_3_Value = Color.black;
        property_4_Value = "";
    }
    /*
    public MaterialColor(float normalIntensity, Color diffuseColor, Color rimColor, Color rim1Color) {
        property_1_Value = normalIntensity;
        property_2_Value = diffuseColor;
        property_3_Value = rimColor;
        property_4_Value = rim1Color;
    } */

    public MaterialColor(Material material)
    {
        materialName = material.name;
        
        property_1_Value = material.GetFloat("_NormalIntensity");
        property_2_Value = material.GetColor("_Diffuse");
        property_3_Value = material.GetColor("_RimColor");
        property_4_Value = material.shader.name;
    }
}
