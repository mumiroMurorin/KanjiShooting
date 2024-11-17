using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointSelectorRandomQueue : MonoBehaviour , ISpawnpointSelector
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int randomness = 10; 

    List<Transform> unselectedPoints;

    /// <summary>
    /// �X�|�[���|�C���g�̃����_���I�o
    /// </summary>
    /// <returns></returns>
    public Transform GetSpawnPoint()
    {
        if (spawnPoints == null) { Debug.LogError("�ySystem�z�X�|�[���|�C���g���ݒ肳��Ă��܂���"); return null; }
        
        // ���Z�b�g
        if(unselectedPoints == null) { ResetSpawnPoints(); }
        if (unselectedPoints.Count == 0) { ResetSpawnPoints(); }

        Transform point = unselectedPoints[0];
        unselectedPoints.RemoveAt(0);
        return point;
    }

    /// <summary>
    /// �����_���L���[���쐬
    /// </summary>
    private void ResetSpawnPoints()
    {
        unselectedPoints = new List<Transform>();
        foreach(Transform tr in spawnPoints)
        {
            unselectedPoints.Add(tr);
        }

        // ���X�g������������
        for(int i = 0; i < randomness; i++)
        {
            int previous = Random.Range(0, unselectedPoints.Count);
            int current = Random.Range(0, unselectedPoints.Count);
            Transform tr = unselectedPoints[previous];
            unselectedPoints[previous] = unselectedPoints[current];
            unselectedPoints[current] = tr;
        }
    }

}
