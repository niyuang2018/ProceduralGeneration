using UnityEngine;
using System.Collections.Generic;

public class Box : MonoBehaviour
{
    #region private field 
    private float length;
    private float width;
    private float height;

    private MeshRenderer _render;
    private MeshFilter _filter;
    private Mesh _mesh;

    private List<Mesh> meshList = new List<Mesh>();

    private Material selfIlluminMainBuilding;
    private Material boardMaterial;
    private Texture2D emissionMap;


    private Vector3 parentPivot;
    #endregion

    #region public field 
    public float maxLength = 0.8f;
    public float maxWidth = 0.8f;

    public float heightFraction = 0.5f;

    public Vector3[] baseVertices;
    public Vector3[] lastExtrudedFaceVertices;
    // public List<List<Vector3>> additionTextured;

    private List<int> boardTriangles;
    private List<int> combineMeshTriangles;
    #endregion

    #region setter and getter
    public float Length
    {
        set
        {
            this.length = value;
        }
        get
        {
            return this.length;
        }
    }

    public float Width
    {
        set
        {
            this.width = value;
        }
        get
        {
            return this.width;
        }
    }

    public float Height
    {
        set
        {
            this.height = value;
        }
        get
        {
            return this.height;
        }
    }

    public Mesh Mesh
    {
        set
        {
            this._mesh = value;
        }
        get
        {
            return this._mesh;
        }
    }

    public MeshFilter MeshFilter
    {
        set
        {
            this._filter = value;
        }
        get
        {
            return this._filter;
        }
    }
    #endregion


    #region Initialization 
    private void initializeRandomness()
    {
        length = Random.Range(0.5f, maxLength);
        width = Random.Range(0.5f, maxWidth);

        height = Random.Range(10.0f / ProceduralUtil.getDistanceTowardsCenter(gameObject.transform.position),
            10.0f / ProceduralUtil.getDistanceTowardsCenter(gameObject.transform.position) + heightFraction);
    }        

    private void initializeBaseShapeType()
    {   
        // Random BaseShape
        int baseShape = Random.Range(0, 3);

        if (baseShape == 0)
        {
            baseVertices = ProceduralUtil.generateSquareBaseFace(transform, Width, Length).ToArray();
        }
        else if (baseShape == 1)
        {
            baseVertices = ProceduralUtil.generateBaseShape_1(transform, Width, Length, 0.1f).ToArray();
        }
        else if (baseShape == 2)
        {
            baseVertices = ProceduralUtil.generateBaseShape_2(transform, Width, Length, 0.1f).ToArray();
        }

        // load baseshape xml

        // assign BaseVertuces
    }

    private void InitializeMaterials()
    {
        // load material xml

        // assign material

        // Main building material

        selfIlluminMainBuilding = Resources.Load("Materials/apt_4", typeof(Material)) as Material;
        selfIlluminMainBuilding.SetColor("_EmissionColor", Color.white);

        int randomEmissionMap = Random.Range(4, 6);
        Texture emission = Resources.Load("Texture/emission_" + randomEmissionMap, typeof(Texture)) as Texture;
        selfIlluminMainBuilding.SetTexture("_EmissionMap", emission);

        boardMaterial = Resources.Load("Materials/board", typeof(Material)) as Material;
    }
    #endregion

    #region GenerateAttachment
    private void generateBoard() {
        // 1 / 6 chance to generate a board
        //int generateABoard = Random.Range(1, 4);
        //if (generateABoard == 1)
        //{
            meshList.Add(ProceduralUtil.generateBoardOnRoof(this));
        //}

        //generateABoard = Random.Range(1, 4);
        //if (generateABoard == 1)
        //{
            meshList.Add(ProceduralUtil.generateBoardOnSide(this));
        //}
    }
    #endregion


    void Awake() {
        // initializePivot();
        initializeRandomness();
        initializeBaseShapeType();
        // InitializeMaterials();

        // additionTextured = new List<List<Vector3>>();
    }
    
    void Start()
    {
        _render = gameObject.AddComponent<MeshRenderer>();
        _filter = gameObject.AddComponent<MeshFilter>();
        _mesh = _filter.mesh;
        
        // generate Random level of towers
        meshList.Add(ProceduralUtil.extrudeAFace(this, baseVertices, Height, false));

        // need to perform strip in the first place or top face vertice will be overwritten
        int strip = Random.Range(0, 3);
        float stripSize = Random.Range(0.0f, 0.2f);
        float stripHeight = Random.Range(0.0f, Height);
        if (strip == 0)
        {
            for (int i = 0; i < baseVertices.Length; i++)
            {
                Vector3[] squareBase = new Vector3[4];
                squareBase = ProceduralUtil.generateSquareBaseFromVertice(baseVertices[i], stripSize);

                meshList.Add(ProceduralUtil.extrudeAFace(this, squareBase, stripHeight, true));
            }
            // only generate strip lines if the building is a cube
        }

        int preserveResizeRatio = Random.Range(0, 4);
        float randomHeightLevel = Random.Range(0.1f, 0.3f);

        if (preserveResizeRatio == 0)
        {
            int numberOfLevel = Random.Range(0, 5);
            float randomRatio = Random.Range(0.0f, 1.0f);
            for (int i = 0; i < numberOfLevel; i++)
            {
                // set level resizing offset
                meshList.Add(ProceduralUtil.extrudeAFaceWithLevels(this, lastExtrudedFaceVertices, Height, Length, randomHeightLevel, randomRatio));

                meshList.Add(ProceduralUtil.extrudeAFaceWithLevels(this, lastExtrudedFaceVertices, Height, Length, randomHeightLevel, 1.0f / randomRatio));
            }
            
            for (int i = 0; i < lastExtrudedFaceVertices.Length; i++)
            {
                lastExtrudedFaceVertices[i] = lastExtrudedFaceVertices[i] + new Vector3(0.0f, randomHeightLevel, 0.0f);
            }
        }
        else {
            int numberOfLevel = Random.Range(0, 3);
            float randomRatio = Random.Range(0.0f, 1.0f); for (int i = 0; i < numberOfLevel; i++)
            {
                meshList.Add(ProceduralUtil.extrudeAFaceWithLevels(this, lastExtrudedFaceVertices, Width, Length, randomHeightLevel, randomRatio));
            }
            
            for (int i = 0; i < lastExtrudedFaceVertices.Length; i++)
            {
                lastExtrudedFaceVertices[i] = lastExtrudedFaceVertices[i] + new Vector3(0.0f, randomHeightLevel, 0.0f);
            }

        }

        // Generate Attachment
        generateBoard();

        // Combine Mesh
        Mesh combinedMesh = ProceduralUtil.combineMeshes(gameObject.transform, meshList);

        combineMeshTriangles = new List<int>(combinedMesh.triangles);

        // need to set filter mesh before call findTrianglesWIthinVertices
        this._filter.mesh = combinedMesh;

        // AssignMaterial
        assignMaterial(combinedMesh);
    }

    private void assignMaterial(Mesh combinedMesh) {
        /*

        // if the tower contains board 
        if (additionTextured.Count != 0)
        {
            combinedMesh.subMeshCount = additionTextured.Count + 1;
            List<int> towerTriangles = new List<int>();

            for (int i = 0; i < additionTextured.Count; i++)
            {
                boardTriangles = ProceduralUtil.findTrianglesWithinVertices(this, additionTextured[i].ToArray());

                combinedMesh.SetTriangles(boardTriangles.ToArray(), i);
                towerTriangles = ProceduralUtil.removeTrianglesFromList(new List<int>(combineMeshTriangles), boardTriangles);
            }

            combinedMesh.SetTriangles(towerTriangles.ToArray(), additionTextured.Count);

            Material[] materials = new Material[additionTextured.Count + 1];
            for (int i = 0; i < additionTextured.Count; i++)
            {
                materials[i] = boardMaterial;

            }
            materials[additionTextured.Count] = selfIlluminMainBuilding;
            _render.materials = materials;
        }
        else
        {
            // set default material as main building material
            _render.material = selfIlluminMainBuilding;
        }*/


        // set default material as main building material
        _render.material = selfIlluminMainBuilding;
    }
}
