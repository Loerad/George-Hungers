using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HungerUI : MonoBehaviour
{

    [SerializeField] private Image hungerMeter;


    private void Update()
    {
        hungerMeter.fillAmount = HungerManager.Instance.HungerPercent();
    }
}
