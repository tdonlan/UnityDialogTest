using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

using Assets;

public class TileSceneControllerScript : MonoBehaviour {

    public GameDataObject gameDataObject { get; set; }
    public ZoneTree zoneTree { get; set; }
    private ZoneTreeNode currentNode; 

    public TileMapData tileMapData;

    public string debugTextString;
    public Text debugText;
    public Text debugText2;
    public Text panelText;
    public Button panelButton;

    public GameObject player;

    public GameObject spritePrefab;
    public List<GameObject> boundingSpriteList = new List<UnityEngine.GameObject>();

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
        loadTileMapData();
        setPlayerStart();
    }

    private void loadTree()
    {
        gameDataObject.treeStore.SelectTree(1);
        zoneTree = (ZoneTree)gameDataObject.treeStore.getCurrentTree();
       
    }

    private void initPrefabs()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        debugText = GameObject.FindGameObjectWithTag("debugText").GetComponent<Text>();
        debugText2 = GameObject.FindGameObjectWithTag("debugText2").GetComponent<Text>();
        panelText = GameObject.FindGameObjectWithTag("PanelText").GetComponent<Text>();
        panelButton = GameObject.FindGameObjectWithTag("PanelButton").GetComponent<Button>();

        spritePrefab = Resources.Load<GameObject>("Prefabs/SpritePrefab");
    }

    private void loadTileMapData()
    {
        string outStr = "";
        var tileMapObject = GameObject.FindGameObjectWithTag("tileMap");
        tileMapData = new TileMapData(tileMapObject);
        foreach (var b in tileMapData.collisionBoundsList)
        {
            outStr += b + "\n";
            displayBoundingRect(b);
        }
        setDebugText2(outStr);
    }

    private void setPlayerStart()
    {
        player.transform.position = tileMapData.spawnBounds.center;
    }
    
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setDebugText2(string text)
    {
        this.debugText2.text = text;
    }

    public void setDebugText(string text)
    {
        this.debugText.text = text;
    }

    //display a sprite rectangle where bounds are
    //testing
    public void displayBoundingRect(Bounds b)
    {
        clearBoundingRect();
        GameObject bSprite = Instantiate(spritePrefab);
        bSprite.transform.position = b.center;
        var sprite = bSprite.GetComponent<Sprite>();
        var spriteRenderer = bSprite.GetComponent<SpriteRenderer>();
        

        boundingSpriteList.Add(bSprite);
       
    }

    private void clearBoundingRect()
    {
        foreach(var b in boundingSpriteList)
        {
            Destroy(b);
        }
        boundingSpriteList.Clear();
    }


    public void checkPlayerObjectCollision(Bounds playerBounds)
    {
        if (tileMapData != null)
        {
            int objectIndex = tileMapData.checkObjectCollision(playerBounds);
            if(objectIndex >= 0)
            {
                //check condition here
                if (zoneTree.treeNodeDictionary.TryGetValue(objectIndex + 1, out currentNode))
                {
                    panelText.text = currentNode.content.nodeName + " " + currentNode.content.description;
                    panelButton.enabled = true;
                }
            }
            else
            {
                panelText.text = "";
                panelButton.enabled = false;
            }

        }
       
    }

    
    public void ZoneNodeButtonClick()
    {
        switch (currentNode.content.nodeType)
        {
            case ZoneNodeType.Link:
                ClickLinkNode(currentNode.content.linkIndex);
                break;
            case ZoneNodeType.Dialog:
                ClickDialogNode(currentNode.content.linkIndex);
                break;
            case ZoneNodeType.Battle:
                break; 
            case ZoneNodeType.Info:
                break;
            default:
                break;
                     
        }
    }

    private void ClickLinkNode(long linkIndex)
    {
        Application.LoadLevel(1);
    }

    private void ClickDialogNode(long dialogIndex)
    {
        Application.LoadLevel(3);
    }


}
