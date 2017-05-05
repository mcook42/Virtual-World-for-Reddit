using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReverseNormals  {


	/// <summary>
	/// Reverses the mesh filter on an object.
	/// </summary>
	[MenuItem("Reverse/ReverseNomralsOnSelected")]
	public static void reverseOnSelection()
	{
		///get selected object
		var obj = Selection.activeGameObject;

		//get mesh
		MeshFilter filter = obj.GetComponent<MeshFilter>() as MeshFilter;
		if (filter != null)
		{
			//reverse the normals
			Mesh mesh = filter.mesh;

			Vector3[] normals = mesh.normals;
			for (int i=0;i<normals.Length;i++)
				normals[i] = -normals[i];
			mesh.normals = normals;

			for (int m=0;m<mesh.subMeshCount;m++)
			{
				int[] triangles = mesh.GetTriangles(m);
				for (int i=0;i<triangles.Length;i+=3)
				{
					int temp = triangles[i + 0];
					triangles[i + 0] = triangles[i + 1];
					triangles[i + 1] = temp;
				}
				mesh.SetTriangles(triangles, m);
			}
		}        

		//reset mesh
		obj.GetComponent<MeshCollider>().sharedMesh = filter.mesh;
	}


}


