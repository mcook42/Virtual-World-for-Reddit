/**OutsideInfo.cs
* Caleb Whitman
* January 28, 2017
* 
*/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton class that holds information related to the outside scene.
/// </summary>
public class OutsideState : SceneState<OutsideState> {


    //The center of the loaded chunks
    public Point loadedCenterChunk=new Point(0,0);

    //The chunk that lies at Unity coordinate position (0,0)
    public Point worldCenterChunk=new Point(0,0);


    /// <summary>
    /// Clears data on transition from one scene to another.
    /// </summary>
    public override void clear()
    {
        loadedCenterChunk = new Point(worldCenterChunk);

    }

    /// <summary>
    /// Clears the data when exiting to the mainmenu.
    /// </summary>
    public override void reset()
    {
        loadedCenterChunk = new Point(0, 0);
        worldCenterChunk = new Point(0, 0);
    }

}
