using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHover : MonoBehaviour
{

    public Sprite goodBackground, sinisterBckground;
    
    public Image background;

    GameObject auxImage ;

    void Start(){
        auxImage = GameObject.Find("Image");
        background = auxImage.GetComponent<Image>();
    }

    void Update(){
        auxImage = GameObject.Find("Image");
        background = auxImage.GetComponent<Image>();
    }
    
    public void ChangeBackgroundImgtoSinister(){
        
        background.sprite = sinisterBckground;
    }

    public void ChangeBackgroundImgtoGood(){

        background.sprite = goodBackground;

    }
}

