﻿using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class GraphBoot
{
    //  ----Private Variables----


    private static EntityArchetype CubeArchetype;
    private static MeshInstanceRenderer cubeRenderer;
    private static EntityManager entityManager;
    private static Entity cube;


    //  ----initial functions----


   [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Awake()
    {
        //create entities
        entityManager = World.Active.GetOrCreateManager<EntityManager>();

        CubeArchetype = entityManager.CreateArchetype(
            typeof(Position),
            typeof(Index),
            typeof(Scale)
            );

        //other game startup peram
        Input.backButtonLeavesApp = true;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Start()
    {
        cubeRenderer = GameObject.FindObjectOfType<MeshInstanceRendererComponent>().Value;
        PopulateCubles();
    }

    private static void PopulateCubles()
    {
        for (int i = 0; i < 40000; i++)
        {
            cube = entityManager.CreateEntity(CubeArchetype);
            entityManager.SetComponentData(cube, new Position { Value = new float3(0f, 0f, 0f) });
            entityManager.SetComponentData(cube, new Index { index = i });
            entityManager.AddSharedComponentData(cube, cubeRenderer);
        }
    }
}