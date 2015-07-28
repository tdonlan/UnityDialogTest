using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour {

    public GameDataObject gameDataObject;
    public RectTransform panelRectTransform;
    public Text ZoneInfoText;
    public Text GlobalFlagsText;

	// Use this for initialization
	void Start () {
        initRefs();
	}

    private void initRefs()
    {
        gameDataObject = GameObject.FindObjectOfType<GameDataObject>();

        ZoneInfoText = GameObject.FindGameObjectWithTag("PauseZoneInfo").GetComponent<Text>();
        GlobalFlagsText = GameObject.FindGameObjectWithTag("PauseGlobalFlagInfo").GetComponent<Text>();

        panelRectTransform = gameObject.GetComponent<RectTransform>();

    }

	// Update is called once per frame
	void Update () {
        UpdateData();
	}

    public void UpdateData()
    {
        ZoneInfoText.text = getZoneInfo();
        GlobalFlagsText.text = getGlobalFlags();
    }

    public void CloseMenu()
    {
        panelRectTransform.localPosition = new Vector3(0, 1000, 0);
    }

    private string getZoneInfo()
    {
        string zoneInfo = "Zone Info: ";
        zoneInfo += gameDataObject.treeStore.getCurrentTree().treeName;
        zoneInfo += gameDataObject.treeStore.getCurrentTree().treeType.ToString();
        return zoneInfo;
    }

    private string getGlobalFlags()
    {
        string gfString = "";
        foreach (var flag in gameDataObject.treeStore.globalFlags.globalFlagList)
        {
            gfString += string.Format("{0} : {1} ({2})", flag.name, flag.value, flag.flagType.ToString());
        }
        return gfString;
    }
}
