using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ExplosiveBarrelManager : MonoBehaviour
{
    public static List<ExplosiveBarrel> allTheBarrels = new List<ExplosiveBarrel>();
    public  static void UpdateAllBarrelColors()
    {
        foreach (ExplosiveBarrel barrel in allTheBarrels)
        {
            barrel.tryApplyColor();
        }
    }


    void OnDrawGizmos()
    {
        foreach (var barrel in allTheBarrels)
        {
            if (barrel.type == null)
                return;
            
            #if UNITY_EDITOR
            Vector3 managerPos = transform.position;
            Vector3 barrelPos = barrel.transform.position;
            float halfPos = (managerPos.y - barrelPos.y)*.5f;
            Vector3 halfOffset = halfPos * Vector3.up;

            Handles.DrawBezier(
                transform.position,
                barrel.transform.position,
                managerPos - halfOffset,
                barrelPos + halfOffset,
                barrel.type.color,
                Texture2D.whiteTexture,
                1f);
            #endif
        }
    }
}
