using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{

    static Color matchColor = Color.green;
    static Color unmatchColor = Color.red;

    private TextMesh textMesh;


    public int value;
    public int position;
    public XMLHandler.XMLMATCHTYPE type;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }


    public void initMatch(int position, int value, XMLHandler.XMLMATCHTYPE type)
    {

        this.value = value;
        this.position = position;
        this.type = type;

        this.spriteRenderer.color = unmatchColor;
    }

    public void updateMatch(Block[,] blocks)
    {

        int count = 0;
        if (type == XMLHandler.XMLMATCHTYPE.R)
        {
            for (int c = 0; c < Level.instance.Sides; c++)
            {
                if ((blocks[position, c].filled == true))
                {
                    count++;
                }
            }

        }
        else
        {

            for (int r = 0; r < Level.instance.Sides; r++)
            {
                if ((blocks[r, position].filled == true))
                {
                    count++;
                }
            }

        }

        setMatch(count == value);
    }


    public void setMatch(bool matched)
    {
        if (matched)
        {
            this.spriteRenderer.color = matchColor;
        }
        else
        {
            this.spriteRenderer.color = unmatchColor;
        }
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
