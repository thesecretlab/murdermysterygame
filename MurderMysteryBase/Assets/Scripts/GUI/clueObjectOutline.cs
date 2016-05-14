using UnityEngine;
using System.Collections;

public class clueObjectOutline : MonoBehaviour {
	 private Color startcolor;

	 void OnMouseEnter()
	 {
		startcolor = GetComponent<Renderer>().material.GetColor("_OutlineColor");
		GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.yellow);
	 }
	 void OnMouseExit()
	 {
		GetComponent<Renderer>().material.SetColor("_OutlineColor", startcolor);
	 }
}
