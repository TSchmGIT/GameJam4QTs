using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Item item       = other.GetComponent<Item>();
        if (!item)
        {
            return;
        }

        ItemTier tier   = item.GetTier();

        ItemSettings settings = GameManager.FindObjectOfType<ItemManager>().GetItemTierSetting(tier);
        GameManager.Instance.TickManager.AddPoints(settings.ScorePerMachineDone * item.GetRuntimeData().MachinesNeededTotal);

        GameObject.Destroy(item);
    }
}
