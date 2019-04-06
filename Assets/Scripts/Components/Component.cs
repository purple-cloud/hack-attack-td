using Defenses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Contains the most basic functions for all structures such as base, firewall, etc...
/// </summary>
public abstract class Component : MonoBehaviour, IPointerUpHandler {

	[SerializeField] // A reference to the image displayed in canvas
    private Image canvasImage;

	[SerializeField]
	private string outputObjectName;

	[HideInInspector]
	public List<GameObject> input;

	[HideInInspector]
	public GameObject output;

    /// <summary>
    /// List containing all the specific upgrades for desired component
    /// </summary>
    public ComponentUpgrade[] Upgrades { get; set; }

    /// <summary>
    /// Getter & setter for the component level
    /// </summary>
    public int ComponentLevel { get; set; }

    /// <summary>
    /// Getter & setter for the component name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Getter & setter for the component status
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// Getter & setter for the component repair price
    /// </summary>
    public int RepairPrice { get; set; }

    /// <summary>
    /// Getter & setter for the component sell price
    /// </summary>
    public int SellValue { get; set; }

    /// <summary>
    /// Getter & setter for the component sprite
    /// </summary>
    public Sprite Sprite { get; set; }

    /// <summary>
    /// Getter & setter for the component price
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Getter & setter for the component locked status
    /// </summary>
    public bool Locked { get; set; }

    /// <summary>
    /// Getter & setter for the component initial price
    /// </summary>
    public int InitialPrice { get; set; }
  
    /// <summary>
    /// Getter & setter for the component durability
    /// </summary>
    public float Durability { get; set; }

    /// <summary>
    /// Generates getter and private setter for upgrade encryption
    /// </summary>
    public float Encryption { get; set; }

    /// <summary>
    /// Getter & setter for sellable
    /// </summary>
    public bool Sellable { get; set; }

    /// <summary>
    /// Getter & setter for components immunity for virus
    /// </summary>
    public bool ImmuneToVirus { get; set; }
  
    /// <summary>
    /// Awake is called after all objects are initialized
    /// </summary>
    private void Awake() {
		input = new List<GameObject>();
		output = (outputObjectName != null) ? GameObject.Find(outputObjectName) : null;

		this.ComponentLevel = 1;
        this.Sellable = false;
        this.InitialPrice = 0;
        // Sets immune default to false;
        ImmuneToVirus = false;
		ValidateLineRenderComponent();
	}

    /// <summary>
    /// Returns the next upgrade for the component if there are any.
    /// Otherwise return null
    /// </summary>
    public ComponentUpgrade NextUpgrade {
        get {
            if (this.Upgrades.Length > this.ComponentLevel - 1) {
                return this.Upgrades[this.ComponentLevel - 1];
            }
            return null;
        }
        set {

        }
    }

	public void SetLineRendererMaterial(Material m) {
		// Set the material for the line
		gameObject.GetComponent<LineRenderer>().material = m;
	}

    /// <summary>
    /// Is called by the GameManager when pressing Repair on the stats panel
    /// after selecting a component
    /// </summary>
    public void Repair() {
        try {
            GameManager.Instance.SetCurrency(GameManager.Instance.GetCurrency() - RepairPrice);
            this.Status = true;
        } catch (NullReferenceException nre) {
            Debug.LogException(nre);
        }
    }

    /// <summary>
    /// Is called by the GameManager when pressing Upgrade on the stats panel
    /// after selecting a component. 
    /// </summary>
    public void Upgrade() {
        if (this.NextUpgrade != null) {
            try {
                Debug.Log("Component Level: " + this.ComponentLevel);
                GameManager.Instance.SetCurrency(GameManager.Instance.GetCurrency() - NextUpgrade.Price);
                this.Sprite = NextUpgrade.Sprite;
                this.Name = NextUpgrade.Name;
                this.RepairPrice = NextUpgrade.RepairPrice;
                this.SellValue = NextUpgrade.SellValue;
                this.Encryption = NextUpgrade.Encryption; 

                // Assign the value to the upgrade button
                Debug.Log("this.Price: " + Price);
                Debug.Log("this.NextUpgrade.Price" + NextUpgrade.Price);
                ComponentLevel++;
                // UpdateComputerPanel();
            } catch (NullReferenceException nre) {
                Debug.LogException(nre);
            }
        } else {
            Debug.Log("Currently no Upgrades left");
        }
    }

	/// <summary>
	/// Loops though all child objects of Component and ensures they have a LineRenderer component.
	/// If they don't, add it.
	/// </summary>
	private void ValidateLineRenderComponent() {
		if (gameObject.GetComponent(typeof(Component)) != null) {
			// Add LineRenderer if it is doesn't exist
			if (gameObject.GetComponent<LineRenderer>() == null)
				gameObject.AddComponent<LineRenderer>();

			gameObject.GetComponent<LineRenderer>().sortingLayerName = "UI";
			gameObject.GetComponent<LineRenderer>().material = CompController.Instance.pathLineMaterial;
		}
	}

	public void Buy() {
        Debug.Log("Buying " + this.Name + " component...");
        GameManager.Instance.SetCurrency(GameManager.Instance.GetCurrency() - this.InitialPrice);
    }

    /*
    public virtual string GetStats() {

    }*/

    /// <summary>
    /// Takes in the status boolean and returns a text
    /// depending on if boolean is true or false
    /// </summary>
    /// <returns>
    /// Return either "Active" or "Disabled" depending on status boolean
    /// </returns>
    public string GetStatus() {
        string status = "";
        if (this.Status) {
            status = string.Format("<color=#00FF00>Active</color>");
        } else {
            status = string.Format("<color=#FF0000>Disabled</color>");
        }
        return status;
    }

	/// <summary>
	/// Notifies the controller about the click event.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerUp(PointerEventData eventData) {
        if (Defenses.CompController.Instance.IsPlacingStructure) {
            Defenses.CompController.Instance.OnStructureClickEvent(gameObject);
        } else if (BackupManager.Instance.BackupReady) {
            BackupManager.Instance.AddToBackupPool(gameObject);
        }
	}

	/// <summary>
	/// Creates a green border on all compatible combination of structures on the canvas (when an item is picked from the item slot).
	/// </summary>
	/// <param name="state">Controls if the game object will create or remove border.</param>
	public void ShowHighlight(bool state) {
		GameObject borderGO;
		if (state == true) {
			// Instantiates new game object under this game object and gives it a color
			borderGO = Instantiate(CompController.Instance.highlightBorder as GameObject);
			borderGO.name = "HighlightBorder";
			borderGO.GetComponent<Image>().color = Color.green;
			borderGO.transform.position = gameObject.transform.position;
			borderGO.transform.SetParent(gameObject.transform);
		} else {
			// Removes the border if it exists
			if (gameObject.transform.Find("HighlightBorder") != null) {
				Destroy(gameObject.transform.Find("HighlightBorder").gameObject);
			}
		}
	}

	/// <summary>
	/// Sets the component's canvas image
	/// </summary>
	/// <param name="sprite">the sprite to set to the canvas image</param>
	public void SetCanvasSprite(Sprite sprite) {
        this.canvasImage.sprite = sprite;
    }

    /// <summary>
    /// Currently this method needs to be called for each component
    /// 
    /// When the component is clicked it checks if the GameManagers selected component = this component,
    /// and if so closes the information panel. If not it updates the information panel with
    /// the specified information from the clicked component and opens up the panel if its not open
    /// </summary>
    public void OnPointerClick(PointerEventData eventData) {
        if (GameManager.Instance.GetSelectedGameObjext == this) {
            GameManager.Instance.DeselectGameObject();
        } else {
            GameManager.Instance.SelectGameObjext(gameObject);
            GameManager.Instance.UpdateComputerPanel();
        }
    }

	void OnTriggerStay2D(Collider2D col) {
		CompController.Instance.CollisionDetected = true;
	}

	//TODO Stop mouse pointer altering the clones position when collision is detected.
	void OnTriggerExit2D(Collider2D col) {
		CompController.Instance.CollisionDetected = false;
	}

	void Update() {
		if (output != null) {
			// Creates lines between this gameObject and output gameObject
			LineRenderer lr = gameObject.GetComponent<LineRenderer>();
			lr.SetPosition(0, gameObject.transform.position);
			lr.SetPosition(1, output.transform.position);
		}
	}

  public Image GetCanvasImage() {
        return this.canvasImage;
    }

    public void SetCanvasImage(Image image) {
        this.canvasImage = image;
    }
}
