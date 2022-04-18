#if UNITY_EDITOR

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnPoints))]
public class SpawnPointsEditor : Editor 
{
    [Serializable] public struct GroupSpawnTemplate {
        public enum GroupSpawnType {
            Line,
            Cross
        };
        
        public GroupSpawnType type;
        public Vector3 midle;
        [Min(2)] public int nbr_spawn;
        [Min(0)] public float size;
        
        public float angle;
    };
    
    SpawnPoints myScript;
    [SerializeField] public GroupSpawnTemplate tempGroupSpawn;

    void OnEnable()
    {
        myScript = (SpawnPoints)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        DrawHelpSpawn();
    }

    private void DrawHelpSpawn()
    {
        EditorGUILayout.LabelField("Help to Add Spawns", EditorStyles.boldLabel);
//        EditorGUILayout.PropertyField(tempGroupSpawn_serialized);
        DrawGroupSpawnTemplate(tempGroupSpawn);
        void DrawGroupSpawnTemplate(GroupSpawnTemplate to_draw)
        {
            EditorGUILayout.BeginVertical("Box");
            
            tempGroupSpawn.type = (GroupSpawnTemplate.GroupSpawnType)
                EditorGUILayout.EnumPopup("Type",
                                            tempGroupSpawn.type);
            tempGroupSpawn.midle =
                EditorGUILayout.Vector3Field("midle", tempGroupSpawn.midle);
            tempGroupSpawn.nbr_spawn = 
                EditorGUILayout.IntField("nbr of spawns",
                                        tempGroupSpawn.nbr_spawn);
        switch (tempGroupSpawn.type) {
            case GroupSpawnTemplate.GroupSpawnType.Line:
                if (tempGroupSpawn.nbr_spawn < 2)
                    tempGroupSpawn.nbr_spawn = 2;
                break;
            case GroupSpawnTemplate.GroupSpawnType.Cross:
                if (tempGroupSpawn.nbr_spawn < 4)
                    tempGroupSpawn.nbr_spawn = 4;
                break;
        }
            tempGroupSpawn.size = 
                EditorGUILayout.FloatField("size of group",
                                        tempGroupSpawn.size);
            if (tempGroupSpawn.size < 0)
                tempGroupSpawn.size = 0;
                                        
            tempGroupSpawn.angle = 
                EditorGUILayout.FloatField("angle",
                                        tempGroupSpawn.angle);
            switch (tempGroupSpawn.type) {
                case GroupSpawnTemplate.GroupSpawnType.Line:
                    break;
                case GroupSpawnTemplate.GroupSpawnType.Cross:
                    break;
            }
            
            EditorGUILayout.EndVertical();
        }
        if(GUILayout.Button("Add Spawns")) {
            myScript.AddSpawns(GetGroupSpawnPositions(tempGroupSpawn));
        }
    }

    private List<Vector3> GetGroupSpawnPositions(GroupSpawnTemplate spawns)
    {
        List<Vector3> to_return = new List<Vector3>();

        switch (spawns.type) {
            case GroupSpawnTemplate.GroupSpawnType.Line:
                foreach (float i in Enumerable.Range(0, spawns.nbr_spawn)) {
                    Vector3 new_vector = new Vector3(i / (spawns.nbr_spawn - 1), 0, 0);
                    new_vector.x -= 0.5f;
                    new_vector = Quaternion.AngleAxis(spawns.angle, Vector3.up) * new_vector;
                    new_vector *= spawns.size;
                    new_vector += spawns.midle;
                    to_return.Add(new_vector);
                }
                break;
            case GroupSpawnTemplate.GroupSpawnType.Cross:
                GroupSpawnTemplate Line_a = spawns;
                Line_a.type = GroupSpawnTemplate.GroupSpawnType.Line;
                GroupSpawnTemplate Line_b = spawns;
                Line_b.type = GroupSpawnTemplate.GroupSpawnType.Line;
                Line_a.nbr_spawn /= 2;
                Line_b.nbr_spawn /= 2;
                if (spawns.nbr_spawn % 2 == 1)
                    Line_b.nbr_spawn += 1;
                Line_b.angle += 90;
                
                to_return.AddRange(GetGroupSpawnPositions(Line_a));
                to_return.AddRange(GetGroupSpawnPositions(Line_b));
                break;
        }
        return to_return;
    }
}

#endif
