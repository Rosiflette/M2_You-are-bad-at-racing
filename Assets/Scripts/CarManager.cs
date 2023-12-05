using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour
{
    [SerializeField] private float maxEssence = 100;
    [SerializeField] private float essenceConsommation;
    [SerializeField] private float essenceQuantity;
    [SerializeField] private PlayerBehaviour player;
    [SerializeField] private Slider fuelGauge;
    // Start is called before the first frame update
    void Start()
    {
        essenceQuantity = maxEssence;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.Throttle != 0){
            essenceQuantity -= essenceConsommation * Time.deltaTime;
            fuelGauge.value = essenceQuantity/maxEssence;
        }
    }


    
}
