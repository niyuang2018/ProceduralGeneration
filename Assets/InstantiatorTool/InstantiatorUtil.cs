using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class InstantiatorUtil {
    public static string trimMaterialInstanceString(string instanceMaterialName) {
        Match matchResult = Regex.Match(instanceMaterialName, "(.*)(\\(Instance\\))");
        string trimmed = "";

        if (matchResult.Success)
        {
            trimmed = matchResult.Groups[1].ToString();
            // remove the whitespace at the end 
            trimmed = trimmed.Substring(0, trimmed.Length - 1);

            return trimmed;
        }
        else
        {
            return instanceMaterialName;
        }
    }

    public static string trimPrefabNameString(string prefabNameString)
    {
        string pattern = "(.*\\/)(.*)";

        Match matchResult = Regex.Match(prefabNameString, pattern);

        if (matchResult.Success)
        {
            return matchResult.Groups[2].ToString();
        }

        return "";
    }

    public static PrefabInfo ParsePrefabInfoFromGameObject(Object Object) {
        // AssetDataBase does work here
        return new PrefabInfo(trimPrefabNameString(AssetDatabase.GetAssetPath(Object)), AssetDatabase.GetAssetPath(Object));
    }
}
