using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ProcGenHelpers;
public class GenerateDungeonLinedef : MonoBehaviour
{


    public int numberOfPOints = 10;
    public int minDimension = -48;
    public int maxDimension = 48;

    public float minDistance = 5.0f;
    // Use this for initialization

    public int minAlienTries = 30;

    public int maxAlienTries = 100;


    void Start()
    {


        //make a list of points to join up
        List<Point> points = new List<Point>();
        for (int i = numberOfPOints; i > 0; i--)
        {

            Point newPoint = new Point(Random.Range(minDimension, maxDimension), Random.Range(minDimension, maxDimension));
            bool canAdd = true;
            foreach (Point p in points)
            {
                if ((System.Math.Sqrt(((p.x - newPoint.x) * (p.x - newPoint.x)) + ((p.y - newPoint.y) * (p.y - newPoint.y)))) < minDistance)
                {
                    
                    canAdd = false;
                    break;
                }
            }


            if (canAdd)
            {
                points.Add(newPoint);
            }
        }

        List<Linedef> lines = new List<Linedef>();

        foreach (Point p in points)
        {
            List<Point> others = points.FindAll(a => a.x == p.x || a.y == p.y);
            others.Remove(p);
            if (others.Count != 0)
            {
                Point other = others[Random.Range(0, others.Count)];
               
                lines.Add(new Linedef(p, other));
            }

        }


        //now create the walls

        foreach (Linedef l in lines)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.position = new Vector3(l.AveragePoint().x, 0, l.AveragePoint().y);
            if (l.Direction() == true)
            {
                //wall is on x axis
                //Debug.Log(l.end.x+","+l.end.y +","+l.start.x +","+l.start.y);
                if (l.Length() == 0) {
                    Debug.Log("zero x");
                }
                wall.transform.localScale = new Vector3(l.Length()+2, 7, 1);

            }
            else
            {
                //wall is on y axis
                //Debug.Log(l.end.x + "," + l.end.y + "," + l.start.x + "," + l.start.y);
                if (l.Length() == 0)
                {
                    Debug.Log(l.end.x + "," + l.end.y + "," + l.start.x + "," + l.start.y);
                    Debug.Log("zero y");
                }
                wall.transform.localScale = new Vector3(1, 7, l.Length()+2);
            }
        }



        //spawn aliens
        // does not check if they are even spawned in a reachable area,  and I dont even have a way to figure out how.
        int tries = Random.Range(minAlienTries, maxAlienTries);
        //for (int i = tries; i > 0; i--)
        //{
        //    //try to place an ayy
        //    int x = Random.Range(1, dimension - 1);
        //    int y = Random.Range(1, dimension - 1);

        //    if (tiles[x, y] == false)
        //    {
        //        GameObject p = GameObject.Instantiate(alien);
        //        p.transform.position = new Vector3(x - dimension / 2, 2, y - dimension / 2);
        //    }

        //}

    }

}


