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


public class LoadTest  {


	[MenuItem("LoadTest/ShereCreator")]
	static void makeSheres() {
		for (int x = 0; x < 200; x++) {
			for (int z = 0; z < 200; z++) {
				
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.position = new Vector3 (x *50, 5, z*50);



			}
		}
	}

	//Destorys everything in the scene
	[MenuItem("LoadTest/DestroyEverything")]
	static void destroySheres() {
		if(EditorUtility.DisplayDialog("Warning!", "This will destory ALL objects in the scenes. You should only have the LoadTest scene active!", "Destroy Everything", "Cancel"))
		{foreach(GameObject o in Object.FindObjectsOfType<GameObject>()) {
				GameObject.DestroyImmediate (o);}
		}
	}
	

}
