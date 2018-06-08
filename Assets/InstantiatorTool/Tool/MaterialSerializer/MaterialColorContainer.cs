using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System;

[XmlRoot("MaterialColorCollection")]
public class MaterialColorContainer
{
    [XmlArray("MaterialColor")]
    [XmlArrayItem("MaterialColor")]
    public List<MaterialColor> materialColorList = new List<MaterialColor>();

    public void applyMaterialColor(Material material, MaterialColor materialColor)
    {
        if (material != null)
        {
            material.SetColor(materialColor.property_2_Name, materialColor.property_2_Value);
            material.SetColor(materialColor.property_3_Name, materialColor.property_3_Value);
        }
    }

    public void applyMaterialArray(Material[] materialArray, MaterialColor[] materialColorArray)
    {
        for (int i = 0; i < materialArray.Length; i++)
        {
            // must be in same order
            // materialArray[i].SetColor(materialColorArray[i].property_2_Name, materialColorArray[i].property_2_Value);
            // materialArray[i].SetColor(materialColorArray[i].property_3_Name, materialColorArray[i].property_3_Value);

            // or...
            for (int j = 0; j < materialColorArray.Length; j++)
            {
                if (Regex.Match(materialColorArray[j].materialName, InstantiatorUtil.trimMaterialInstanceString(materialArray[i].name)).Success)
                {
                    // Debug.Log("match success" + materialColorArray[j].materialName + " " + trimString(materialArray[i].name));
                    materialArray[i].SetColor(materialColorArray[j].property_2_Name, materialColorArray[j].property_2_Value);
                    materialArray[i].SetColor(materialColorArray[j].property_3_Name, materialColorArray[j].property_3_Value);
                    // Debug.Log("apply color to: " + materialArray[i].name);
                }
            }
        }
    }

    public void applyMaterialArrayRandomShader(Material[] materialArray, MaterialColor[] materialColorArray)
    {
        for (int i = 0; i < materialArray.Length; i++)
        {
            // must be in same order
            // materialArray[i].SetColor(materialColorArray[i].property_2_Name, materialColorArray[i].property_2_Value);
            // materialArray[i].SetColor(materialColorArray[i].property_3_Name, materialColorArray[i].property_3_Value);

            // or...
            for (int j = 0; j < materialColorArray.Length; j++)
            {
                if (Regex.Match(materialColorArray[j].materialName, InstantiatorUtil.trimMaterialInstanceString(materialArray[i].name)).Success)
                {
                    // Debug.Log("match success" + materialColorArray[j].materialName + " " + trimString(materialArray[i].name));
                    materialArray[i].SetColor(materialColorArray[j].property_2_Name, materialColorArray[j].property_2_Value);
                    materialArray[i].SetColor(materialColorArray[j].property_3_Name, materialColorArray[j].property_3_Value);
                    // Debug.Log("apply color to: " + materialArray[i].name);
                }
            }
        }
    }
    
    public void applyRandomMaterialCombo(Material[] materialArray)
    {
        List<MaterialCombo> materialCombo = getSplittedMaterialComboListByPrefixNumber(materialColorList);
        
        int randomIndex = UnityEngine.Random.Range(0, materialCombo.Count);

        MaterialCombo materialComboToAdd = materialCombo[randomIndex];

        applyMaterialArrayRandomShader(materialArray, materialComboToAdd.toMaterialColorArray());
    }

    public List<MaterialCombo> getSplittedMaterialComboListByPrefixNumber(List<MaterialColor> _materialColorList)
    {
        List<MaterialCombo> materialComboList = new List<MaterialCombo>();
        string pattern = "[a-zA-Z]*_(\\d)";

        for (int i = 0; i < _materialColorList.Count; i++)
        {
            Match matchResult = Regex.Match(_materialColorList[i].materialName, pattern);
            int groupNumber;

            if (matchResult.Success)
            {
                groupNumber = Int32.Parse(matchResult.Groups[1].ToString());

                MaterialCombo mc = checkMaterialComboExists(materialComboList, groupNumber);

                if (mc != null)
                {
                    mc.addCombo(_materialColorList[i]);
                }
                else
                {
                    mc = new MaterialCombo(groupNumber);
                    mc.addCombo(_materialColorList[i]);
                    materialComboList.Add(mc);
                }
            }
        }

        return materialComboList;
    } 

    private MaterialCombo checkMaterialComboExists(List<MaterialCombo> materialComboList, int groupNumber)
    {
        foreach (MaterialCombo mc in materialComboList)
        {
            if (mc.Index == groupNumber)
            {
                return mc;
            }
        }
        return null;
    }
    // Not being used if colors are in combo
    /*
    public MaterialColor getRandomColorByNamePrefix(string prefix)
    {
        List<MaterialColor> splittedMaterialColorList = new List<MaterialColor>();
        bool foundMatch = false;

        string formattedString = "";

        Match matchResult = Regex.Match(prefix, "(.*)(\\(Instance\\))");

        if (matchResult.Success)
        {
            formattedString = matchResult.Groups[1].ToString();
            // remove the whitespace at the end 
            formattedString = formattedString.Substring(0, formattedString.Length - 1);
        }
        else
        {
            formattedString = prefix;
        }

        foreach (MaterialColor materialColor in materialColorList)
        {
            if (Regex.Match(materialColor.materialName, formattedString).Success)
            {
                splittedMaterialColorList.Add(materialColor);
                foundMatch = true;
            }
        }

        if (foundMatch)
        {
            int randomIndex = UnityEngine.Random.Range(0, splittedMaterialColorList.Count);
            UnityEngine.Random.InitState(randomIndex);

            return splittedMaterialColorList[randomIndex];
        }
        else
        {
            return new MaterialColor();
        } 
    }*/
}
