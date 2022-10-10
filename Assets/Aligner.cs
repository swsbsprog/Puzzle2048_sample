using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Aligner : MonoBehaviour
{
    public int xCount = 4, yCount = 4;
    public int width = 1, height = 1;
    [ContextMenu("Á¤·Ä")]
    void Align()
    {
        var childs = GetComponentsInChildren<Transform>()
            .Where(x => x.parent == transform)
            .ToArray();

        for (int y = 1; y <= yCount; y++)
        {
            for (int x = 1; x <= xCount; x++)
            {
                Vector3 pos = new Vector3(x * width, y * height, 0);
                int boxIndex = (y-1) * xCount + (x-1);
                if (boxIndex >= childs.Length)
                    break;
                childs[boxIndex].localPosition = pos;
            }
        }
    }
}
