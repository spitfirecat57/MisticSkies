using UnityEngine;
using System.Collections;

public class MusicFadeInOut : MonoBehaviour
{


}

/*
var track1 : AudioClip;
 var track2 : AudioClip;
 
 audio.clip = track1;
 audio.Play();
 
 var audio1Volume : float = 1.0;
 var audio2Volume : float = 0.0;
 var track2Playing : boolean = false;
 
 function Update() {
     fadeOut();
 
     if (audio1Volume <= 0.1) {
         if(track2Playing == false)
         {
           track2Playing = true;
           audio.clip = track2;
           audio.Play();
         }
         
         fadeIn();
     }
 }
 
 function OnGUI()
 {
     GUI.Label(new Rect(10, 10, 200, 100), "Audio 1 : " + audio1Volume.ToString());
     GUI.Label(new Rect(10, 30, 200, 100), "Audio 2 : " + audio2Volume.ToString());
 }
 
 function fadeIn() {
     if (audio2Volume < 1) {
         audio2Volume += 0.1 * Time.deltaTime;
         audio.volume = audio2Volume;
     }
 }
 
 function fadeOut() {
     if(audio1Volume > 0.1)
     {
         audio1Volume -= 0.1 * Time.deltaTime;
         audio.volume = audio1Volume;
     }
 }*/