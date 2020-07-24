using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region variables

    //game play
    public bool gameIsPlaying;
    public int levelNum = 1;

    //xml handler
    public XMLHandler xmlHandler;

    //sprites
    public List<Sprite> sprites;

    //singleton
    public static GameManager instance = null;

    void makeSingleton()
    {
        Debug.Log("game manager instance: ");
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    #endregion

    void Awake()
    {
        Debug.Log("Game Manager awake: ");
        makeSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {

        xmlHandler = GetComponent<XMLHandler>();
        setSprites();
        gameIsPlaying = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void processMouseBlockClick(Block block)
    {
        if (gameIsPlaying)
        {
            Level.instance.processBlockClick(block);
        }
    }

    private void setSprites()
    {
        Object[] gos = Resources.LoadAll("blocks", typeof(Sprite));
        sprites = new List<Sprite>();
        foreach (Object go in gos)
        {
            sprites.Add((Sprite)go);
        }
    }
}
