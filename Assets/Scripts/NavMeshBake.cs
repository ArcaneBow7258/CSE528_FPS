using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using Unity.AI;
public class NavMeshBake : MonoBehaviour
{
    public NavMeshSurface surface;

    void Start() { surface.BuildNavMesh(); }
    
}
