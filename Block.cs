using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public int row, col;
    public bool filled;
    public SpriteRenderer spriteRenderer;

    public Group group;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void initBlock(int row, int col)
    {
        this.row = row;
        this.col = col;
        this.filled = false;
    }


    void OnMouseUp()
    {
        Debug.Log("clicked block: " + row + " " + col + " filled: " + filled);
        GameManager.instance.processMouseBlockClick(this);
    }


    private void fillBlock()
    {
        if (filled)
        {
            spriteRenderer.sprite = group.groupSpriteOn;
        }
        else
        {
            spriteRenderer.sprite = group.groupSpriteOff;
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


    public bool Filled
    {
        get { return filled; }
        set
        {
            filled = value;
            fillBlock();
        }
    }

    public Group Group
    {
        get { return group; }
        set { group = value; }
    }
}
