using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ConveyorBuilder : MonoBehaviour
{
    [SerializeField] Mesh end;
    [SerializeField] Mesh middle;
    [SerializeField] Vector3 rotation;
    [SerializeField] Vector3 offset;
    [SerializeField] float length = 0.01f;
    [SerializeField] float shrink;
    [SerializeField] float uvOffset = 0.9142822f;
    [SerializeField] int repeatX;
    [SerializeField] bool reverse;

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            Bake();
        }
    }

    private void Bake()
    {
        if (!middle || !end) return;

        MeshFilter filter = GetComponent<MeshFilter>();

        Quaternion r = Quaternion.Euler(rotation);
        float smin = float.MaxValue;
        float smax = float.MinValue;
        foreach (var vert in middle.vertices)
        {
            float d = Vector3.Dot(Vector3.forward, r * vert);
            if (d < smin) smin = d;
            if (d > smax) smax = d;
        }

        float wmin = float.MaxValue;
        float wmax = float.MinValue;
        foreach (var vert in middle.vertices)
        {
            float d = Vector3.Dot(Vector3.right, r * vert);
            if (d < wmin) wmin = d;
            if (d > wmax) wmax = d;
        }

        float width = wmax - wmin;
        float segmentLength = smax - smin - shrink;

        Mesh mesh = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        List<Vector3> normals = new List<Vector3>();

        for (int i = 0; i < repeatX; i++)
        {
            int c = (int)(length / segmentLength);
            for (int j = 0; j < c; j++)
            {
                float d = j * segmentLength;
                var segment = middle;
                Matrix4x4 matrix = Matrix4x4.TRS(offset, r, Vector3.one);

                if (j == 0)
                {
                    segment = end;
                    matrix = Matrix4x4.Rotate(Quaternion.Euler(Vector3.right * 180.0f)) * matrix;
                }

                if (j == c - 1)
                {
                    segment = end;
                }

                foreach (var tri in segment.triangles)
                {
                    tris.Add(tri + verts.Count);
                }

                foreach (var vert in segment.vertices)
                {
                    verts.Add((Vector3)(matrix * vert) + Vector3.forward * d + Vector3.right * i * width);
                }

                foreach (var normal in segment.normals)
                {
                    normals.Add(matrix * normal);
                }

                foreach (var uv in segment.uv)
                {
                    Vector2 val = uv + Vector2.right * uvOffset * j;
                    if (reverse) val.x = 1.0f - val.x;
                    uvs.Add(val);
                }
            }
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateBounds();
        filter.mesh = mesh;

        if (TryGetComponent(out BoxCollider collider))
        {
            collider.center = mesh.bounds.center;
            collider.size = mesh.bounds.size;
        }
    }
}