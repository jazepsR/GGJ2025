using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;
    public static BoardManager instance;
    public Transform hand;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }
    // Start is called before the first frame update
    public void UpdateGold(int gold)
    {
        goldText.text = "GOLD: " + gold;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
