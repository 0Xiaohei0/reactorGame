using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Planet : MonoBehaviour
{

    public Slider controlRodSlider;
    public TextMeshProUGUI statusValue;
    public TextMeshProUGUI powerOutputValue;
    public TextMeshProUGUI coreTempertureValue;
    public TextMeshProUGUI damageValue;
    public TextMeshProUGUI controlRodValue;

    public bool status = true;
    [SerializeField] private float powerOutput = 0;
    [SerializeField] private float coreTemperture = 0;
    [SerializeField] private float structuralDamage = 0;
    [SerializeField] private float controlRodInsertion = 0;

    [SerializeField] private float equilibriumTemperature = 300;

    [SerializeField] private Color32 NormalColor = new Color32(255, 255, 255, 255);
    [SerializeField] private const float warningTemp = 2400;
    [SerializeField] private const float warningDamage = 70;
    [SerializeField] private Color32 warningColor = new Color32(255, 255, 102, 255);

    [SerializeField] private const float DangerTemp = 2700;
    [SerializeField] private const float DangerDamage = 90;
    [SerializeField] private Color32 DangerColor = new Color32(255, 102, 51, 255);

    [SerializeField] private const float HeatExchangeRate = 0.01f;
    [SerializeField] private const float DamageRate = 0.1f;
    [SerializeField] private const float RecoverRate = 0.1f;
    [SerializeField] private const float basePower = 2f;
    [SerializeField] private const float maxPower = 10f;

    public float PowerOutput
    {
        get => powerOutput; set
        {
            powerOutput = value;
            powerOutputValue.text = (int)powerOutput + " TW";
        }
    }
    public float CoreTemperture
    {
        get => coreTemperture; set
        {
            coreTemperture = value;
            coreTempertureValue.text = (int)CoreTemperture + "\u00B0";
            if (CoreTemperture > DangerTemp)
                coreTempertureValue.color = DangerColor;
            else if (CoreTemperture > warningTemp)
                coreTempertureValue.color = warningColor;
            else
                coreTempertureValue.color = NormalColor;
        }
    }
    public float StructuralDamage
    {
        get => structuralDamage; set
        {
            structuralDamage = Mathf.Clamp(value, 0, 100);
            damageValue.text = (int)structuralDamage + " %";
            if (StructuralDamage > DangerDamage)
                damageValue.color = DangerColor;
            else if (StructuralDamage > warningDamage)
                damageValue.color = warningColor;
            else
                damageValue.color = NormalColor;
        }
    }
    public float ControlRodInsertion { get => controlRodInsertion; set { controlRodInsertion = value; controlRodValue.text = (int)ControlRodInsertion + " %"; } }


    public void UpdateControlRod()
    {
        ControlRodInsertion = (int)controlRodSlider.value;
        equilibriumTemperature = (300 + (100 - ControlRodInsertion) * 4000 / 100);
    }

    private void ChangeTemperature()
    {
        CoreTemperture += HeatExchangeRate * (equilibriumTemperature - CoreTemperture);
    }

    private void ChangeStructuralDamage()
    {
        if (CoreTemperture > DangerTemp)
            StructuralDamage += DamageRate;
        else
            StructuralDamage -= RecoverRate;
    }

    private void ChangePowerOutput()
    {
        PowerOutput = basePower + Mathf.Pow((100 - controlRodInsertion) / 100, 2) * maxPower;
    }
    // Start is called before the first frame update
    void Start()
    {
        CoreTemperture = 300;
        equilibriumTemperature = CoreTemperture;
        PowerOutput = basePower;
        controlRodInsertion = 100;
        InvokeRepeating(nameof(ChangeTemperature), 0f, 0.1f);
        InvokeRepeating(nameof(ChangeStructuralDamage), 0f, 0.1f);
        InvokeRepeating(nameof(ChangePowerOutput), 0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
