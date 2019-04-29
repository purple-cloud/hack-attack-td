using Defenses;
using System;
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
	private string[] outputObjectName;

	//[HideInInspector]
	public List<GameObject> input;

	//[HideInInspector]
	public List<GameObject> outputs;

	#region PROPERTIES
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
    /// Getter & setter for the component backup price
    /// </summary>
    public int BackupPrice { get; set; }

    /// <summary>
    /// Getter & setter for the component backup restore price
    /// </summary>
    public int BackupRestorePrice { get; set; }
  
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
	#endregion

	#region EVENTS

	/// <summary>
	/// Notifies the controller about the click event.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerUp(PointerEventData eventData) {
		if (Defenses.CompController.Instance.IsPlacingStructure) {
			Defenses.CompController.Instance.OnStructureClickEvent(gameObject);
		} else if (PathConnection.Instance.IsSelectingStructure) {
			PathConnection.Instance.ShowComponentInputOutput(this);
		}
		// If user is choosing to select component to backup
		else if (BackupManager.Instance.BackupReady) {
			BackupManager.Instance.AddToBackupPool(gameObject);
		}
		// If user have pressed backupped component from backup selection panel
		else if (BackupManager.Instance.BackupComponentSelected == true && BackupManager.Instance.BackupReady == false) {
			if (((Component) BackupManager.Instance.BackuppedComponent.GetComponent(typeof(Component))).GetType() ==
					((Component) gameObject.GetComponent(typeof(Component))).GetType()) {
				// TODO Find a way to add the backupped component to replace current with as parameter
				BackupManager.Instance.ReplaceComponent(BackupManager.Instance.BackuppedComponent, gameObject);
			}
		}
	}

	/// <summary>
	/// Currently this method needs to be called for each component
	/// 
	/// When the component is clicked it checks if the GameManagers selected component = this component,
	/// and if so closes the information panel. If not it updates the information panel with
	/// the specified information from the clicked component and opens up the panel if its not open
	/// </summary>
	public void OnPointerClick(PointerEventData eventData) {
		if (GameManager.Instance.GetSelectedGameObject == this.gameObject) {
			GameManager.Instance.DeselectGameObject();
		} else {
			GameManager.Instance.SelectGameObject(gameObject);
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

	#endregion

	/// <summary>
	/// Awake is called after all objects are initialized
	/// </summary>
	private void Awake() {
		input = new List<GameObject>();

		foreach (string outputString in outputObjectName) {
			GameObject obj = GameObject.Find(outputString);
			if (obj != null) {
				outputs.Add(obj);
			}
		}

		if (gameObject.GetComponent<LineHandler>() == null) {
			gameObject.AddComponent<LineHandler>();
		}

		LineHandler lineHandler = gameObject.GetComponent<LineHandler>();
		foreach (GameObject output in outputs) {
			lineHandler.AddList(output);
		}

		this.ComponentLevel = 1;
        this.Sellable = false;
        this.InitialPrice = 0;
        // Sets immune default to false;
        ImmuneToVirus = false;
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
                this.Durability = NextUpgrade.Durability;
                this.BackupPrice = NextUpgrade.BackupPrice;
                this.BackupRestorePrice = NextUpgrade.BackupRestorePrice;

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


	#region OUTPUT
	/// <summary>
	/// Finds all outputs that has a defined component type.
	/// </summary>
	/// <param name="type">A structure type</param>
	/// <returns>A list of all outputs with that has the specified type</returns>
	public GameObject[] GetOutputs(System.Type type) {
		return outputs.FindAll(obj => obj.GetComponent(typeof(Component)).GetType() == type).ToArray();
	}

    /// <summary>
    /// Removes the output object from this structure if it exists.
    /// </summary>
    /// <param name="obj">Output object</param>
    /// <returns>If the object was removed without errors, return<code>true</code>, else <code>false</code></returns>
    public bool RemoveOutput(GameObject obj) {
		if (outputs.Contains(obj)) {
			outputs.Remove(obj);
		}
		return gameObject.GetComponent<LineHandler>().RemoveLine(obj);
	}

	/// <summary>
	/// Collects all components with a spesific type of <code>Component</code>.
	/// </summary>
	/// <param name="type">A derived class type of <code>Component</code></param>
	/// <returns>A list of all components with the type</returns>
	public Component[] GetOutputComponents(System.Type type) {
		List<Component> comps = new List<Component>();

		foreach (GameObject output in 
			outputs.FindAll(obj => obj.GetComponent(typeof(Component)).GetType() == type)) {
			comps.Add(output.GetComponent(typeof(Component)) as Component);
		}

		return comps.ToArray();
	}

	/// <summary>
	/// Collects all output components.
	/// </summary>
	/// <returns>All output components</returns>
	public Component[] GetOutputComponents() {
        List<Component> outputComps = new List<Component>();
        foreach (GameObject obj in outputs) {
            outputComps.Add(obj.GetComponent(typeof(Component)) as Component);
        }
        return outputComps.ToArray();
	}

	#endregion

	#region INPUT

	/// <summary>
	/// Adds a new input to the specified structure
	/// </summary>
	/// <param name="obj"></param>
	public void AddInput(GameObject obj) {
		if (!this.input.Contains(obj)) {
			this.input.Add(obj);
		}
	}

	/// <summary>
	/// Removes the input object from this structure if it exists.
	/// </summary>
	/// <param name="obj">Input object</param>
	/// <returns>If the object was removed without errors, return<code>true</code>, else <code>false</code></returns>
	public bool RemoveInput(GameObject obj) {
		if (this.input.Contains(obj)) {
			this.input.Remove(obj);
		}
		return gameObject.GetComponent<LineHandler>().RemoveLine(obj);
	}

	/// <summary>
	/// Adds a new output to this structure.
	/// </summary>
	/// <param name="obj"></param>
	public void AddOutput(GameObject obj) {
		if (!outputs.Contains(obj)) {
			outputs.Add(obj);
			gameObject.GetComponent<LineHandler>().Add(obj);
		}
	}

	public Component[] GetInputComponents() {
        List<Component> inputComps = new List<Component>();
        foreach (GameObject obj in input) {
            inputComps.Add(obj.GetComponent(typeof(Component)) as Component);
        }
        return inputComps.ToArray();
    }

	#endregion

	public Image GetCanvasImage() {
        return this.canvasImage;
    }

    public void SetCanvasImage(Image image) {
        this.canvasImage = image;
    }
}
