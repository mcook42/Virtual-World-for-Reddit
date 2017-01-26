/* GameInfo.cs
 * Author: Caleb Whitman
 * January 18, 2016
 * 
 * A simple class used to represent an integer point.
 * 
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;


public class Point: EqualityComparer<Point>
{

	public int x;
	public int z;

	public Point(int x,int z)
	{
		this.x = x;
		this.z = z;
	}

	public Point()
	{
		x = 0;
		z = 0;
	}

	public override bool Equals (object obj)
	{
		return (obj is Point) &&( (obj as Point).x==x) && ((obj as Point).z==z);
	}

	public override bool Equals(Point p1, Point p2)
	{
		return (p1.x == p2.x) && (p1.z == p2.z);
	}

	public override int GetHashCode(Point p)
	{
		int hCode = p.x *7 + p.z; //TODO make this better
		return hCode.GetHashCode();
	}

	public override int GetHashCode ()
	{
		int hCode = x *7 + z; //TODO make this better
		return hCode.GetHashCode();
	}
}



