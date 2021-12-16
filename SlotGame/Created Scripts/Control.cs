using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Core Scene Control
/// Handles Tile Selecting, Character Loading, Turn States
/// </summary>
public class Control : MonoBehaviour
{
    [SerializeField] int turnState = 0;

    [SerializeField] int playerPosition = 1;
    [SerializeField] GameObject playerObjectPrefab; 
    GameObject playerObject;
    
    
    public PlayerTurn playerTurn;
    public TilePool tilePoolPrefab;
    public Deck deck;
    public GameObject GameEndSplash;

    //Enemies
    public EnemyLoader enemyLoader;
    public List<EnemyControl> activeEnemies;

    //Energy
    public int maxEnergy = 3;
    public int currentEnergy;
    int displayedEnergy;
    public GameObject energyGUI;

    public AdjacencyCheck adjCheck;
    public GameObject tileOptionScreen;
    public bool fightRunning = false;

    [SerializeField] GameObject nextLevelScreen;

    Tile m_selectedTile;

    public bool tilesSelectable = false; //Whether or not tiles can be clicked
    public Tile selectedTile
    {
        get{return m_selectedTile;}
        set{m_selectedTile = value;}
    }

    /// <summary>
    /// Reset deck and bring up next level screen
    /// </summary>
    public void LoadRound()
    {
        deck.SetUp();
        Instantiate(nextLevelScreen);
    }
    /// <summary>
    /// Sets selected tile
    /// Runs call to turn on selected display
    /// </summary>
    public void SelectTile(Tile tile)
    {
        DeSelectTile();
        selectedTile = tile;
        adjCheck.RunCheck(selectedTile);
        ToggleSelectedDisplay(true, tile);
    }

    /// <summary>
    /// Removes currently selected tile
    /// Runs call to turn off selected display
    /// </summary>
    public void DeSelectTile()
    {
        if (m_selectedTile) 
        {
            ToggleSelectedDisplay(false, selectedTile);
        }
        selectedTile = null;
    }

    /// <summary>
    /// Call to end game
    /// </summary>
    /// <param name="won">Whether game was won or lost</param>
    public void EndGame(bool won)
    {
        GameObject splash = Instantiate(GameEndSplash);
        splash.transform.SetParent(FindObjectOfType<Canvas>().transform);
        splash.transform.localPosition = new Vector3(0, 0, 0);
        splash.GetComponentInChildren<TextMeshPro>().text = won == true ? "YOU WIN" : "YOU LOSE";
        splash.GetComponent<Button>().onClick.AddListener(LoadMenu);
    }

    /// <summary>
    /// Call to kill a character aand remove from scene properly
    /// </summary>
    /// <param name="character">CharacterScript of the character to be killed</param>
    public void KillCharacter(CharacterScript character)
    {
        Debug.Log("Killing " + character);
        if (!character.player)
        {
            activeEnemies.Remove(character.GetComponent<EnemyControl>());
        }
        Destroy(character.gameObject);
    }

    /// <summary>
    /// Call to load the load scene menu
    /// </summary>
    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    ///Run initializion on awake
    /// </summary>
    void Awake()
    {
        playerTurn = FindObjectOfType<PlayerTurn>();
        energyGUI = GameObject.Find("EnergyGUI");
        if (playerTurn == null) Debug.Log("ERROR: playerTurn object not found");
        deck = GetComponent<Deck>();
        deck.Initialize();
    }

    /// <summary>
    /// Load player and run turn on start
    /// </summary>
    void Start()
    {
        LoadPlayer();
        ExecuteTurn();
    }

    /// <summary>
    /// Instantiate, name, and position player
    /// </summary>
    void LoadPlayer()
    {
        playerObject = Instantiate(playerObjectPrefab);
        playerObject.transform.parent = GameObject.Find("Position: Player").transform;
        playerObject.transform.localPosition = Vector3.zero;
        playerObject.name = "Player";
    }

    /// <summary>
    /// Runs turns and updates GUI on update
    /// </summary>
    void Update()
    {
        if(!playerTurn.running)
        {
            ExecuteTurn();
            NextTurnState();
        }


        //Update EnergyGUI
        if (currentEnergy != displayedEnergy)
        {
            energyGUI.GetComponent<TextMeshProUGUI>().text = currentEnergy + "/"  + maxEnergy;
            displayedEnergy = currentEnergy;
        }

        if (activeEnemies.Count == 0 && fightRunning)
        {
            fightRunning = false;
            CreateTileOptionScreen();
        }
    }

    /// <summary>
    /// Executes turn base on turn state
    /// </summary>
    void ExecuteTurn()
    {
        switch(turnState)
        {
            case 0:
                foreach (EnemyControl enemy in FindObjectsOfType<EnemyControl>())
                {
                    enemy.DeclareAttack();
                }
                break;
            case 1:
                currentEnergy = maxEnergy;
                playerTurn.RunTurn();
                break;
            case 2:
                foreach (EnemyControl enemy in FindObjectsOfType<EnemyControl>())
                {
                    enemy.RunTurn();
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Increases turn state looping at max increment
    /// </summary>
    void NextTurnState()
    {
        turnState++;
        if(turnState>2) turnState=0;
    }

    /// <summary>
    /// Enables change on tile to show selected
    /// </summary>
    void ToggleSelectedDisplay(bool state, Tile tile)
    {

    }

    /// <summary>
    /// Instantiates TileOptionScreen
    /// </summary>
    void CreateTileOptionScreen()
    {
        Debug.Log("Spawning tile option screen");
        if (FindObjectOfType<TileOption>() == null && !fightRunning) Instantiate(tileOptionScreen);
    }
}
