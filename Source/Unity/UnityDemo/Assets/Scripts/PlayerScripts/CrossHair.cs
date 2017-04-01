using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair: MonoBehaviour {

  public Texture2D crosshairTexture;
  Rect position;
    public bool on = true;

    void Start()
    {
        position = new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height -
            crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
       
    }

    void OnGUI()
    {
        if (on == true)
        {
            GUI.DrawTexture(position, crosshairTexture);
        }
    }
}
