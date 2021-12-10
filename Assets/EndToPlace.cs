using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[ExecuteInEditMode]
public class EndToPlace : MonoBehaviour
{
    [MenuItem("Tools/Alexmelyon/End to place object _END", true, 3)]
    static bool ValidatePlaceSelectionOnTerrain()
    {
        return Selection.activeTransform != null;
    }

    [MenuItem("Tools/Alexmelyon/End to place object _END", false, 103)]
    static void PlaceSelectionOnTerrain()
    {
        foreach (Transform t in Selection.transforms)
        {
            Undo.RecordObject(t, "Move " + t.name);

            var meshComponents = t.GetComponentsInChildren<MeshRenderer>();
            if (meshComponents.Length != 0)
            {
                var vs = GetVertices(meshComponents);
                for(int i = 0; i < vs.Length; i++) { vs[i].y += t.position.y; }
                var min = LowestVertice(vs);
                var origin = t.position;
                RaycastHit hit;
                if(Physics.Raycast(origin, Vector3.down, out hit))
                {
                    float distance = min.y - hit.point.y;
                    t.Translate(Vector3.down * distance);
                }
            }
        }
    }

    private static Vector3 LowestVertice(Vector3[] vs)
    {
        Vector3 min = vs[0];
        foreach(var v in vs)
        {
            if(v.y < min.y)
            {
                min = v;
            }
        }
        return min;
    }

    private static Vector3[] GetVertices(MeshRenderer[] meshes)
    {
        var vertices = new List<Vector3>();
        foreach (var m in meshes)
        {
            var vs = m.GetComponent<MeshFilter>().sharedMesh.vertices;
            vertices.AddRange(vs);
        }
        return vertices.ToArray();
    }
}

#endif