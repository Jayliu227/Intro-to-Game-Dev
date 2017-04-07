using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ComboControl : MonoBehaviour {

    public static ComboControl instance;
    public AudioClip comboLaugh;

    [SerializeField]
    private Sprite[] comboSprites;
    private Image image;

    public delegate void comboDelegate();
    public static event comboDelegate comboEvent;

    private float counter = 2f;
    private bool isComboable = true;
    private int comboCount = 0;
    private AudioSource ads;

    // Use this for initialization
    void Start () {
        instance = this;
        image = transform.GetChild(0).GetComponent<Image>();
        ads = GetComponent<AudioSource>();
        //StartCoroutine("changeImage");
	}
	
	// Update is called once per frame
	void Update () {
        image.gameObject.SetActive((comboCount < 2) ? false : true);

        if (comboCount <= 0) isComboable = true;
        counter -= Time.deltaTime;
        if (counter < 0) { isComboable = true; comboCount = 0; }
        if (comboCount < comboSprites.Length && comboCount != 1) image.sprite = comboSprites[comboCount];
        //if (Input.GetMouseButtonDown(0)) comboUp();
	}

    public void comboUp()
    {
        if (!isComboable) { comboCount = 0; return; };
        comboCount++;
        if (comboEvent != null) comboEvent.Invoke();
        if (comboCount > 3)
        {
            ads.PlayOneShot(comboLaugh);
        }

        counter = 2f;
    }

    System.Collections.IEnumerator changeImage()
    {
        while (comboCount < comboSprites.Length)
        {
            image.sprite = comboSprites[comboCount++];
            if (comboEvent != null) comboEvent.Invoke();
            yield return new WaitForSeconds(2f);
        }
    }

}
