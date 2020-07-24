using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    #region variables


    public GameObject blockPrefab;
    public GameObject matchPrefab;


    private float xoffset = -2.3f;
    private float yoffset = -1.5f;
    private float gap = .1f;
    private float scale = .55f;



    public static Level instance = null;
    private int levelNum;
    private int sides;
    private Block[,] blocks;
    private List<Group> groups;
    private Match[] matches;

    void MakeSingleton()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion



    void Awake()
    {
        MakeSingleton();
    }


    void Start()
    {
        levelNum = GameManager.instance.levelNum;
        XMLHandler.XMLLevel xmlLevel = GameManager.instance.xmlHandler.getLevelData(levelNum);
        sides = xmlLevel.sides;

        initializeBlocks(xmlLevel);
        initializeGroups(xmlLevel);
        initializeMatches(xmlLevel);

        GameManager.instance.gameIsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region gameplay
    public void processBlockClick(Block selectedBlock)
    {
        selectedBlock.group.fill();
        updateMatches();
    }


    private void updateMatches()
    {
        for (int i = 0; i < matches.Length; i++)
        {
            matches[i].updateMatch(blocks);
        }
    }



    #endregion


    private void initializeBlocks(XMLHandler.XMLLevel xmlLevel)
    {

        //offsets from center
        xoffset = -(sides * (scale + gap) / 2f - scale);
        yoffset = -(sides * (scale + gap) / 2f - .5f);

        blocks = new Block[sides, sides];

        for (int r = 0; r < blocks.GetLength(0); r++)
        {
            for (int c = 0; c < blocks.GetLength(1); c++)
            {

                Vector2 pos = new Vector2(xoffset + c * (scale + gap), yoffset + r * (scale + gap));
                GameObject go = Instantiate(blockPrefab, pos, Quaternion.identity) as GameObject;
                go.transform.localScale = new Vector3(scale, scale, scale);
                go.name = r + " " + c;


                Block block = go.GetComponent<Block>();
                block.initBlock(r, c);
                blocks[r, c] = block;
            }
        }
    }

    private void initializeGroups(XMLHandler.XMLLevel xmlLevel)
    {
        //empty to hold all the groups
        GameObject groupsHolder = new GameObject("groups");

        groups = new List<Group>();
        XMLHandler.XMLGroup[] xmlgroups = xmlLevel.groups;

        int colorindex = 0;

        foreach (XMLHandler.XMLGroup xmlGroup in xmlgroups)
        {

            GameObject groupObject = new GameObject("group " + colorindex);
            groupObject.transform.parent = groupsHolder.transform;

            Group group = groupObject.AddComponent<Group>();
            List<Block> groupBlocks = new List<Block>();

            for (int i = 0; i < xmlGroup.positions.Length; i++)
            {

                //get the block belonging to this group
                int row = xmlGroup.positions[i].row;
                int col = xmlGroup.positions[i].col;
                Block block = blocks[row, col];


                //set the block's group
                block.Group = group;
                //update block game object parent
                block.gameObject.transform.parent = groupObject.transform;

                //add block to list
                groupBlocks.Add(block);

            }

            //init group
            Sprite groupSpriteOff = GameManager.instance.sprites[colorindex];
            Sprite groupSpriteOn = GameManager.instance.sprites[colorindex + 1];

            group.initGroup(groupBlocks, groupSpriteOff, groupSpriteOn);

            groups.Add(group);
            colorindex += 2;
        }
    }

    private void initializeMatches(XMLHandler.XMLLevel xmlLevel)
    {

        GameObject matchesHolder = new GameObject("matches");

        XMLHandler.XMLMatch[] xmlMatches = xmlLevel.matches;
        matches = new Match[xmlMatches.Length];

        for (int i = 0; i < matches.Length; i++)
        {
            matches[i] = makeMatchObject(xmlMatches[i], matchesHolder, i, xmlLevel);
        }
    }

    private Match makeMatchObject(XMLHandler.XMLMatch xmlMatch, GameObject parent, int colorindex, XMLHandler.XMLLevel xmlLevel)
    {

        int pos = xmlMatch.pos;
        int value = xmlMatch.value;
        XMLHandler.XMLMATCHTYPE type = xmlMatch.type;
        Vector2 v;
        if (type == XMLHandler.XMLMATCHTYPE.R)
        {
            v = new Vector2(xoffset + gap + xmlLevel.sides * (scale + gap) - .2f, yoffset + pos * (scale + gap) + .2f);
        }
        else
        {
            v = new Vector2(xoffset + pos * (scale + gap) - 0.2f, yoffset - scale - 2 * gap + .3f);

        }

        GameObject go = Instantiate(matchPrefab, v, Quaternion.identity) as GameObject;
        go.transform.localScale = new Vector3(scale, scale, scale);
        go.name = type + " " + value;

        go.GetComponent<TextMesh>().text = value.ToString();
        go.transform.parent = parent.transform;

        Match match = go.transform.GetChild(0).GetComponent<Match>();
        match.initMatch(pos, value, type);
        return match;
    }



    public int Sides
    {
        get { return sides; }
        set { sides = value; }
    }



}


