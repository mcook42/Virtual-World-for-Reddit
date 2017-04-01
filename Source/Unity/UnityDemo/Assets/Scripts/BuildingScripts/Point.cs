/* Point.cs
 * Author: Caleb Whitman
 * January 18, 2016
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple class used to represent an integer point.
/// </summary>
public class Point: EqualityComparer<Point>
{

	public int x;
	public int z;

    /// <summary>
    /// Creates a point with input coordinates.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="z">Z coordinate.</param>
	public Point(int x,int z)
	{
		this.x = x;
		this.z = z;
	}

    /// <summary>
    /// Creates the Point (0,0).
    /// </summary>
	public Point()
	{
		x = 0;
		z = 0;
	}

    /// <summary>
    /// Copy Constructor.
    /// </summary>
    /// <param name="p">Point to copy.</param>
    public Point(Point p)
    {
        this.x = p.x;
        this.z = p.z;
    }

    /// <summary>
    /// Two points are equal if both their x and z values are equal.
    /// </summary>
    /// <param name="obj">A Point.</param>
    /// <returns>True if the object is a point and has the same x and z values.</returns>
	public override bool Equals (object obj)
	{
		return (obj is Point) &&( (obj as Point).x==x) && ((obj as Point).z==z);
	}

    /// <summary>
    /// Two points are equal if both their x and z values are equal.
    /// </summary>
    /// <param name="p1">A Point.</param>
    /// <param name="p2">A Point.</param>
    /// <returns>True if the points have the same x and z values.</returns>
	public override bool Equals(Point p1, Point p2)
	{
		return (p1.x == p2.x) && (p1.z == p2.z);
	}

    /// <summary>
    /// Returns a hashcode for the point.
    /// </summary>
    /// <param name="p">A point.</param>
    /// <returns>A hashcode.</returns>
	public override int GetHashCode(Point p)
	{
		int hCode = p.x *7 + p.z; //TODO make this better
		return hCode.GetHashCode();
	}

    /// <summary>
    /// Returns a hashcode for the point.
    /// </summary>
    /// <returns>A hashcode.</returns>
	public override int GetHashCode ()
	{
		int hCode = x *7 + z; //TODO make this better
		return hCode.GetHashCode();
	}
}



