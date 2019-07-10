using UnityEngine;
using System.Collections.Generic;

internal abstract class BaseInfrastructureMaker
{
    protected MapReader map;

    public abstract int NodeCount { get; }

    public BaseInfrastructureMaker(MapReader mapReader)
    {
        map = mapReader;
    }
    public abstract IEnumerable<int>  Process();

    protected Vector3 GetCenter(OsmWay way)
    {
        Vector3 total = Vector3.zero;

        foreach (var id in way.NodeIDs)
        {
            total += map.nodes[id];
        }

        return total / way.NodeIDs.Count;
    }

    protected void CreateObject(OsmWay way, Material mat, string objectName)
    {
        // Make sure we have some name to display
        objectName = string.IsNullOrEmpty(objectName) ? "OsmWay" : objectName;

        // Create an instance of the object and place it in the center of its points
        GameObject go = new GameObject(objectName);
        Vector3 localOrigin = GetCenter(way);
        go.transform.position = localOrigin - map.bounds.Center;

        // Add the mesh filter and render components to the object
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();

        // Apply the material
        mr.material = mat;

        // Creat the collections for the object's vertices, indeces, UVs etc.
        List<Vector3> vectors = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> indices = new List<int>();

        // Call the child class' object creation code
        OnObjectCreated(way, localOrigin, vectors, normals, uvs, indices);

        mf.sharedMesh = new Mesh();

        mf.sharedMesh.vertices = vectors.ToArray();
        mf.sharedMesh.normals = normals.ToArray();
        mf.sharedMesh.triangles = indices.ToArray();
        mf.sharedMesh.uv = uvs.ToArray();
    }
    protected abstract void OnObjectCreated(OsmWay way, Vector3 origin, List<Vector3> vectors, List<Vector3> normals, List<Vector2> uvs, List<int> indices);
}