/**LoadTest.cs
 * Author: Caleb Whitman
 * Jan 4, 2017
 * 
 * This is going to be used to test loading and unloading large amounts of objects
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class LoadTest : MonoBehaviour {



	[MenuItem("LoadTest/CubeCreator")]
	static void makeSheres() {
		if (!EditorUtility.DisplayDialog ("Warning!", "This will create hundereds of cubes. You should only have the LoadTest scene active!", "Create Cubes", "Cancel"))
			return;
		Object my_light = AssetDatabase.LoadAssetAtPath("Assets/LoadTest/TestLight.prefab", typeof(GameObject));
		var temp = Instantiate(my_light, Vector3.zero, Quaternion.identity) as GameObject;
		temp.transform.eulerAngles = new Vector3 (-30, 50, 0);

		Object  my_camera= AssetDatabase.LoadAssetAtPath("Assets/LoadTest/TestCamera.prefab", typeof(GameObject));
		 temp= Instantiate(my_camera, Vector3.zero, Quaternion.identity) as GameObject;

		Object cube = AssetDatabase.LoadAssetAtPath("Assets/LoadTest/Cube.prefab", typeof(GameObject));

		for (int x = 0; x < 100; x++) {
			for (int z = 0; z < 100; z++) {
				

				GameObject clone = Instantiate(cube, Vector3.zero, Quaternion.identity) as GameObject;
				clone.transform.position = new Vector3 (x *50, 5, z*50);



			}
		}
	}

	//Destorys everything in the scene
	[MenuItem("LoadTest/DestroyEverything")]
	static void destroySheres() {
		if(EditorUtility.DisplayDialog("Warning!", "This will destory ALL objects in the scene. You should only have the LoadTest scene active!", "Destroy Everything", "Cancel"))
		{foreach(GameObject o in Object.FindObjectsOfType<GameObject>()) {
				GameObject.DestroyImmediate (o);}
		}
	}
	

}
