using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class BaseVertice
{
    [XmlAttribute("Root Object Name")]
    private string root_Object_Name = "_RootObjectName";
    
    private int vertice_Index = 0;

    private Vector3 position;

    public Vector3 Position {
        get {
            return position;
        }

        set {
            position = value;
        }
    }
    public string RootObjectName {
        get {
            return root_Object_Name;
        }

        set {
            root_Object_Name = value;
        }
    }

    public int VerticeIndex {
        get {
            return vertice_Index;
        }

        set {
            vertice_Index = value;
        }
    }


    public BaseVertice()
    {
        root_Object_Name = "";
        position = new Vector3();
        vertice_Index = 0;
    }

    public BaseVertice(string _root_Object_Name, Vector3 _position, int _verticeIndex)
    {
        root_Object_Name = _root_Object_Name;
        position = _position;
        vertice_Index = _verticeIndex;
    }
}
