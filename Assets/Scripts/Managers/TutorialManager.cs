using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>TutorialManager</c> 
/// </summary>
public class TutorialManager : MonoBehaviour {

    #region SERIALIZED FIELDS

    [Header("Tutorial Panel")]

    [SerializeField] // Reference to the tutorial panel
    private GameObject tutorialPanel;

    [SerializeField] // Reference to the tutorial panel text
    private Text tutorialTxt;

    [SerializeField] // Reference to the tutorial panel next button
    private Button nextBtn;

    [SerializeField] // Reference to the tutorial panel previous button
    private Button prevBtn;

    [Header("Level Information Panel")]

    [SerializeField] // A reference to the full covering information panel button
    private Button tutorialInfoBtn;

    [Header("Restricted Access")]

    [SerializeField] // A reference to the modify path button
    private GameObject modifyPathBtn;

    [SerializeField] // A reference to the add path button
    private GameObject addPathBtn;

    [SerializeField] // A reference to the firewall action bar slot
    private GameObject firewallSlot;

    [SerializeField] // A reference to the backup action bar 
    private GameObject backupSlot;

    [SerializeField] // A reference to the add backup menu selection on backup menu popup
    private GameObject addBackupBtn;

    [SerializeField] // A reference to the predefined location to place the first firewall
    private GameObject predefinedLocationForFirstFirewall;

    [SerializeField] // A reference to the predefined location to place the second firewall
    private GameObject predefinedLocationForSecondFirewall;

    [Header("Components")]

    [SerializeField] // A reference to the earth component
    private GameObject earth;

    [SerializeField] // A reference to the computer component
    private GameObject computer;

    [SerializeField] // A reference to the document component
    private GameObject document;

    #endregion

    #region VARIABLES

    private bool readyToMoveOn;

    private int state;

    private bool firewallSlotState = false;

    private bool computerState = false;

    private bool internetState = false;

    private bool inbetweenStructPlacement = false;

    #endregion

    /// <summary>
    /// Initializes the tutorial level. Sets default values and disables/enables 
    /// appropriate components, buttons, menus etc and setup event listeners
    /// </summary>
    private void Start() {
        UserBehaviourProfile.Instance.tutorialLvl = true;
        Defenses.CompController.Instance.canPlaceTutorialStruct = false;

        this.readyToMoveOn = false;
        this.state = 0;
        this.nextBtn.onClick.AddListener(NextStep);
        this.tutorialInfoBtn.onClick.AddListener(TutorialInit);
        GameManager.Instance.SetCurrency(100000);

        // Disable all access to restricted buttons
        this.modifyPathBtn.SetActive(false);
        this.addPathBtn.SetActive(false);
        this.firewallSlot.SetActive(false);
        this.backupSlot.SetActive(false);

        // Init restricted access event trigger
        this.modifyPathBtn.GetComponent<Button>().onClick.AddListener(NextStep);
        this.firewallSlot.GetComponent<Button>().onClick.AddListener(FireFirewallEventTrigger);
        this.addBackupBtn.GetComponent<Button>().onClick.AddListener(NextStep);
        this.computer.GetComponent<Button>().onClick.AddListener(FireComputerEventTrigger);
        this.earth.GetComponent<Button>().onClick.AddListener(FireInternetEventTrigger);
        this.predefinedLocationForFirstFirewall.GetComponent<Button>().onClick.AddListener(PlaceFirewallComponentOnSpecifiedLocation);
        this.predefinedLocationForSecondFirewall.GetComponent<Button>().onClick.AddListener(PlaceFirewallComponentOnSpecifiedLocation);

        // Catches component placed event
        EventManager.onComponentPlaced += PlaceFirewallComponentOnSpecifiedLocation;

        // Disable component clickable
        ChangeComponentClickableState(this.computer, false);
        ChangeComponentClickableState(this.earth, false);
        ChangeComponentClickableState(this.document, false);
    }

    /// <summary>
    /// Controls the state of the tutorial level. Each state contains
    /// different tasks.
    /// </summary>
    private void StateMachine() {
        switch (this.state) {
            case 1:
                FirstTask();
                break;

            case 2:
                SecondTask();
                break;

            case 3:
                ThirdTask();
                break;

            case 4:
                FourthTask();
                break;

            case 5:
                FifthTask();
                break;

            case 6:
                SixthTask();
                break;

            case 7:
                SeventhTask();
                break;

            case 8:
                EightTask();
                break;

            case 9:
                NinthTask();
                break;

            case 10:
                TenthTask();
                break;

            case 11:
                EleventhTask();
                break;

            case 12:
                TwelfthTask();
                break;

            case 13:
                ThirteenthTask();
                break;

            case 14:
                FourteenthTask();
                break;

            case 15:
                FifteenthTask();
                break;

            default:
                Debug.Log("Couldn't find next or previous stage");
                break;
        }
    }

    /// <summary>
    /// Is triggered by clicking the firewall slot
    /// </summary>
    private void FireFirewallEventTrigger() {
        if (this.firewallSlotState) {
            NextStep();
        }
    }

    /// <summary>
    /// Is triggered by clicking the computer component
    /// </summary>
    private void FireComputerEventTrigger() {
        if (this.computerState && this.inbetweenStructPlacement == false) {
            NextStep();
        } else if (this.computerState && this.inbetweenStructPlacement) {
            // Check what mouse btn is pressed
            if (Input.GetMouseButtonUp(1)) {
                NextStep();
            }
        }
    }

    /// <summary>
    /// Is triggered by clicking the internet component
    /// </summary>
    private void FireInternetEventTrigger() {
        Debug.Log("Firing internet event trigger");
        if (this.internetState && this.inbetweenStructPlacement == false) {
            NextStep();
        } else if (this.internetState && this.inbetweenStructPlacement) {
            // Check what mouse btn is pressed
            if (Input.GetMouseButtonDown(1)) {
                Debug.Log("Right Mouse Click..");
                NextStep();
            }
        }
    }

    /// <summary>
    /// Is triggered by clicking one of the predefined locations for placing a firewall
    /// </summary>
    public void PlaceFirewallComponentOnSpecifiedLocation() {
        Defenses.CompController.Instance.canPlaceTutorialStruct = true;
        NextStep();
    }

    /// <summary>
    /// Initial task in the tutorial
    /// </summary>
    private void TutorialInit() {
        ShowTutorialPanel(true);
        this.tutorialTxt.text = "For starters we can see we have a computer, a document and the internet. All these are connected together via inputs and outputs";
    }

    private void FirstTask() {
        ChangeBtnState(this.nextBtn);
        this.firewallSlotState = true;
        this.firewallSlot.SetActive(true);
        HighlightComponent(this.firewallSlot, true);
        this.tutorialTxt.text = "To extend your system with another component, (this is just an example) click the highlighted FirewallSlot in your actionbar";
    }

    private void SecondTask() {
        this.firewallSlotState = false;
        this.computerState = true;
        ChangeComponentClickableState(this.computer, true);
        HighlightComponent(this.firewallSlot, false);
        HighlightComponent(this.computer, true);
        this.tutorialTxt.text = "Now LEFT CLICK on the hightlighted computer";
    }

    private void ThirdTask() {
        ChangeComponentClickableState(this.computer, false);
        this.computerState = false;
        HighlightComponent(this.computer, false);
        this.predefinedLocationForFirstFirewall.SetActive(true);
        HighlightComponent(this.predefinedLocationForFirstFirewall, true);
        this.tutorialTxt.text = "Now LEFT CLICK the highlighted area in the canvas";
    }

    private void FourthTask() {
        Defenses.CompController.Instance.canPlaceTutorialStruct = false;
        HighlightComponent(this.predefinedLocationForFirstFirewall, false);
        this.predefinedLocationForFirstFirewall.SetActive(false);
        this.tutorialTxt.text = "WELL DONE! Now you can try and place a firewall between to existing components. First LEFT CLICK on the firewall slot again";
        this.firewallSlotState = true;
        HighlightComponent(this.firewallSlot, true);
        this.inbetweenStructPlacement = true;
        this.internetState = true;
    }

    private void FifthTask() {
        this.firewallSlotState = false;
        HighlightComponent(this.firewallSlot, false);
        this.tutorialTxt.text = "Now comes the really important task. To place a defense between to existing components you have to use the RIGHT CLICK mouse button. First RIGHT CLICK on the internet.";
        ChangeComponentClickableState(this.earth, true);
        HighlightComponent(this.earth, true);
		// Waits for component being right clicked before proceeding
		EventManager.onRightClickComponent += NextStep;
	}

    private void SixthTask() {
        ChangeComponentClickableState(this.earth, false);
        HighlightComponent(this.earth, false);
        this.internetState = false;
        this.tutorialTxt.text = "Now RIGHT CLICK the computer";
        this.computerState = true;
        ChangeComponentClickableState(this.computer, true);
        HighlightComponent(this.computer, true);
    }

    private void SeventhTask() {
        this.computerState = false;
        ChangeComponentClickableState(this.computer, false);
        HighlightComponent(this.computer, false);
        this.predefinedLocationForSecondFirewall.SetActive(true);
        HighlightComponent(this.predefinedLocationForSecondFirewall, true);
        this.tutorialTxt.text = "Now LEFT CLICK the highlighted area in the canvas";
		// After the two previous tasks has been done, unsubscribe
		EventManager.onRightClickComponent -= NextStep;
    }

    private void EightTask() {
        HighlightComponent(this.predefinedLocationForSecondFirewall, false);
        this.predefinedLocationForSecondFirewall.SetActive(false);
        this.inbetweenStructPlacement = false;
        this.tutorialTxt.text = "Well done, you are now relatively secure against attacks from the internet. Now you need to remove the direct connection from the internet to the computer. To do this, click the hightlighted button in the top right corner.";
        this.modifyPathBtn.SetActive(true);
        HighlightComponent(this.modifyPathBtn, true);
    }

    private void NinthTask() {
        HighlightComponent(this.modifyPathBtn, false);
        this.modifyPathBtn.GetComponent<Button>().enabled = false;
        this.tutorialTxt.text = "Now click the internet";
        this.internetState = true;
        HighlightComponent(this.earth, true);
        ChangeComponentClickableState(this.earth, true);
    }

    private void TenthTask() {
        this.internetState = false;
        HighlightComponent(this.earth, false);
        ChangeComponentClickableState(this.earth, false);
        this.tutorialTxt.text = "Now you can see the different connections for the selected component in the panel in the bottom right. We wish to keep the connection to the firewall, but delete the direct connection to the computer. Click the 'X' button on the right of the displayed connection. Press NEXT after you have done this.";
        ChangeBtnState(this.nextBtn);
    }

    private void EleventhTask() {
        this.addPathBtn.SetActive(true);
        HighlightComponent(this.addPathBtn, true);
        this.tutorialTxt.text = "You can also create a new connection between existing components by clicking the '+'button highlighted in the top right corner. Now you can select two compoents by right clicking them and a connection will automatically be established. Press NEXT after you have tried this out.";
    }

    private void TwelfthTask() {
        ChangeBtnState(this.nextBtn);
        this.addPathBtn.GetComponent<Button>().enabled = false;
        HighlightComponent(this.addPathBtn, false);
        HighlightComponent(this.backupSlot, true);
        this.tutorialTxt.text = "Now it is time to take backup of a component. First click the highlighted backupslot, then click the 'Backup Component' button";
        this.backupSlot.SetActive(true);
    }

    private void ThirteenthTask() {
        this.addBackupBtn.GetComponent<Button>().enabled = false;
        HighlightComponent(this.backupSlot, false);
        this.computerState = true;
        ChangeComponentClickableState(this.computer, true);
        HighlightComponent(this.computer, true);
        this.tutorialTxt.text = "Now click the computer component to take a backup of it";
    }

    private void FourteenthTask() {
        this.computerState = false;
        ChangeComponentClickableState(this.computer, false);
        HighlightComponent(this.computer, false);
        this.tutorialTxt.text = "You have successfully taken a backup! You can replace components in the system of the same type as you have backup of. Try it! Click the backup you just created and replace it with the highlighted computer in the system. Press NEXT when you are done";
        ChangeBtnState(this.nextBtn);
    }

    private void FifteenthTask() {
        Settings.Instance.ChangeConcedeAndNextLevel();
        ChangeBtnState(this.nextBtn);
        this.tutorialTxt.text = "You have successfully completed the tutorial. We encourage you to test different features out now to get familiar with the game mechanics. You can also upgrade, encrypt, repair and sell components that support this. To continue to the first level of the game open settings (the settings icon on top right) and click 'Next Level'.";
        UserBehaviourProfile.Instance.tutorialLvl = false;
        if (this.computer != null) {
            ChangeComponentClickableState(this.computer, true);
        }
        if (this.earth != null) {
            ChangeComponentClickableState(this.earth, true);
        }
        if (this.document != null) {
            ChangeComponentClickableState(this.document, true);
        }
        this.addBackupBtn.GetComponent<Button>().enabled = true;
        this.modifyPathBtn.GetComponent<Button>().enabled = true;
        this.addPathBtn.GetComponent<Button>().enabled = true;
    }

    /// <summary>
    /// Go to the next step of the tutorial
    /// </summary>
    private void NextStep() {
        if (this.state <= 15) {
            Debug.Log("Next step...");
            this.state++;
            StateMachine();
        }
    }

    /// <summary>
    /// Highlights the specified component if state == true and remove highlight if false
    /// </summary>
    /// <param name="objectToHighlight">Object to highlight</param>
    /// <param name="state">true to highlight, false to remove highlight</param>
    private void HighlightComponent(GameObject objectToHighlight, bool state) {
        GameObject borderGO;
        if (state) {
            borderGO = Instantiate(Defenses.CompController.Instance.highlightBorder as GameObject);
            borderGO.name = "HighlightBorder";
            borderGO.GetComponent<Image>().color = Color.green;
            borderGO.transform.position = objectToHighlight.transform.position;
            borderGO.transform.SetParent(objectToHighlight.transform);
            if (objectToHighlight == this.firewallSlot || objectToHighlight == this.backupSlot) {
                borderGO.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            } else if (objectToHighlight == this.computer || objectToHighlight == this.earth || objectToHighlight == this.document) {
                borderGO.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        } else {
            if (objectToHighlight.transform.Find("HighlightBorder") != null) {
                Destroy(objectToHighlight.transform.Find("HighlightBorder").gameObject);
            }
        }
    }

    /// <summary>
    /// Disables or enables the a components clickable state. If false, then the user
    /// can't interact with the specified component
    /// </summary>
    /// <param name="obj">object to modify clickable state</param>
    /// <param name="clickableState">true to make it clickable and false to make it non-interactable</param>
    private void ChangeComponentClickableState(GameObject obj, bool clickableState) {
        if (clickableState) {
            obj.GetComponent<Button>().enabled = true;
        } else {
            obj.GetComponent<Button>().enabled = false;
        }
    }

    /// <summary>
    /// Switches the enabled state of the selected button.
    /// If previous state was enabled then it will be disabled
    /// and vice versa,
    /// </summary>
    /// <param name="btnToChange">button to modify state</param>
    private void ChangeBtnState(Button btnToChange) {
        btnToChange.enabled = !btnToChange.enabled;
        if (btnToChange.enabled) {
            btnToChange.GetComponent<Image>().color = Color.green;
        } else {
            btnToChange.GetComponent<Image>().color = Color.grey;
        }
    }

    /// <summary>
    /// Shows or hides the tutorial panel depending on bool state
    /// </summary>
    /// <param name="state"></param>
    private void ShowTutorialPanel(bool state) {
        this.tutorialPanel.SetActive(state);
    }

}