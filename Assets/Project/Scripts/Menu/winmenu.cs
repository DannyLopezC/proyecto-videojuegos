using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class winmenu : MonoBehaviour
{
    public Sprite[] Backgrounds; //this is an array of type sprite
 
     public Image img; //this is a variable of type sprite renderer
 
     // Use this for initialization
     void Start () {
            img = GetComponent<Image>(); 
            /*assigning the Render to the object's SpriteRender, this will allow us to access the image from 
            code*/
            img.sprite = Backgrounds[Random.Range(0, Backgrounds.Length)]; 
            /*this will change the current sprite of the sprite renderer to a random sprite that was chosen 
            randomly from the array of backgrounds */
         }
}
