using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int gold;
    bool isLocal = false;

    public int GetGold()
    {
        return gold;
    }

    public Player(int gold, string name, bool isLocal)
    {
        this.gold = gold;
        this.isLocal = isLocal;
        if (isLocal)
            BoardManager.instance.UpdateGold(gold);
    }
    // Start is called before the first frame update
    public void ModifyGold(int modifyBy)
    {
        gold += modifyBy;
        if(isLocal)
            BoardManager.instance.UpdateGold(gold);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
