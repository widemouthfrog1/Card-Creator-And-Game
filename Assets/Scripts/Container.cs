using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Container : Item
{
    public float thickness = 0.1f;
    public float boarderThickness = 0.05f;
    public Vector2 size = new Vector2(0,0);
    public List<Item> items = new List<Item>();
    public Material material;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Generate Mesh")]
    public virtual void GenerateMesh()
    {
        if (TryGetComponent(out MeshFilter m)){}
        else
        {
            gameObject.AddComponent<MeshFilter>();
            gameObject.AddComponent<MeshRenderer>();
            GetComponent<MeshRenderer>().material = material;
        }
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        //bottom
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, -thickness, -size.y / 2 - boarderThickness)); //bottom left
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, -thickness, size.y / 2 + boarderThickness)); //top left
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, -thickness, size.y / 2 + boarderThickness)); //top right
        vertices.Add(new Vector3(-size.x/2 - boarderThickness, -thickness, -size.y/2 - boarderThickness)); //bottom right

        //left outside
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, -thickness, size.y / 2 + boarderThickness)); //bottom left
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, thickness, size.y / 2 + boarderThickness)); //top left
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, thickness, -size.y / 2 - boarderThickness)); //top right
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, -thickness, -size.y / 2 - boarderThickness)); //bottom right

        //front outside
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, -thickness, size.y / 2 + boarderThickness)); //bottom left
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, thickness, size.y / 2 + boarderThickness)); //top left
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, thickness, size.y / 2 + boarderThickness)); //top right
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, -thickness, size.y / 2 + boarderThickness)); //bottom right

        //right outside
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, -thickness, -size.y / 2 - boarderThickness)); //bottom left
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, thickness, -size.y / 2 - boarderThickness)); //top left
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, thickness, size.y / 2 + boarderThickness)); //top right
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, -thickness, size.y / 2 + boarderThickness)); //top right

        //back outside
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, -thickness, -size.y / 2 - boarderThickness)); //bottom left
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, thickness, -size.y / 2 - boarderThickness)); //top left
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, thickness, -size.y / 2 - boarderThickness)); //top right
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, -thickness, -size.y / 2 - boarderThickness)); //bottom right

        //left top
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, thickness, size.y / 2 + boarderThickness)); //bottom left
        vertices.Add(new Vector3(-size.x / 2, thickness, size.y / 2)); //top left
        vertices.Add(new Vector3(-size.x / 2, thickness, -size.y / 2)); //top right
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, thickness, -size.y / 2 - boarderThickness)); //bottom left

        //front top
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, thickness, size.y / 2 + boarderThickness)); //bottom left
        vertices.Add(new Vector3(size.x / 2, thickness, size.y / 2)); //top left
        vertices.Add(new Vector3(-size.x / 2, thickness, size.y / 2)); //top right
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, thickness, size.y / 2 + boarderThickness)); //bottom right

        //right top
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, thickness, -size.y / 2 - boarderThickness)); //bottom left
        vertices.Add(new Vector3(size.x / 2, thickness, -size.y / 2)); //top left
        vertices.Add(new Vector3(size.x / 2, thickness, size.y / 2)); //top right
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, thickness, size.y / 2 + boarderThickness)); //bottom right

        //back top
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, thickness, -size.y / 2 - boarderThickness)); //bottom left
        vertices.Add(new Vector3(-size.x / 2, thickness, -size.y / 2)); //top left
        vertices.Add(new Vector3(size.x / 2, thickness, -size.y / 2)); //top right
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, thickness, -size.y / 2 - boarderThickness)); //bottom right

        //left inside
        vertices.Add(new Vector3(-size.x / 2, 0, -size.y / 2)); //bottom left
        vertices.Add(new Vector3(-size.x / 2, thickness, -size.y / 2)); //top left
        vertices.Add(new Vector3(-size.x / 2, thickness, size.y / 2)); //top right
        vertices.Add(new Vector3(-size.x / 2, 0, size.y / 2)); //bottom right

        //front inside
        vertices.Add(new Vector3(-size.x / 2, 0, size.y / 2)); //bottom left
        vertices.Add(new Vector3(-size.x / 2, thickness, size.y / 2)); //top left
        vertices.Add(new Vector3(size.x / 2, thickness, size.y / 2)); //top right
        vertices.Add(new Vector3(size.x / 2, 0, size.y / 2)); //bottom right

        //right inside
        vertices.Add(new Vector3(size.x / 2, 0, size.y / 2)); //bottom left
        vertices.Add(new Vector3(size.x / 2, thickness, size.y / 2)); //top left
        vertices.Add(new Vector3(size.x / 2, thickness, -size.y / 2)); //top right
        vertices.Add(new Vector3(size.x / 2, 0, -size.y / 2)); //bottom right

        //back inside
        vertices.Add(new Vector3(size.x / 2, 0, -size.y / 2)); //bottom left
        vertices.Add(new Vector3(size.x / 2, thickness, -size.y / 2)); //top left
        vertices.Add(new Vector3(-size.x / 2, thickness, -size.y / 2)); //top right
        vertices.Add(new Vector3(-size.x / 2, 0, -size.y / 2)); //bottom right
        /*
        //outside top
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, thickness, -size.y / 2 - boarderThickness)); //bottom left
        vertices.Add(new Vector3(-size.x / 2 - boarderThickness, thickness, size.y / 2 + boarderThickness)); //top left
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, thickness, size.y / 2 + boarderThickness)); //top right
        vertices.Add(new Vector3(size.x / 2 + boarderThickness, thickness, -size.y / 2 - boarderThickness)); //bottom right
        //inside top
        vertices.Add(new Vector3(-size.x / 2, thickness, -size.y / 2)); //bottom left
        vertices.Add(new Vector3(-size.x / 2, thickness, size.y / 2)); //top left
        vertices.Add(new Vector3(size.x / 2, thickness, size.y / 2)); //top right
        vertices.Add(new Vector3(size.x / 2, thickness, -size.y / 2)); //bottom right
        */
        //inside bottom
        vertices.Add(new Vector3(-size.x / 2, 0, -size.y / 2)); //bottom left
        vertices.Add(new Vector3(-size.x / 2, 0, size.y / 2)); //top left
        vertices.Add(new Vector3(size.x / 2, 0, size.y / 2)); //top right
        vertices.Add(new Vector3(size.x / 2, 0, -size.y / 2)); //bottom right
        
        for(int i = 0; i < vertices.Count; i += 4)
        {
            triangles.Add(i); triangles.Add(i + 1); triangles.Add(i + 2);
            triangles.Add(i); triangles.Add(i + 2); triangles.Add(i + 3);
        }
        

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
