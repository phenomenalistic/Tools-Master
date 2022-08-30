using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    public int length;
	public bool finishObstacless = false;

	[Range(0.0f, 1.0f)] public float persent;
    public MeshRenderer mr;
    public MeshFilter mf;

	public GameObject trigger, roll, way, finishObstaclessGO;

	public int xSize = 20, ySize = 10, zSize = 4;
	public float roundness;

	private Mesh mesh;
	private Vector3[] vertices;
	private Vector3[] normals;
	private Color32[] cubeUV;

	public RollMaterials.RollColors rollColor;
	public List<RollMaterials> rollMaterials;
	[HideInInspector] [SerializeField] public RollMaterials rm = new RollMaterials();

	public RollTrigger rollTrigger;

	public void UpdateSpiralTwisting(GameObject pointer)
    {
		persent = Mathf.InverseLerp(transform.position.z - 0.4f, transform.position.z + (float)length - 0.5f - (((float)length / 1f) * 0.085f), pointer.transform.position.z);
		UpdateShader();
		if (persent >= 1f) { DropRoll(pointer); }
	}

	private void UpdateShader()
    {
		mr.materials[0].SetFloat("_RollCenterPosX", Mathf.Lerp(16f, (length + 1) * 20 - 10, persent));
		mr.materials[1].SetFloat("_RollCenterPosX", Mathf.Lerp(16f, (length + 1) * 20 - 10, persent));
		mr.materials[2].SetFloat("_RollCenterPosX", Mathf.Lerp(16f, (length + 1) * 20 - 10, persent));
	}

	public void DropRoll(GameObject pointer)
    {
		pointer.GetComponent<ChiselPS>().Stop();
		trigger.SetActive(false);
		StartCoroutine(StartDrop());
		StartCoroutine(StartRollRotate());
	}

	IEnumerator StartDrop()
    {
		Vector3 push = new Vector3(-0.08f, 0.55f, 0.19f), graviti = new Vector3(0, -0.7f, 0);
		//float scale = (5f / (float)length);
		float scale = 10;
		while (roll.transform.position.y > -20)
        {
			push += graviti * Time.deltaTime;
			roll.transform.position += push * Time.deltaTime * scale;
			yield return null;
        }
    }

	IEnumerator StartRollRotate()
    {
		//float scale = (5f / (float)length);
		float scale = 10;
		while (roll.transform.position.y > -20)
		{
			roll.transform.Rotate(0, 0, 10f * Time.deltaTime * scale);
			yield return null;
		}
	}

#if UNITY_EDITOR
    private void OnValidate()
    {
		//if (gameObject.scene.name != null && gameObject.scene.name != gameObject.name) { Init(); }
		Init();
	}

	public void ChangeLength(int length)
	{
		trigger.transform.localScale = new Vector3(1, 3, length);
		float newZposition = Mathf.FloorToInt((float)length / 2f);
		newZposition = length % 2f == 0 ? newZposition - 0.5f : newZposition;
		trigger.transform.localPosition = new Vector3(0, 1.1f, newZposition);
	}

	private void Init()
    {
        xSize = (length + 1) * 20 - 10;
		Generate();
		mr.sharedMaterial.SetFloat("_RollCenterPosX", Mathf.Lerp(16f, (length + 1) * 20 - 10, persent));
		//mr.material.SetFloat("_RollCenterPosX", Mathf.Lerp(14, (length + 1) * 10, persent));
		ChangeLength(length);
		SetRollColor();
		PutWay();
		IsFinishObstacles();
	}

	public void IsFinishObstacles()
	{
		finishObstaclessGO.GetComponent<FinishObstacles>().obstacleTag = ObstacleTag.spiral;
		if (finishObstacless)
		{
			transform.localScale = new Vector3(3, 3, 3);
			finishObstaclessGO.SetActive(true);
		}
		else
		{
			transform.localScale = new Vector3(1, 1, 1);
			finishObstaclessGO.SetActive(false);
		}
	}

	private void PutWay()
    {
		way.transform.localScale = new Vector3(length, 1, 1);
		way.transform.localPosition = new Vector3(0, -1, (float)length/2f - 0.5f);
    }

	private void Generate()
	{
		mf.mesh = mesh = new Mesh();
		mesh.name = "Prism";
		CreateVertices();
		CreateTriangles();
		mesh.RecalculateNormals();
		//mesh.RecalculateTangents();
		mesh.RecalculateBounds();
		//mesh.OptimizeReorderVertexBuffer();
		mesh.Optimize();
	}

	private void CreateVertices()
	{
		int cornerVertices = 8;
		int edgeVertices = (xSize + ySize + zSize - 3) * 4;
		int faceVertices = (
			(xSize - 1) * (ySize - 1) +
			(xSize - 1) * (zSize - 1) +
			(ySize - 1) * (zSize - 1)) * 2;
		vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
		normals = new Vector3[vertices.Length];
		cubeUV = new Color32[vertices.Length];

		int v = 0;
		for (int y = 0; y <= ySize; y++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				SetVertex(v++, x, y, 0);
			}
			for (int z = 1; z <= zSize; z++)
			{
				SetVertex(v++, xSize, y, z);
			}
			for (int x = xSize - 1; x >= 0; x--)
			{
				SetVertex(v++, x, y, zSize);
			}
			for (int z = zSize - 1; z > 0; z--)
			{
				SetVertex(v++, 0, y, z);
			}
		}
		for (int z = 1; z < zSize; z++)
		{
			for (int x = 1; x < xSize; x++)
			{
				SetVertex(v++, x, ySize, z);
			}
		}
		for (int z = 1; z < zSize; z++)
		{
			for (int x = 1; x < xSize; x++)
			{
				SetVertex(v++, x, 0, z);
			}
		}

		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.colors32 = cubeUV;
	}

	private void SetVertex(int i, int x, int y, int z)
	{
		Vector3 inner = vertices[i] = new Vector3(x, y, z);

		if (x < roundness)
		{
			inner.x = roundness;
		}
		else if (x > xSize - roundness)
		{
			inner.x = xSize - roundness;
		}
		if (y < roundness)
		{
			inner.y = roundness;
		}
		else if (y > ySize - roundness)
		{
			inner.y = ySize - roundness;
		}
		if (z < roundness)
		{
			inner.z = roundness;
		}
		else if (z > zSize - roundness)
		{
			inner.z = zSize - roundness;
		}

		normals[i] = (vertices[i] - inner).normalized;
		vertices[i] = inner + normals[i] * roundness;
		cubeUV[i] = new Color32((byte)x, (byte)y, (byte)z, 0);
	}

	private void CreateTriangles()
	{
		int[] trianglesZ = new int[(xSize * ySize) * 12];
		int[] trianglesX = new int[(ySize * zSize) * 12];
		int[] trianglesY = new int[(xSize * zSize) * 12];
		int ring = (xSize + zSize) * 2;
		int tZ = 0, tX = 0, tY = 0, v = 0;

		for (int y = 0; y < ySize; y++, v++)
		{
			for (int q = 0; q < xSize; q++, v++)
			{
				tZ = SetQuad(trianglesZ, tZ, v, v + 1, v + ring, v + ring + 1);
			}
			for (int q = 0; q < zSize; q++, v++)
			{
				tX = SetQuad(trianglesX, tX, v, v + 1, v + ring, v + ring + 1);
			}
			for (int q = 0; q < xSize; q++, v++)
			{
				tZ = SetQuad(trianglesZ, tZ, v, v + 1, v + ring, v + ring + 1);
			}
			for (int q = 0; q < zSize - 1; q++, v++)
			{
				tX = SetQuad(trianglesX, tX, v, v + 1, v + ring, v + ring + 1);
			}
			tX = SetQuad(trianglesX, tX, v, v - ring + 1, v + ring, v + 1);
		}

		tY = CreateTopFace(trianglesY, tY, ring);
		tY = CreateBottomFace(trianglesY, tY, ring);

		mesh.subMeshCount = 3;
		mesh.SetTriangles(trianglesZ, 0);
		mesh.SetTriangles(trianglesX, 1);
		mesh.SetTriangles(trianglesY, 2);
	}

	private int CreateTopFace(int[] triangles, int t, int ring)
	{
		int v = ring * ySize;
		for (int x = 0; x < xSize - 1; x++, v++)
		{
			t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + ring);
		}
		t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + 2);

		int vMin = ring * (ySize + 1) - 1;
		int vMid = vMin + 1;
		int vMax = v + 2;

		for (int z = 1; z < zSize - 1; z++, vMin--, vMid++, vMax++)
		{
			t = SetQuad(triangles, t, vMin, vMid, vMin - 1, vMid + xSize - 1);
			for (int x = 1; x < xSize - 1; x++, vMid++)
			{
				t = SetQuad(
					triangles, t,
					vMid, vMid + 1, vMid + xSize - 1, vMid + xSize);
			}
			t = SetQuad(triangles, t, vMid, vMax, vMid + xSize - 1, vMax + 1);
		}

		int vTop = vMin - 2;
		t = SetQuad(triangles, t, vMin, vMid, vTop + 1, vTop);
		for (int x = 1; x < xSize - 1; x++, vTop--, vMid++)
		{
			t = SetQuad(triangles, t, vMid, vMid + 1, vTop, vTop - 1);
		}
		t = SetQuad(triangles, t, vMid, vTop - 2, vTop, vTop - 1);

		return t;
	}

	private int CreateBottomFace(int[] triangles, int t, int ring)
	{
		int v = 1;
		int vMid = vertices.Length - (xSize - 1) * (zSize - 1);
		t = SetQuad(triangles, t, ring - 1, vMid, 0, 1);
		for (int x = 1; x < xSize - 1; x++, v++, vMid++)
		{
			t = SetQuad(triangles, t, vMid, vMid + 1, v, v + 1);
		}
		t = SetQuad(triangles, t, vMid, v + 2, v, v + 1);

		int vMin = ring - 2;
		vMid -= xSize - 2;
		int vMax = v + 2;

		for (int z = 1; z < zSize - 1; z++, vMin--, vMid++, vMax++)
		{
			t = SetQuad(triangles, t, vMin, vMid + xSize - 1, vMin + 1, vMid);
			for (int x = 1; x < xSize - 1; x++, vMid++)
			{
				t = SetQuad(
					triangles, t,
					vMid + xSize - 1, vMid + xSize, vMid, vMid + 1);
			}
			t = SetQuad(triangles, t, vMid + xSize - 1, vMax + 1, vMid, vMax);
		}

		int vTop = vMin - 1;
		t = SetQuad(triangles, t, vTop + 1, vTop, vTop + 2, vMid);
		for (int x = 1; x < xSize - 1; x++, vTop--, vMid++)
		{
			t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vMid + 1);
		}
		t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vTop - 2);

		return t;
	}

	private static int
	SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
	{
		triangles[i] = v00;
		triangles[i + 1] = triangles[i + 4] = v01;
		triangles[i + 2] = triangles[i + 3] = v10;
		triangles[i + 5] = v11;
		return i + 6;
	}

	public void SetRollColor()
	{
		foreach (RollMaterials r in rollMaterials)
		{
			if (r.name == rollColor) { rm = r; break; }
		}

		rollTrigger.rm = rm;
		
		mr.sharedMaterials = new Material[3] { rm.material, rm.material, rm.material };
	}
#endif
}

[System.Serializable]
public struct RollMaterials
{
	public enum RollColors
	{
		purple, blue, yellow, green, orange, red, 
	}

	public Material material;
	public Color oneColor, twoColor;
	public RollColors name;
}
