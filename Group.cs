using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{

    public Sprite groupSpriteOff, groupSpriteOn;
    public List<Block> blocks;

    //upper/lower row boundaries in the matrix
    public int upperRow;
    public int lowerRow;
    //current row filled
    public int currentRow;




    public void initGroup(List<Block> blocks, Sprite groupSpriteOff, Sprite groupSpriteOn)
    {
        this.blocks = blocks;
        this.groupSpriteOff = groupSpriteOff;
        this.groupSpriteOn = groupSpriteOn;

        fillColor();
        setBoundaries();

    }


    private void fillColor()
    {
        foreach (Block block in blocks)
        {
            block.Filled = false;
        }
    }



    private void setBoundaries()
    {

        //calculate upper and 
        upperRow = blocks[0].row;
        lowerRow = blocks[0].row;


        for (int i = 1; i < blocks.Count; i++)
        {
            if (blocks[i].row > upperRow)
            {
                upperRow = blocks[i].row;
            }

            if (blocks[i].row < lowerRow)
            {
                lowerRow = blocks[i].row;
            }
        }
        currentRow = lowerRow - 1;
    }

    public void fill()
    {

        if (currentRow < upperRow)
        {

            //fill to next row
            currentRow++;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].row == currentRow)
                {
                    blocks[i].Filled = true;
                }
            }
        }
        else
        {
            currentRow = lowerRow - 1;
            //empty all
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Filled = false;
            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



}
