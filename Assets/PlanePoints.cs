using System.Collections.Generic;
using UnityEngine;

public class PlanePoints : ObjectPoints
{
    protected override void CalculateEdgeVectors(int VectorCornerIdx)
    {
        base.CalculateEdgeVectors(VectorCornerIdx);
        EdgeVectors.Add(CornerPoints[3] - CornerPoints[VectorCornerIdx]);
        EdgeVectors.Add(CornerPoints[1] - CornerPoints[VectorCornerIdx]);
    }

    protected override void CalculateRandomPoint()
    {
        int randomCornerIdx = Random.Range(0, 2) == 0 ? 0 : 2; //there is two triangles in a plane, which tirangle contains the random point is chosen
                                                               //corner point is chosen for triangles as the variable
        CalculateEdgeVectors(randomCornerIdx);
        float u = Random.Range(0.0f, 1.0f);
        float v = Random.Range(0.0f, 1.0f);
        if (v + u > 1) //sum of coordinates should be smaller than 1 for the point be inside the triangle
        {
            v = 1 - v;
            u = 1 - u;
        }
        RandomPoint = CornerPoints[randomCornerIdx] + u * EdgeVectors[0] + v * EdgeVectors[1];
    }

    protected override void CalculateCornerPoints()
    {
        base.CalculateCornerPoints();
        CornerPoints.Add(ObjectVertices[0]); //corner points are added to show  on the editor
        CornerPoints.Add(ObjectVertices[10]);
        CornerPoints.Add(ObjectVertices[110]);
        CornerPoints.Add(ObjectVertices[120]);
    }


}
