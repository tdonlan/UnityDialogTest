﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ZoneControllerScript : MonoBehaviour {

    public GameDataObject gameDataObject { get; set; } 

    public TreeStore treeStore { get; set; }
    public ZoneTree zoneTree { get; set; }

    public GameObject zoneNodeButtonPrefab { get; set; }
    public GameObject zoneName { get; set; }
    public GameObject zoneDescription { get; set; }
    public GameObject zoneIcon { get; set; }
    public GameObject zoneButton { get; set; }


    public Canvas uiCanvas { get; set; }

    public List<GameObject> zoneNodeList = new List<GameObject>();

	// Use this for initialization
	void Start () {
       
	}

    void OnLevelWasLoaded(int level)
    {
        loadGameData();
        initScene();
    }

    private void loadGameData()
    {
        gameDataObject = GameObject.FindObjectOfType<GameDataObject>();
    }

    private void initScene()
    {
        initPrefabs();
        loadTree();
        setZoneNodes();
    }


    private void loadTree()
    {
        gameDataObject.treeStore.SelectTree(1);
        zoneTree = (ZoneTree)gameDataObject.treeStore.getCurrentTree();

        /*(
        TextAsset zoneTextAsset = Resources.Load<TextAsset>("SimpleWorld1/ZoneTown");
        zoneTree = (ZoneTree)SimpleTreeParser.getTreeFromString(zoneTextAsset.text, TreeType.Zone, new GlobalFlags());
        */
    }


    private void initPrefabs()
    {
        uiCanvas = (Canvas)GameObject.FindObjectOfType<Canvas>();
        zoneNodeButtonPrefab = (GameObject)Resources.Load<GameObject>("Prefabs/ZoneNodeButtonPrefab");

        zoneName = GameObject.FindGameObjectWithTag("ZoneName");
        zoneDescription = GameObject.FindGameObjectWithTag("ZoneDescription");
        zoneIcon = GameObject.FindGameObjectWithTag("ZoneIcon");
        zoneButton = GameObject.FindGameObjectWithTag("ZoneButton");
    }

    private void setZoneNodes()
    {
        clearZoneNodes();


        ZoneTreeNode currentNode = (ZoneTreeNode)zoneTree.getNode(zoneTree.currentIndex);

        var zoneNode = createZoneNodeIcon(currentNode);
        zoneNodeList.Add(zoneNode);

        foreach (var branch in currentNode.getBranchList(zoneTree))
        {
            zoneNode = createZoneNodeIcon((ZoneTreeNode)zoneTree.getNode(branch.linkIndex));
            zoneNodeList.Add(zoneNode);
        }
    }

    private void clearZoneNodes()
    {
        foreach (var node in zoneNodeList)
        {
            Destroy(node);
        }
        zoneNodeList.Clear();
    }

    private GameObject createZoneNodeIcon(ZoneTreeNode node)
    {
        GameObject zoneNodeIcon = (GameObject)Instantiate(zoneNodeButtonPrefab);
        zoneNodeIcon.transform.SetParent(uiCanvas.transform, false);

        var rect = zoneNodeIcon.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(node.content.x, node.content.y);

        var worldNodeButtonButt = zoneNodeIcon.GetComponent<Button>();
        worldNodeButtonButt.onClick.AddListener(() => clickZoneNodeIcon(node.index));

        var img = zoneNodeIcon.GetComponent<Image>();
        var sprite = Resources.Load<Sprite>("ZoneImage/" + node.content.icon);
        img.sprite = sprite;

        var text = zoneNodeIcon.GetComponentInChildren<Text>();
        text.text = node.content.nodeName;

        return zoneNodeIcon;

    }

    //click the zone node to view details
    public void clickZoneNodeIcon(long index)
    {
        zoneTree.SelectNode(index);

        ZoneTreeNode clickedNode = (ZoneTreeNode)zoneTree.getNode(index);

        var text = zoneName.GetComponent<Text>();
        text.text = clickedNode.content.nodeName;

        var detail = zoneDescription.GetComponent<Text>();
        detail.text = clickedNode.content.description;

        var butt = zoneButton.GetComponent<Button>();
        butt.onClick.AddListener(() => clickZoneNodeButton(clickedNode.index));

        var img = zoneIcon.GetComponent<Image>();
        var sprite = Resources.Load<Sprite>("ZoneImage/" + clickedNode.content.icon);
        img.sprite = sprite;

        setZoneNodes();
    }

    //enter the zone node to change scenes. also need to know the type (world link, battle, etc)
    public void clickZoneNodeButton(long nodeIndex)
    {
        ZoneTreeNode clickedNode = (ZoneTreeNode)zoneTree.getNode(nodeIndex);

        if (clickedNode.content.nodeType == ZoneNodeType.Link)
        {
            Application.LoadLevel(1);
        }
        if(clickedNode.content.nodeType == ZoneNodeType.Dialog)
        {
            Application.LoadLevel(3);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}