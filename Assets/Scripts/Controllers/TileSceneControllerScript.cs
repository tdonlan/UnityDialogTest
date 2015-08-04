using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

using Assets;
using UnityEngine.EventSystems;

public class TileSceneControllerScript : MonoBehaviour {

    public GameDataObject gameDataObject { get; set; }

    private GameObject pauseButtonPrefab;
    private GameObject pauseMenuPrefab;
    private RectTransform canvasRectTransform;

    public GameObject TreeInfoPanel;
    private RectTransform treeInfoPanelRectTransform;

    public GameObject tileMapPrefab;
    public GameObject tileMapObject;

    public GameObject tileSelectPrefab;
    public GameObject tileSelectSprite;

    public TileMapData tileMapData;

    public ZoneTree zoneTree { get; set; }
    private ZoneTreeNode currentNode;
    private long currentNodeIndex;


    public string debugTextString;
    public Text debugText;
    public Text debugText2;
    public Text panelText;
    public Button panelButton;

    public GameObject player;
    public PlayerControllerScript playerScript;

    public GameObject spritePrefab;
    public List<GameObject> boundingSpriteList = new List<UnityEngine.GameObject>();

    Camera mainCamera;

    Point mouseTilePoint;
    List<Point> movePath = new List<Point>();

    public float moveTimer;
    public float moveTime = .05f;



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
        loadTileMap();
        
        loadTileMapData();
        loadTileSprites();
        setPlayerStart();

        loadPauseMenu();

        //testing
        //TestDisplayTileArray();
    }

    private void loadTree()
    {
        //gameDataObject.treeStore.SelectTree(1);
        zoneTree = (ZoneTree)gameDataObject.treeStore.getCurrentTree();
    }

    private void initPrefabs()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>().GetComponent<Camera>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerControllerScript>();
        debugText = GameObject.FindGameObjectWithTag("debugText").GetComponent<Text>();
        debugText2 = GameObject.FindGameObjectWithTag("debugText2").GetComponent<Text>();
        panelText = GameObject.FindGameObjectWithTag("PanelText").GetComponent<Text>();
        panelButton = GameObject.FindGameObjectWithTag("PanelButton").GetComponent<Button>();
        
        
        tileSelectPrefab = Resources.Load<GameObject>("Prefabs/TileSelectPrefab");

        spritePrefab = Resources.Load<GameObject>("Prefabs/SpritePrefab");

        pauseButtonPrefab = Resources.Load<GameObject>("Prefabs/PauseButtonPrefab");
        pauseMenuPrefab = Resources.Load<GameObject>("Prefabs/PauseMenuPrefab");
        canvasRectTransform = GameObject.FindObjectOfType<Canvas>().GetComponent<RectTransform>();

        treeInfoPanelRectTransform = TreeInfoPanel.GetComponent<RectTransform>();
    }

    private void loadTileMap()
    {
        //get name of prefab of this map - match same name of tree?
        tileMapPrefab = Resources.Load<GameObject>(zoneTree.treeName);
        tileMapObject = (GameObject)Instantiate(tileMapPrefab);
        tileMapPrefab.tag = "tileMap";
    }

    private void loadTileMapData()
    {
        string outStr = "";
        //var tileMapObject = GameObject.FindGameObjectWithTag("tileMap");
        tileMapData = new TileMapData(tileMapObject);
        foreach (var b in tileMapData.collisionBoundsList)
        {
            outStr += b + "\n";
            displayBoundingRect(b);
        }
    }

    private void loadTileSprites()
    {
        for (int i = 0; i < tileMapData.objectBounds.Count; i++)
        {
            ZoneTreeNode node = (ZoneTreeNode)zoneTree.getNode(i+1);
            loadTileSprite(node.content.icon, tileMapData.objectBounds[i].center);
        }
    }

    private void loadTileSprite(string spriteName, Vector3 pos)
    {

        var spriteResource = Resources.Load<Sprite>("ZoneImage/"+spriteName);
        if(spriteResource != null){
              var spriteObject = Instantiate(spritePrefab);
              spriteObject.transform.position = pos;
            var spriteObjectSprite = spriteObject.GetComponent<SpriteRenderer>();
            spriteObjectSprite.sprite = spriteResource;
        }

    }


    //Added manually to scene - remove?
    private void loadPauseMenu()
    {
        GameObject pauseButton = Instantiate(pauseButtonPrefab);
        var pauseButtonRect = pauseButton.GetComponent<RectTransform>();
        pauseButtonRect.localPosition = new Vector3(0, 0, 0);
        pauseButtonRect.SetParent(canvasRectTransform);

        GameObject pauseMenu = Instantiate(pauseMenuPrefab);
        var pauseMenuRect = pauseMenu.GetComponent<RectTransform>();
        pauseButtonRect.localPosition = new Vector3(0, 1000, 0);
        pauseMenuRect.SetParent(canvasRectTransform);

    }

    //TESTING
    private void TestDisplayTileArray()
    {
        for (int y = 0; y < tileMapData.tileArray.GetLength(1); y++)
        {
            for (int x = 0; x < tileMapData.tileArray.GetLength(0); x++)
            {
                Tile t = tileMapData.tileArray[x,y];
                if (!t.empty)
                {
                    var pos = new Vector3(t.x * Tile.TILE_SIZE, -t.y * Tile.TILE_SIZE, -5);
                    var tileSprite = Instantiate(tileSelectPrefab);
                    tileSprite.transform.position = pos;
                }

            }
        }
    }

    private void setPlayerStart()
    {
        player.transform.position = tileMapData.getSpawnPoint((int)zoneTree.currentIndex-1).center;
    }
    
	
	// Update is called once per frame
	void Update () {
	    //check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                UpdateMouseClick();
            }
          
        }
        UpdateMove();

	}

    private void UpdateMove()
    {
        if (movePath.Count > 0)
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0)
            {
                Vector3 newPos = getWorldPosFromTilePoint(new Point(movePath[0].x, -movePath[0].y));
                playerScript.Move(newPos);
               // player.transform.position = new Vector3(newPos.x, newPos.y, player.transform.position.z);
                movePath.RemoveAt(0);
                moveTimer = moveTime;
            }
        }
    }

    private void UpdateMouseClick()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        mouseTilePoint = getTileLocationFromVectorPos(mouseWorldPosition);
        if (mouseTilePoint != null)
        {
            Bounds mouseTileBounds = getTileBounds(mouseTilePoint.x, mouseTilePoint.y);
            AddTileSelectSprite(getWorldPosFromTilePoint(mouseTilePoint));

            if (!(tileMapData.checkCollision(mouseTileBounds)))
            {
              
                Point playerPointPos = getTileLocationFromVectorPos(player.transform.position);
                movePath = tileMapData.getPath(playerPointPos.x , -playerPointPos.y , mouseTilePoint.x, -mouseTilePoint.y);
            }
        }
    }

    private void AddTileSelectSprite(Vector3 pos)
    {
        Destroy(tileSelectSprite);
        tileSelectSprite = Instantiate(tileSelectPrefab);
        tileSelectSprite.transform.position = new Vector3(pos.x,pos.y,-5);
        setDebugText(pos.ToString());
    }

    private Bounds getTileBounds(int x, int y)
    {
        Vector3 center = new UnityEngine.Vector3(x,y,0);
        Vector3 size = new UnityEngine.Vector3(Tile.TILE_SIZE,Tile.TILE_SIZE);
        Bounds b = new UnityEngine.Bounds(center, size);
        return b;
    }

    private Point getTileLocationFromVectorPos(Vector3 pos)
    {

        int x = Mathf.RoundToInt(pos.x / Tile.TILE_SIZE);
        int y = Mathf.RoundToInt(pos.y / Tile.TILE_SIZE);
        
        Point retval = null;

        if (x >= 0 && x <= tileMapData.tileArray.GetLength(0) && y <= 0 && y >= -tileMapData.tileArray.GetLength(1))
        {
            retval = new Point() { x = (int)x, y = (int)y };
        }
        return retval;
    }

    private Vector3 getWorldPosFromTilePoint(Point p)
    {
        return new Vector3(p.x * Tile.TILE_SIZE, p.y * Tile.TILE_SIZE, 0);
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
            currentNodeIndex = tileMapData.checkObjectCollision(playerBounds) + 1;
            if (currentNodeIndex > 0)
            {
                //check condition here
                //should use a helper in the Tree class, not drilling down into dictionary/TryGetValue method
                if (zoneTree.treeNodeDictionary.TryGetValue(currentNodeIndex, out currentNode))
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
        zoneTree.SelectNode(currentNodeIndex);
        switch (currentNode.content.nodeType)
        {
            case ZoneNodeType.Link:
                ClickLinkNode(currentNode.content.linkIndex);
                break;
            case ZoneNodeType.Dialog:
                ClickDialogNode(currentNode.content.linkIndex);
                break;
            case ZoneNodeType.Battle:
                ClickBattleNode(currentNode.content.linkIndex);
                break; 
            case ZoneNodeType.Info:
                ClickInfoNode(currentNode.content.linkIndex);
                break;
            default:
                break;
                     
        }
    }

    private void ClickInfoNode(long linkIndex)
    {
        //update the TreeInfoPanel
        InfoTree curInfoTree = (InfoTree)gameDataObject.treeStore.getTree(linkIndex);

        TreeInfoControllerScript treeInfoScript = TreeInfoPanel.GetComponent<TreeInfoControllerScript>();
        treeInfoScript.UpdateInfo(gameDataObject, curInfoTree);


        treeInfoPanelRectTransform.localPosition = new UnityEngine.Vector3(0, 0, 0);
    }

    private void ClickLinkNode(long linkIndex)
    {
        gameDataObject.treeStore.SelectTree(linkIndex);
        if (gameDataObject.treeStore.getCurrentTree() is WorldTree)
        {
            Application.LoadLevel(1);
        }
        else
        {
            Application.LoadLevel(4);
        }
    }

    private void ClickDialogNode(long dialogIndex)
    {
        Application.LoadLevel(3);
    }

    private void ClickBattleNode(long battleIndex)
    {
        Application.LoadLevel(5);
    }



}
