using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("BaseVerticeCollection")]
public class BaseVerticeContainer {
    [XmlArray("BaseVertice")]
    [XmlArrayItem("BaseVertice")]

    private List<BaseVertice> baseVerticesList;

    public  List<BaseVertice> BaseVerticeList {
        get {

            if (baseVerticesList == null) {
                baseVerticesList = new List<BaseVertice>();
            }
            return baseVerticesList;
        }

        set {
            this.baseVerticesList = value;
        }
    }

    public List<Vector2> sortClockwise() {
        // 

        return null;
    }
}
