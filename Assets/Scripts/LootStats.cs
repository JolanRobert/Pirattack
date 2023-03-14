using System;
using Player;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

[Serializable]
public enum LootType
{
    damage,
    bounce,
    zap,
    slow,
    pierce,
    fireRate,
}

public class LootStats : MonoBehaviour
{
    [SerializeField] private LootType lootType;
    [SerializeField] private Material[] materials;

    private void OnEnable()
    {
        Init(Random.Range(0, Enum.GetNames(typeof(LootType)).Length));
    }

    private void Init(int index)
    {
        lootType = (LootType) index;
        GetComponent<MeshRenderer>().material = materials[index];
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController)
        {
            playerController.WeaponData.AddStat(lootType);
            Pooler.Instance.Depop(Key.PerkLoot, gameObject);
        }
    }
}
