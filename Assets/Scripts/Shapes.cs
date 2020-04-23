using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shapes
{
    public static bool IsIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float x1 = p1.x;
        float x2 = p2.x;
        float x3 = p3.x;
        float x4 = p4.x;
        float y1 = p1.y;
        float y2 = p2.y;
        float y3 = p3.y;
        float y4 = p4.y;
        float denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
        if (denominator == 0)
        {
            return false;
        }
        float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
        float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
        return t <= 1 && t >= 0 && u <= 1 && u >= 0;
    }
    public static List<Vector2> Flatten(List<Vector3> points, char axis)
    {
        List<Vector2> newPoints = new List<Vector2>();
        if(axis == 'z')
        {
            for(int i = 0; i < points.Count; i++)
            {
                newPoints.Add(new Vector2(points[i].x, points[i].y));
            }
        }else if(axis == 'y')
        {
            for (int i = 0; i < points.Count; i++)
            {
                newPoints.Add(new Vector2(points[i].x, points[i].z));
            }
        }else if(axis == 'x')
        {
            for (int i = 0; i < points.Count; i++)
            {
                newPoints.Add(new Vector2(points[i].z, points[i].y));
            }
        }
        return newPoints;
    }

    public static bool InsidePolygon(Vector2 point, List<Vector2> polygon)
    {
        Vector2 maxX = polygon[0];
        for(int i = 1; i < polygon.Count; i++)
        {
            if(polygon[i].x > maxX.x)
            {
                maxX = polygon[i];
            }
        }
        maxX = new Vector2(maxX.x + 1, maxX.y + 1);
        int intersections = 0;
        for(int i = 0; i < polygon.Count - 1; i++)
        {
            if(IsIntersection(polygon[i], polygon[i+1], point, maxX))
            {
                intersections++;
            }
        }

        if (IsIntersection(polygon[polygon.Count - 1], polygon[0], point, maxX))
        {
            intersections++;
        }
        if(intersections%2 == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public static bool InsidePolygon(Vector2 point1, Vector2 point2, List<Vector2> polygon)
    {
        return InsidePolygon(point1 + (point2 - point1)/2, polygon);
    }

    public static bool IntersectsPolygon(Vector2 point1, Vector2 point2, List<Vector2> polygon)
    {
        for(int i = 0; i < polygon.Count; i++)
        {
            int j = i + 1;
            if (j == polygon.Count)
            {
                j = 0;
            }
            if (point1 != polygon[i] && point1 != polygon[j] && point2 != polygon[i] && point2 != polygon[j])
            {
                if (IsIntersection(point1, point2, polygon[i], polygon[j]))
                {
                    return true;
                }
            }
        }
        return false;
    }
    //if points.Count is odd there will be one triangle at Quads()[0]
    public static List<List<Vector2>> Quads(List<Vector3> points, char axis)
    {
        List<Vector2> pts = Flatten(points, axis);
        List<List<Vector2>> quads = new List<List<Vector2>>();
        points = new List<Vector3>(points);

        int index = 0;
        if (points.Count%2 == 1)
        {
            //number of points are odd so add a triangle on the outside
            for(int i = 0; i < pts.Count; i++)
            {
                int j = i + 1;
                int k = j + 1;
                if(j == pts.Count)
                {
                    j = 0;
                    k = 1;
                }
                if(k == pts.Count)
                {
                    k = 0;
                }
                //find a line inside the polygon to separate a triangle from
                if(InsidePolygon(pts[i], pts[k], pts))
                {
                    quads.Add(new List<Vector2>());
                    quads[0].Add(points[i]);
                    quads[0].Add(points[j]);
                    quads[0].Add(points[k]);
                    pts.RemoveAt(j); //this point can no-longer be accessed without going through the triangle already made
                    points.RemoveAt(j);
                    index++;
                    break;
                }
            }
        }
        while(pts.Count != 2)
        {
            for(int i = 0; i < pts.Count; i++)
            {
                int j = i + 1;
                if (j > pts.Count)
                {
                    j = 0;
                }
                int k = j + 1;
                if (k > pts.Count)
                {
                    k = 0;
                }
                int l = k + 1;
                if (l > pts.Count)
                {
                    l = 0;
                }
                if(!IntersectsPolygon(pts[i], pts[l], pts) && InsidePolygon(pts[i], pts[l], pts))
                {
                    quads.Add(new List<Vector2>());
                    quads[index].Add(points[i]);
                    quads[index].Add(points[j]);
                    quads[index].Add(points[k]);
                    quads[index].Add(points[l]);
                    //points j and k can no-longer be accessed without going through the quad already made
                    //and so they should b removed
                    if (k > j)
                    {
                        pts.RemoveAt(j);
                        pts.RemoveAt(j);//actually removing k
                        points.RemoveAt(j);
                        points.RemoveAt(j);
                    }
                    else
                    {
                        pts.RemoveAt(k);
                        pts.RemoveAt(j-1);//actually removing j
                        points.RemoveAt(k);
                        points.RemoveAt(j - 1);
                    }
                    break;
                }
            }
        }
        return quads;
    }
}
