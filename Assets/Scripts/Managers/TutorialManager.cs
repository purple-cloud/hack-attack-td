using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    #region SERIALIZED FIELDS

    [Header("Tutorial Panel")]

    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private Text tutorialTxt;

    [SerializeField]
    private Button nextBtn;

    [SerializeField]
    private Button prevBtn;

    [Header("Level Information Panel")]

    [SerializeField]
    private Button tutorialInfoBtn;

    [Header("Restricted Access")]

    [SerializeField]
    private GameObject modifyPathBtn;

    [SerializeField]
    private GameObject addPathBtn;

    [SerializeField]
    private GameObject firewallSlot;

    [SerializeField]
    private GameObject backupSlot;

    #endregion

    #region VARIABLES

    private bool readyToMoveOn;

    private int state;

    #endregion

    private void Start() {
        this.readyToMoveOn = false;
        this.state = 0;
        this.nextBtn.onClick.AddListener(NextStep);
        this.prevBtn.onClick.AddListener(PrevStep);
        this.tutorialInfoBtn.onClick.AddListener(TutorialInit);
        GameManager.Instance.SetCurrency(100000);

        // Disable all access to restricted buttons
        this.modifyPathBtn.SetActive(false);
        this.addPathBtn.SetActive(false);
        this.firewallSlot.SetActive(false);
        this.backupSlot.SetActive(false);
    }

    private void StateMachine() {
        switch (state) {
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

            default:
                Debug.Log("Couldn't find next or previous stage");
                break;
        }
    }

    private void TutorialInit() {
        ShowTutorialPanel(true);
        this.tutorialTxt.text = "For starters we can see we have a computer, a document and the internet. All these are connected together via inputs and outputs";
    }

    private void FirstTask() {
        this.firewallSlot.SetActive(true);
        HighlightComponent(this.firewallSlot, true);
        this.tutorialTxt.text = "To extend your system with another component, simply choose what component from the actionbar you would like to place and left click one of the highlighted components you want to extend from, and then click anywhere on the map. (You can cancel placement anytime by pressing the ESC key) Press Next to continue.";
    }

    private void SecondTask() {
        this.tutorialTxt.text = "You might want to place a firewall between your computer and the internet to protect yourself. To do this, choose firewall from actionbar. RIGHT CLICK computer and then RIGHT CLICK internet. After this you can then choose where you want to place the firewall in the map. Press Next to continue.";
    }

    private void ThirdTask() {
        HighlightComponent(this.firewallSlot, false);
        this.modifyPathBtn.SetActive(true);
        HighlightComponent(this.modifyPathBtn, true);
        this.tutorialTxt.text = "Now we have two connections from the internet to our computer. This isn't really optimal. To remove our direct connection from the internet to our computer first click the highlighted button in the top right corner...";
        this.readyToMoveOn = true;
        ChangeBtnState(this.nextBtn);
    }

    private void FourthTask() {
        HighlightComponent(this.modifyPathBtn, false);
        ChangeBtnState(this.nextBtn);
        this.readyToMoveOn = false;
        this.tutorialTxt.text = "Now click on the highlighted internet and find the connection that goes from the internet to the computer and delete it. This can be done by pressing the x symbol to the right of the connection (You can cancel modification anytime by pressing the ESC key). Press Next to continue.";
    }

    private void FifthTask() {
        this.tutorialTxt.text = "Our system is now a lot more secure. Our firewall can be configured by clicking it and choosing to enable traffic through our different ports or disable the traffic. Press Next to continue.";
    }

    private void SixthTask() {
        this.addPathBtn.SetActive(true);
        this.tutorialTxt.text = "Another thing which is nice to be aware of is that you can create your own connections between existing components. This can be done by clicking the highlighed button in the top right corner...";
        HighlightComponent(this.addPathBtn, true);
        this.readyToMoveOn = true;
        ChangeBtnState(this.nextBtn);
    }

    private void SeventhTask() {
        HighlightComponent(this.addPathBtn, false);
        ChangeBtnState(this.nextBtn);
        this.readyToMoveOn = false;
        this.tutorialTxt.text = "Now you can click on the two components you want to connect. (If you already have deleted the connection from the internet to the computer you can establish a new connection there. After that you can delete it again) (You can cancel path connecting anytime by pressing the ESC key). Press Next to continue.";
    }

    private void EightTask() {
        this.tutorialTxt.text = "Now you have come so far it's time to take backup. You can take backup of each individual component in the system. By taking backup you store all its states and functionality. Press Next to start.";
    }

    private void NinthTask() {
        this.backupSlot.SetActive(true);
        HighlightComponent(this.backupSlot, true);
        this.tutorialTxt.text = "To take backup, first click on the backup slot in the actionbar. Then select 'Backup Component', and click the component you would like to take backup of (You can cancel backup by pressing the ESC key). Press Next to continue.";
    }

    private void TenthTask() {
        HighlightComponent(this.backupSlot, false);
        this.tutorialTxt.text = "If you took a backup you will see it in a panel that pops up. This is your backup management panel. Here you can store up to 6 individual backups. If you choose to add one more after having 6, the oldest backup will be deleted. Choose wisely which components are important. Press Next to continue.";
    }

    private void EleventhTask() {
        this.tutorialTxt.text = "To restore a backup, simply click the backup you want and then select where to place it. You can only replace components of the same type as the backup itself. Press Next to continue.";
    }

    private void TwelfthTask() {
        this.tutorialTxt.text = "You are now ready to start defending yourself against real attacks. You can access the settings menu in the top right corner. Click Next level when you are ready. We recommend that you try and click around in the interface before jumping onwards.";
        ChangeBtnState(this.nextBtn);
        Settings.Instance.ChangeConcedeAndNextLevel();
    }

    private void NextStep() {
        if (this.state <= 12) {
            Debug.Log("Next step...");
            this.state++;
            StateMachine();
        }
    }

    public void NextStepOnAction() {
        if (this.readyToMoveOn) {
            Debug.Log("Next step...");
            this.state++;
            StateMachine();
        }
    }

    private void PrevStep() {
        if (this.state > 0) {
            Debug.Log("Previous step...");
            this.state--;
            StateMachine();
        }
    }

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
            }
        } else {
            if (objectToHighlight.transform.Find("HighlightBorder") != null) {
                Destroy(objectToHighlight.transform.Find("HighlightBorder").gameObject);
            }
        }
    }

    private void ChangeBtnState(Button btnToChange) {
        btnToChange.enabled = !btnToChange.enabled;
        if (btnToChange.enabled) {
            btnToChange.GetComponent<Image>().color = Color.green;
        } else {
            btnToChange.GetComponent<Image>().color = Color.grey;
        }
    }

    private void ShowTutorialPanel(bool state) {
        this.tutorialPanel.SetActive(state);
    }

}