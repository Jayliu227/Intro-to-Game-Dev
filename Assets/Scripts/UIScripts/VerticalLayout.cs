using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VerticalLayout : MonoBehaviour {

    public float buttonSpacing = 35;


    // Use this for initialization
    void Start () {

        AdjustSize();
        
    }
    
    // Update is called once per frame
    void Update () {
  
    }

    public void AdjustSize()
    {
        Vector2 size = this.GetComponent<RectTransform>().sizeDelta;
        size.y = transform.childCount * buttonSpacing;
        this.GetComponent<RectTransform>().sizeDelta = size;
    }

}
