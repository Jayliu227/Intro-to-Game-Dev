using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoBarFixation : MonoBehaviour {

    Transform panel = null;
    RectTransform rtChild = null;
    public RectTransform canvas;

	// Use this for initialization
	void Start () {
        panel = this.transform.GetChild(0);
        panel.gameObject.SetActive(true);
        rtChild = panel.gameObject.GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update () {
        AdjustSize();
    }

    void AdjustSize()
    {
        Vector2 size = rtChild.sizeDelta;
        size.x = canvas.sizeDelta.x;
        rtChild.sizeDelta = size;
    }
}
