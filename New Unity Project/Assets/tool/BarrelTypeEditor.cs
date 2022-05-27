using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(BarrelType))]
[CanEditMultipleObjects]//important!
public class BarrelTypeEditor : Editor
{
    public enum Things
    {
        eeee, aaaa, mmmm
    }
    SerializedObject so;
    
    SerializedProperty PropRadius;
    SerializedProperty PropDamage;
    SerializedProperty PropColor;
    void OnEnable()
    {
        so = serializedObject; //refer to the serialized version
        PropRadius = so.FindProperty("radius");
        PropDamage = so.FindProperty("damage");
        PropColor = so.FindProperty("color");
    }
    
    public override void OnInspectorGUI()//gui is actually a loop, so keep it simple
    {
        so.Update();//get latest
        EditorGUILayout.PropertyField( PropRadius );//help you pick the field, will also get the set range
        EditorGUILayout.PropertyField( PropDamage );
        EditorGUILayout.PropertyField( PropColor );
        if (so.ApplyModifiedProperties())
        {
            ExplosiveBarrelManager.UpdateAllBarrelColors();

        };//with undo, return status

#if stupid
            GUILayout.BeginHorizontal();
            GUILayout.Label("Eeyore", GUILayout.Width(60));
            if (GUILayout.Button("pet Eeyore"))
            {
                Debug.Log("Lost 1 carrot");
            }

            GUILayout.EndHorizontal();

            GUILayout.Label("Flowey", GUILayout.Width(60));
            GUILayout.Space(40);
            GUILayout.Label("Rosy", GUI.skin.button); //steal the skin:P

            things = (Things)EditorGUILayout.EnumPopup(things);
            someValue = GUILayout.HorizontalSlider(someValue, -1f, 1f);
            GUILayout.Space(40);

            EditorGUILayout.ObjectField("Assign here:", null, typeof(Transform), true);

            BarrelType barrel = target as BarrelType;
            //GUILayout.Label("radius:" + barrel.radius);
            barrel.radius = EditorGUILayout.FloatField("radius:", barrel.radius);
            barrel.damage = EditorGUILayout.FloatField("damage:", barrel.damage);
            barrel.color = EditorGUILayout.ColorField("Color:", barrel.color)

#else
//we need proper serialize

#if stillbad //no multi select support
        BarrelType barrel = target as BarrelType;

        float newRadius = EditorGUILayout.FloatField("radius:", barrel.radius);
        
        if (newRadius != barrel.radius)
        {
            Undo.RecordObject(barrel,"change barrel radius");
            barrel.radius = newRadius;
        }
        
        barrel.damage = EditorGUILayout.FloatField("damage:", barrel.damage);
        barrel.color = EditorGUILayout.ColorField("Color:", barrel.color);
#endif
#endif
    }



}
