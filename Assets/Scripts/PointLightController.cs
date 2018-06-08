using UnityEngine;
using System.Collections;

public class PointLightController : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject _gridPrefab;
    [SerializeField]
    private float gridWidth;
    [SerializeField]
    private float gridLength;
    
    void Start() {
        Instantiate(_gridPrefab, Vector3.zero, transform.rotation);
    }
}
