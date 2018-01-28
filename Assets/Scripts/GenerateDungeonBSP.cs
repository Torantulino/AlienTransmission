using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.ProcGenHelpers;

public class GenerateDungeonBSP : MonoBehaviour
{





    public int iterations = 3;
    public float pivotMin = 0.45f;
    public float pivotMax = 0.55f;

    public int dimension = 96;

    public bool[,] tiles;

    public GameObject pillar;

    public GameObject alien;

    public int minAlienTries = 30;

    public int maxAlienTries = 100;



    public GameObject alpha;
    public GameObject bravo;
    public GameObject charlie;
    public GameObject delta;

    public int maxAlienSpawnSpacing = 2;
    public int maxAlienSpawnPlayerSpacing = 6;

    // Use this for initialization
    void Start()
    {

        //set up tiles
        tiles = new bool[dimension, dimension];
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                tiles[i, j] = true;
            }

        }

        Rectangle allSpace = new Rectangle(1, 1, dimension - 1, dimension - 1);
        Tree<Rectangle> binarySpace = RecurseCreateTree(allSpace, iterations);

        //now that we have made a BSP tree, carve out the dungeon.
        //hmm... maybe we only want 1 or 2 thickness wall

        tiles = RecurseCarveOutDungeon(binarySpace, tiles);




        //lets debug

        //for (int i = 0; i < 96; i++)
        //{
        //    string line = "";
        //    for (int j = 0; j < 96; j++)
        //    {

        //        if (tiles[i, j] == true)
        //        {
        //            line += "#";
        //        }
        //        else
        //        {
        //            line += ".";
        //        }
        //    }
        //    Debug.Log(line);

        //}


        //spawn the walls.

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                if (tiles[i, j] == true)
                {
                    GameObject p = GameObject.Instantiate(pillar);
                    p.transform.position = new Vector3(i - dimension / 2, 0, j - dimension / 2);
                }
            }

        }

        //spawn players

        List<Point> playerPositions = new List<Point>();

        while (true)
        {
            //find 4 adjacent squares
            int x = Random.Range(0, dimension);
            int y = Random.Range(0, dimension - 7);

            if (!tiles[x, y] && !tiles[x, y + 1] && !tiles[x, y + 2] && !tiles[x, y + 3] && !tiles[x, y + 4] && !tiles[x, y + 5] && !tiles[x, y + 6] && !tiles[x, y + 7])
            {
                GameObject a = GameObject.Instantiate(alpha);
                a.transform.position = new Vector3(x - dimension / 2, 0, y - dimension / 2);
                playerPositions.Add(new Point(x,y));
                a.name = "Alpha";
                GameObject b = GameObject.Instantiate(bravo);
                b.transform.position = new Vector3(x - dimension / 2, 0, y + 3 - dimension / 2);
                playerPositions.Add(new Point(x, y+3));
                b.name = "Bravo";
                GameObject c = GameObject.Instantiate(charlie);
                c.transform.position = new Vector3(x - dimension / 2, 0, y + 6 - dimension / 2);
                playerPositions.Add(new Point(x, y+5));
                c.name = "Charlie";
                GameObject d = GameObject.Instantiate(delta);
                d.transform.position = new Vector3(x - dimension / 2, 0, y + 7 - dimension / 2);
                playerPositions.Add(new Point(x, y+7));
                d.name = "Delta";
                //set up command controller

                GameObject turnManagerObj = GameObject.Find("Turn Manager");
                CommandController cc = turnManagerObj.GetComponent<CommandController>();
                cc.SoldierList.Add(a.GetComponent<SoldierInfo>());
                cc.SoldierList.Add(b.GetComponent<SoldierInfo>());
                cc.SoldierList.Add(c.GetComponent<SoldierInfo>());
                cc.SoldierList.Add(d.GetComponent<SoldierInfo>());

                TurnManager turnManagerComponent = turnManagerObj.GetComponent<TurnManager>();
                turnManagerComponent.SoldierList[0]=(a.GetComponent<SoldierCommands>());
                turnManagerComponent.SoldierList[1]=(b.GetComponent<SoldierCommands>());
                turnManagerComponent.SoldierList[2]=(c.GetComponent<SoldierCommands>());
                turnManagerComponent.SoldierList[3]=(d.GetComponent<SoldierCommands>());

                UIIOMan uIIOMan = GameObject.Find("UIIOManager").GetComponent<UIIOMan>();
                uIIOMan.alphasp = a.transform.GetChild(0).gameObject;
                uIIOMan.bravosp = b.transform.GetChild(0).gameObject;
                uIIOMan.charliesp = c.transform.GetChild(0).gameObject;
                uIIOMan.deltasp = d.transform.GetChild(0).gameObject;

                break;
            }



        }



        //spawn ayys

        List<Point> alienPositions = new List<Point>();
        int tries = Random.Range(minAlienTries, maxAlienTries);

        GameObject turnManager = GameObject.Find("Turn Manager");
        TurnManager tm = turnManager.GetComponent<TurnManager>();
        List<AlienAI> ais = new List<AlienAI>();
        for (int i = tries; i > 0; i--)
        {
            //try to place an ayy
            int x = Random.Range(1, dimension - 1);
            int y = Random.Range(1, dimension - 1);

            if (tiles[x, y] == false)
            {
               
                //ayy lamoas dont start too close to player start position
                if (playerPositions.Find(p => Mathf.Sqrt(((p.x - x) ^ 2) + ((p.y - y) ^ 2)) < maxAlienSpawnPlayerSpacing) == null)
                {
                   
                    if (alienPositions.Find(p => Mathf.Sqrt(((p.x - x) ^ 2) + ((p.y - y) ^ 2)) < maxAlienSpawnSpacing) == null)
                    { //make sure ayy lmaos srent spawned to close to each other


                        GameObject p = GameObject.Instantiate(alien);

                        ais.Add(p.GetComponent<AlienAI>());
                        alienPositions.Add(new Point(x, y));
                        p.transform.position = new Vector3(x - dimension / 2, 2, y - dimension / 2);
                    }
                }
            }

        }

        tm.AlienList = new AlienAI[ais.Count];
        for (int  i = 0; i<ais.Count; i++) {
            tm.AlienList[i] = ais[i];
        }


    }



    private Tree<Rectangle> RecurseCreateTree(Rectangle rect, int togo)
    {

        if (togo == 0)
        {
            return new Tree<Rectangle>(rect);
        }


        //split the rectangle


        bool splitOnX = (Random.Range(0, 2) % 2 == 0);
        float fraction = Random.Range(pivotMin, pivotMax);

        if (splitOnX)
        {
            int pivotLineY = rect.y + (int)(fraction * rect.height);

            Rectangle top = new Rectangle(rect.x, pivotLineY, rect.width, rect.height + rect.y - pivotLineY);
            Rectangle bottom = new Rectangle(rect.x, rect.y, rect.width, pivotLineY - rect.y);

            return new Tree<Rectangle>(rect, RecurseCreateTree(top, togo - 1), RecurseCreateTree(bottom, togo - 1));




        }
        else
        {
            int pivotLineX = rect.x + (int)(fraction * rect.width);

            Rectangle left = new Rectangle(rect.x, rect.y, pivotLineX - rect.x, rect.height);
            Rectangle right = new Rectangle(pivotLineX, rect.y, rect.x + rect.width - pivotLineX, rect.height);

            return new Tree<Rectangle>(rect, RecurseCreateTree(left, togo - 1), RecurseCreateTree(right, togo - 1));

        }


    }

    private bool[,] RecurseCarveOutDungeon(Tree<Rectangle> currentRoom, bool[,] dungeon)
    {
        Rectangle rect = currentRoom.value;
        bool[,] ret = dungeon;



        if (currentRoom.left != null)
        {
            ret = RecurseCarveOutDungeon(currentRoom.left, ret);
        }
        if (currentRoom.right != null)
        {
            ret = RecurseCarveOutDungeon(currentRoom.right, ret);
        }
        if (currentRoom.left == null && currentRoom.right == null)
        {

            //carve the room out
            for (int i = 0; i < rect.width - 1; i++)
            {
                for (int j = 0; j < rect.height - 1; j++)
                {
                    //carve out that tile
                    ret[rect.x + i, rect.y + j] = false;
                }
            }



        }

        //carve an x corridor

        if ((Random.Range(0, 3) % 3 != 0))
        {

            if (rect.x >= 1)
            {
                int corridorY = Random.Range(rect.y + 1, rect.y + rect.height - 2);
                ret[rect.x, corridorY] = false;
                ret[rect.x, corridorY + 1] = false;
                ret[rect.x + 1, corridorY] = false;
                ret[rect.x + 1, corridorY + 1] = false;
                ret[rect.x - 1, corridorY] = false;
                ret[rect.x - 1, corridorY + 1] = false;
            }
        }



        if ((Random.Range(0, 3) % 3 != 0))
        {

            if (rect.x + rect.width < dimension)
            {

                int corridorY = Random.Range(rect.y + 1, rect.y + rect.height - 2);
                ret[rect.x + rect.width - 1, corridorY] = false;
                ret[rect.x + rect.width - 1, corridorY + 1] = false;

                ret[rect.x + rect.width + 1, corridorY] = false;
                ret[rect.x + rect.width + 1, corridorY + 1] = false;

                ret[rect.x + rect.width, corridorY] = false;
                ret[rect.x + rect.width, corridorY + 1] = false;
            }
        }

        if ((Random.Range(0, 3) % 3 != 0))
        {

            int corridorX = Random.Range(rect.x + 1, rect.x + rect.width - 2);
            //carve a y corridor
            if (rect.y >= 1)
            {

                ret[corridorX, rect.y] = false;
                ret[corridorX + 1, rect.y] = false;
                ret[corridorX, rect.y + 1] = false;
                ret[corridorX + 1, rect.y + 1] = false;
                ret[corridorX, rect.y - 1] = false;
                ret[corridorX + 1, rect.y - 1] = false;
            }

        }

        if ((Random.Range(0, 3) % 3 != 0))
        {

            int corridorX = Random.Range(rect.x + 1, rect.x + rect.width - 2);
            if (rect.y + rect.height < dimension)
            {
                ret[corridorX, rect.y + rect.height] = false;
                ret[corridorX + 1, rect.y + rect.height] = false;
                ret[corridorX, rect.y + 1 + rect.height] = false;
                ret[corridorX + 1, rect.y + 1 + rect.height] = false;
                ret[corridorX, rect.y - 1 + rect.height] = false;
                ret[corridorX + 1, rect.y - 1 + rect.height] = false;
            }
        }

        //remake the border wall



        return ret;

    }



}
