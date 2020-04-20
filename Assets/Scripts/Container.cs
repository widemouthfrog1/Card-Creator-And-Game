using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Container : Item
{
    public Vector2 size = new Vector2(0,0);
    public List<Item> items = new List<Item>();
    
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

    }
}
