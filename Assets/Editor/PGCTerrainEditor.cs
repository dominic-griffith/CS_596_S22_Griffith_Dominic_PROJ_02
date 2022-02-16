using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PGCTerrain))]                        //Need to tell the Editor what class to act on
[CanEditMultipleObjects]

public class PGCTerrainEditor : Editor
{

    SerializedProperty randomHeightRange;
    SerializedProperty heightMapScale;
    SerializedProperty heightMapImage;
    SerializedProperty hillVariance;

    bool showRandom = false;
    bool showLoadHeights = false;
    bool showTrig = false;
    bool showPerlin = false;

    private void OnEnable()
    {
        randomHeightRange = serializedObject.FindProperty("randomHeightRange");
        heightMapScale = serializedObject.FindProperty("heightMapScale");
        heightMapImage = serializedObject.FindProperty("heightMapImage");
        hillVariance = serializedObject.FindProperty("hillVariance");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PGCTerrain terrain = (PGCTerrain)target;               // Target is the thing the editor is working on

        showRandom = EditorGUILayout.Foldout(showRandom, "Random");

        if(showRandom)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Set Heights Between Random Values", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(randomHeightRange);
            if(GUILayout.Button("Random Heights"))
            {
                terrain.RandomTerrain();                      //Do the thing in yourr other class PGC Terrain
            }
        }


        showLoadHeights = EditorGUILayout.Foldout(showLoadHeights, "Load Heights");

        if (showLoadHeights)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Load Heights From Texture", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(heightMapImage);
            EditorGUILayout.PropertyField(heightMapScale);
            if (GUILayout.Button("Load Texture"))
            {
                terrain.LoadTexture();
            }
        }

        showTrig = EditorGUILayout.Foldout(showLoadHeights, "Trig Function");

        if (showTrig)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Trigonometric Function", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(hillVariance);
            if (GUILayout.Button("Trig Function"))
            {
                terrain.TrigFunction();
            }
        }


        showPerlin = EditorGUILayout.Foldout(showLoadHeights, "Perlin Noise");

        if (showPerlin)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Perlin Noise", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(hillVariance);
            if (GUILayout.Button("Perlin Noise"))
            {
                terrain.PerlinNoise();
            }
        }


        serializedObject.ApplyModifiedProperties();
    }
}
