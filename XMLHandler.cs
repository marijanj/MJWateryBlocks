using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;


public class XMLHandler : MonoBehaviour
{

    #region varibles

    public enum XMLMATCHTYPE { R, C }

    private XmlDocument xmlDoc;
    private TextAsset xmlRawFile;
    private string fileName;

    public List<XMLLevel> levels;

    #endregion


    #region startup


    void Awake()
    {
        fileName = @"Assets/Scripts/LevelData.xml";
    }

    void Start()
    {
        loadXMLFromAssets();
        parseXMLToGames();
    }

    private void loadXMLFromAssets()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(System.IO.File.ReadAllText(fileName));
    }


    #endregion

    #region gameplay

    public XMLLevel getLevelData(int levelNum)
    {
        return levels[levelNum];
    }

    #endregion gameplay



    #region parse
    private void parseXMLToGames()
    {

        levels = new List<XMLLevel>();

        //process groups
       
        foreach (XmlElement node in xmlDoc.SelectNodes("//Games/Game"))
        {

           
            int sides = int.Parse(node.SelectSingleNode("sides").InnerText);

            XmlNodeList xmlGroups = node.SelectNodes("Group");
            XMLGroup[] groups = new XMLGroup[xmlGroups.Count];
            int index = 0;
            foreach (XmlElement xmlgroup in xmlGroups)
            {
                Debug.Log("game: " + index);
                string[] g = xmlgroup.InnerText.Split(',');
                XMLPosition[] positions = new XMLPosition[g.Length];
                for (int i = 0; i < g.Length; i++)
                {

                    string[] v = g[i].Split(' ');
                    positions[i] = new XMLPosition(int.Parse(v[0]), int.Parse(v[1]));

                }
                groups[index] = new XMLGroup(positions);
                index++;
            }

          

            //process the matches

            XmlNode xmlMatch = node.SelectSingleNode("Match");
            string[] m = xmlMatch.InnerText.Split(',');
            XMLMatch[] matches = new XMLMatch[m.Length];
            for (int i = 0; i < matches.Length; i++)
            {
                string[] v = m[i].Split(' ');
                int pos = int.Parse(v[1]);
                int value = int.Parse(v[2]);

                XMLMATCHTYPE type = convert(v[0]);
                matches[i] = new XMLMatch(pos, value, type);

            }

            levels.Add(new XMLLevel(sides, groups, matches));
        }
    }

    #endregion

    // Update is called once per frame
    void Update()
    {

    }

    #region inner classes

    public class XMLLevel
    {
        public int sides;
        public XMLGroup[] groups;
        public XMLMatch[] matches;


        public XMLLevel(int sides, XMLGroup[] groups, XMLMatch[] matches)
        {
            this.sides = sides;
            this.groups = groups;
            this.matches = matches;
        }
    }

    public class XMLGroup
    {
        public XMLPosition[] positions;

        public XMLGroup(XMLPosition[] positions)
        {
            this.positions = positions;
        }
    }

    public class XMLPosition
    {
        public int row, col;

        public XMLPosition(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    public class XMLMatch
    {
        public int pos, value;
        public XMLMATCHTYPE type;

        public XMLMatch(int pos, int value, XMLMATCHTYPE type)
        {
            this.pos = pos;
            this.value = value;
            this.type = type;
        }
    }



    private XMLMATCHTYPE convert(string match)
    {
        XMLMATCHTYPE type;
        if (match.Equals("R"))
        {
            type = XMLMATCHTYPE.R;
        }
        else
        {
            type = XMLMATCHTYPE.C;
        }
        return type;
    }




    #endregion
}
