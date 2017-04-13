using UnityEngine;
using System.Collections;

// the super class for all buildings
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Building {

    public Vector3 position { get; protected set; }
    public string buildingType { get; protected set; }
    public float timeForBuilding { get; protected set; }
    public int price { get; protected set; }
    public GameObject go { get; protected set; }
    public int maxNum { get; protected set; }

    public bool isInstalled = false;

    public SpriteRenderer buildingSpriteRenderer = null;
    public Sprite _sprite;

    public void SetUpBuilding(string buildingType, float timeForBuilding, int price, int maxNum)
    {

        this.buildingType = buildingType;
        this.timeForBuilding = timeForBuilding;
        this.price = price;
        this.maxNum = maxNum;
        go = new GameObject();

    }

    // this is an interface : Ienumerator
    // and it has to have at least one yield return, which would give the control back to unity at this frame and gain back after the time being returned here.
    public IEnumerator WaitForBuildingToFinish()
    {
        OnBuildingMode();
        // above are executed in normal frame and order
        yield return new WaitForSeconds(timeForBuilding); // you can also return another IEnumerator, which means you want to wait for that to finish and come back to next line.
        // but here, the execution would suspend for timeForBuilding (sec)
        FinishedBuildingMode();
    }

    // All different kinds of building should have the same type of behaviours but different implementation.

    // instantiate the building object and attach it to the cursor as a transparent object.
    public abstract void InstantiateBuilding(Vector3 mousePosition);

    // once click the cursor, it should be installed on the grid where the cursor was last frame.
    public abstract void PlaceBuilding(Tile targetTile);

    // when it is set up, it should go into the building mode where a coroutine should appear later and goes to last phrase when finished
    public virtual void OnBuildingMode()
    {
		isInstalled = false;
        buildingSpriteRenderer.sprite = Resources.Load<Sprite>("Construction") as Sprite;
        buildingSpriteRenderer.sortingOrder = 1;

        if (!BuildingFactory.Buildings.Contains(this))
			BuildingFactory.Buildings.Add (this);
		if (!GameController.instance.GetComponent<AudioSource> ().isPlaying) {
			GameController.instance.GetComponent<AudioSource> ().Play ();
		}
    }

    // this should make all the stats work because the building has been finished. So it is literally there as a building.
    public virtual void FinishedBuildingMode()
    {
		isInstalled = true;
		AudioSource.PlayClipAtPoint (EventManager.instance.finishBuilding, Camera.main.transform.position, 3f);
        buildingSpriteRenderer.sprite = _sprite;
		if (BuildingFactory.Buildings.Contains (this)) {
			BuildingFactory.Buildings.Remove (this);
		}
		for (int i = 0; i < BuildingFactory.Buildings.Count; i++) {
			if (!BuildingFactory.Buildings [i].isInstalled) {
				return;
			}
		}
		GameController.instance.GetComponent<AudioSource> ().Stop ();
    }
}
