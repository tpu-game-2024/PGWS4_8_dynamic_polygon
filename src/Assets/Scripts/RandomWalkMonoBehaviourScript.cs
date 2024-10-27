using System.Collections.Generic;
using UnityEngine;

public class RandomWalkMonoBehaviourScript : MonoBehaviour
{
    Mesh mesh;
    float resetTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;        
    }

    // Update is called once per frame
    void Update()
    {
        resetTime -= Time.deltaTime;
        if (resetTime < 0.0f)
        {
            resetTime = Random.Range(1.0f, 3.0f);// ����������Ԋu
            generateThunder();
        }
    }

    void generateThunder()
    {
        // ���������n�߂�ʒu
        Vector3 pos = new Vector3(
            Random.Range(-300.0f, +300.0f), 
            1000.0f,
            Random.Range(-300.0f, +300.0f));

        // ���������n�߂�ۂ̌���
        Vector3 dir = Vector3.down;

        List<Vector3> points = new List<Vector3>();

        points.Add(pos);

        for(int i = 0; i < 1000; i++)// �i�ޏ�������߂Ă���
        {
            float d = Random.Range(-15.0f, 15.0f);// �Ȃ���p�x
            Quaternion q = Quaternion.AngleAxis(d, Vector3.forward);
            dir = (q * dir).normalized;

            float distance = Random.Range(30.0f, 50.0f);
            pos += dir * distance;

            points.Add(pos);

            if (pos.y < 0.0f) break;// �n�ʂɒ�����
        }

        generateMesh(points);
    }

    void generateMesh(List<Vector3> aPos)
    {
        if (aPos.Count < 2) return;// 2���_�͗~����

        mesh.Clear();

        int vertex_count = 2 * aPos.Count;

        // ���_
        Vector3[] vertices = new Vector3[vertex_count];
        int vtx = 0;
        for(int i = 0; i < aPos.Count; i++)
        {
            Vector3 dir;
            if(i == 0)
            {
                dir = aPos[1] - aPos[0];
            }
            else if(i == aPos.Count - 1)
            {
                dir = aPos[i] - aPos[i - 1];
            }
            else
            {
                dir = aPos[i + 1] - aPos[i - 1];
            }
            dir.Normalize();

            const float WIDTH = 3.0f;
            Vector3 t = new Vector3(-dir.y, dir.x,0.0f) * WIDTH;

            vertices[vtx++] = aPos[i] - t;
            vertices[vtx++] = aPos[i] + t;
        }

        // �F�F��
        Color[] colors = new Color[vertex_count];
        for(int i = 0;i < vertex_count; i++)
        {
            colors[i] = Color.white;
        }

        // �C���f�b�N�X
        int triangle_count = 6 * (aPos.Count - 1);
        int[] triangles = new int[triangle_count];
        int idx = 0;
        vtx = 0;
        while(idx < triangle_count)
        {
            triangles[idx + 0] = vtx + 0;
            triangles[idx + 1] = vtx + 1;
            triangles[idx + 2] = vtx + 2;

            triangles[idx + 3] = vtx + 2;
            triangles[idx + 4] = vtx + 1;
            triangles[idx + 5] = vtx + 3;

            idx += 6;
            vtx += 2;
        }

        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
