using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepairZone : MonoBehaviour
{
    public AudioClip[] SmackSound;
    public AudioClip[] finalSmackSound;
    public bool repairOrder;
    public GameObject[] SuppliesToFix;
    public int RepairMeter = 1;

    [Tooltip("Damage done when spawned and healed when repaired.")]
    [Min(0)]
    public int Damage = 1;
    private float RepairPoints = 0;
    //public bool[] isTool;
    public UnityEvent OnAwake;
    public UnityEvent OnRepair;
    public UnityEvent OnFullRepair;
    public UnityEvent OnSmack;
    public BillboardCanvas bc;
    private List<string> SupplyNames = new List<string>();
    private List<PlayerController> Players = new List<PlayerController>();
    private HealthBar hb;
    

    private void Awake()
    {
        foreach (GameObject item in SuppliesToFix)
        {
            SupplyNames.Add(item.name);
        }
        OnAwake.Invoke();
    }
    private void OnEnable()
    {
        //if (Damage > 0)
        //{
        //    hb = GameObject.Find("ship health").GetComponent<HealthBar>();
        //    hb.Damage(Damage);
        //}
    }
    public void DestroyThis()
    {
        RemovePlayers();
        Destroy(gameObject); 
    }
    public void DisableThis()
    {
        gameObject.SetActive(false);
        Refresh();
        RemovePlayers();
        //bc.DisbleBilboard();
    }
    public void Refresh()
    {
        SupplyNames.Clear();
        foreach (GameObject item in SuppliesToFix)
        {
            SupplyNames.Add(item.name);
        }
        RepairPoints = 0;
        bc.UpdateCanvasSlider(RepairPoints);
        bc.DisbleBilboard();
    }
    public void RemovePlayers()
    {
        foreach (PlayerController item in Players)
        {
            item.RemoveInteraction(Repair);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<CharacterController>().Interaction.AddListener(delegate { Repair(other.GetComponent<Pickup>()); });
            other.GetComponent<PlayerController>().AssignInteraction(Repair);
            Players.Add(other.GetComponent<PlayerController>());
            foreach (var player in Players)
            {
                if (player.GetComponent<Pickup>().GetHeldObject() != null && CheckObject(player.GetComponent<Pickup>().GetHeldObject().name))
                {
                    bc.UpdateCanvasSlider(RepairPoints);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<CharacterController>().Interaction.RemoveListener(delegate { Repair(other.GetComponent<Pickup>()); });
            other.GetComponent<PlayerController>().RemoveInteraction(Repair);
            Players.Remove(other.GetComponent<PlayerController>());
        }
    }
    public void Repair(GameObject player)
    {
        //Play Repair Animation or Repair bar
        PlayerController pc = player.GetComponent<PlayerController>();
        Pickup pickup = player.GetComponent<Pickup>();
        
        if (pickup.GetHeldObject() != null && CheckObject(pickup.GetHeldObject().name))   
        {
            RepairPoints += player.GetComponent<PlayerController>().repairPower;
            bc.UpdateCanvasSlider(RepairPoints);
            //pc.anim.SetBool("TempHam", true);
            pc.anim.SetTrigger("Hammer");
            if (RepairPoints >= RepairMeter)
            {
                RemoveObject(pickup.GetHeldObject().name);
                if (pickup.GetHeldObject().GetComponent<Tool>() == null)    
                {
                    pickup.RemoveObject();
                }
                if (SupplyNames.Count == 0)
                {
                    FullRepair();
                    pc.PlayRandomHitSound(finalSmackSound);
                }
                else
                {
                    OnRepair.Invoke();
                    pc.PlayRandomHitSound(SmackSound);
                } 
            }
            else
            {
                OnSmack.Invoke();
                pc.PlayRandomHitSound(SmackSound);
            }
        }
    }
    public void FullRepair()
    {
        OnFullRepair.Invoke();
        //if(Damage > 0)
        //{
        //    hb.Repair(Damage);
        //}
        Debug.Log("repaired");
        return;
    }
    public bool CheckObject(string objName)
    {
        if (repairOrder)
        {
            if (objName.Contains(SupplyNames[0]))
            {
                return true;
            }
            return false;
        }
        else
        {
            for (int i = 0; i < SupplyNames.Count; i++)
            {
                if (objName.Contains(SupplyNames[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
    public void RemoveObject(string objName)
    {
        //SupplyNames.Remove(objName);
        for (int i = 0; i <= SupplyNames.Count; i++)
        {
            if (objName.Contains(SupplyNames[i]))
            {
                SupplyNames.RemoveAt(i);
            }
        }
    }
}
//private void OnTriggerEnter(Collider other)
//{
//    if (other.CompareTag("Player") && other.GetComponent<Pickup>().GetHeldObject() != null)
//    {
//        Pickup player = other.GetComponent<Pickup>();
//        other.GetComponent<CharacterController>().Interaction.AddListener(Repair);
//        if (CheckObject(player.GetHeldObject().name))
//        {

//            if (player.GetHeldObject().GetComponent<Tool>() == null)
//            {
//                player.RemoveObject();
//            }

//            if (SupplyNames.Count == 0)
//            {
//                OnFullRepair.Invoke();
//                Debug.Log("repaired");
//                return;
//            }
//            OnRepair.Invoke();
//        }
//    }
//}