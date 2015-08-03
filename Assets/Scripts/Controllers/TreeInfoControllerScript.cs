using UnityEngine;
using System.Collections;

public class TreeInfoControllerScript : MonoBehaviour {

    public GameObject TreeInfoPanel;
    private RectTransform panelRectTransform;

	// Use this for initialization
	void Start () {
        initRefs();
	}

    private void initRefs()
    {
        panelRectTransform = TreeInfoPanel.GetComponent<RectTransform>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void ClosePanel()
    {
        panelRectTransform.localPosition = new Vector3(2000, 2000, 0);
    }
}
