/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp;

/// <summary>
/// A wrapper class that holds a Post object from RedditSharp.
/// The Post object is not initialized in this class and must be set externally.
/// </summary>
public class BuildingThread : MonoBehaviour {

    public RedditSharp.Things.Post thread=null;
    public string threadName="";
}
