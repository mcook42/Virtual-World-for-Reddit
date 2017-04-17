using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The menu that displays the credits.
/// </summary>
public class CreditsMenu : TempMenu {

	public void close()
	{

		Destroy (gameObject);
	}
}