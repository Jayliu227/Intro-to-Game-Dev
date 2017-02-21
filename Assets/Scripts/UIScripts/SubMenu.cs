using UnityEngine;
using System.Collections;

public class SubMenu : MonoBehaviour {

    public int buttonSpacing = 35;

    // Use this for initialization
    void Start () {
        this.gameObject.SetActive(false);
        AdjustSize();
    }
    
    // Update is called once per frame
    void Update () {
        foreach(Transform t in transform.parent)
        {
            if (t != transform)
                if (t.gameObject.activeSelf)
                    gameObject.SetActive(false);
        }
    }

    public void AdjustSize()
    {
        Vector2 size = this.GetComponent<RectTransform>().sizeDelta;
        size.y = transform.childCount * buttonSpacing;
        this.GetComponent<RectTransform>().sizeDelta = size;
    }

    // this is for the button Build to open the sub menu
    public void SubMenuOpenOrClose()
    {
        if (this.gameObject.activeSelf == true)
            this.gameObject.SetActive(false);
        else
            this.gameObject.SetActive(true);
    }

}
