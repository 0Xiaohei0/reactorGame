using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Planet : MonoBehaviour
{
    public GameObject shieldSprite;
    public SpriteRenderer planetSprite;
    [SerializeField] private Color32 shieldSpriteDangerColor = new Color32(255, 0, 0, 255);
    public AsteroidSpawner asteroidSpawner;
    public float AsteroidSpawnerSpawnRate;

    public TextMeshProUGUI timerText;
    public bool gameWon = false;

    public GameObject victoryPanel;
    public TextMeshProUGUI crisisLevelValue;
    private int crisisLevel;
    public float crisisLevelUpTime = 30f;
    public float crisisLevelUpCountDown;
    public float baseSpawnRate = 1.0f;
    public float crisisMultiplier = 1.3f;

    public Slider controlRodSlider;
    public TextMeshProUGUI statusValue;
    public TextMeshProUGUI powerOutputValue;
    public TextMeshProUGUI coreTempertureValue;
    public TextMeshProUGUI damageValue;
    public TextMeshProUGUI controlRodValue;

    public TextMeshProUGUI devestationValue;
    public TextMeshProUGUI shieldValue;
    public TextMeshProUGUI fireRateValue;
    public TextMeshProUGUI weaponPowerValue;
    public TextMeshProUGUI shieldPowerValue;

    public TextMeshProUGUI mineralValue;

    public TextMeshProUGUI reactorLevelValue;
    public TextMeshProUGUI reactorUpgradeCostValue;
    public Button reactorUpgradeButton;
    public Image reactorUpArrow;
    [SerializeField] private Color32 upArrowGreyColor = new Color32(255, 102, 51, 255);
    [SerializeField] private Color32 upArrowNormalColor = new Color32(255, 255, 255, 255);

    private float devestation;
    private float shield;
    private float fireRate;
    private int weaponPower;
    private int shieldPower;

    private float reactorUpgradeCost;
    private float reactorLevel;

    public Turret turret1;
    public Turret turret2;
    public Turret turret3;
    public Turret turret4;

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

    [SerializeField] private float HeatExchangeRate = 0.01f;
    [SerializeField] private float DamageRate = 0.1f;
    [SerializeField] private float RecoverRate = 0.1f;
    [SerializeField] private float basePower = 2f;
    [SerializeField] private float maxPower = 10f;

    [SerializeField] private float mineral = 0f;

    //[SerializeField] private float shieldRegenRate = 0.1f;
    [SerializeField] private float UITickRate = 0.1f;

    public float timeLeft = 5.0f;

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

    public float Devestation
    {
        get => devestation; set
        {
            devestation = Mathf.Clamp(value, 0, 100);
            devestationValue.text = Devestation + " %";
            if (devestation > DangerDamage)
                devestationValue.color = DangerColor;
            else if (devestation > warningDamage)
                devestationValue.color = warningColor;
            else
                devestationValue.color = NormalColor;
        }
    }
    public float Shield
    {
        get => shield; set
        {
            shield = Mathf.Clamp(value, 0, 100);
            shieldValue.text = Mathf.Round(shield) + " %";
        }
    }
    public float FireRate { get => fireRate; set { fireRate = value; fireRateValue.text = fireRate + " /s"; } }
    public int WeaponPower { get => weaponPower; set { weaponPower = value; weaponPowerValue.text = weaponPower + " TW"; ChangeFireRate(); } }
    public int ShieldPower { get => shieldPower; set { shieldPower = value; shieldPowerValue.text = shieldPower + " TW"; } }

    public float Mineral
    {
        get => mineral; set
        {
            mineral = value;
            mineralValue.text = Mathf.Round(mineral) + " T";

            if (mineral >= ReactorUpgradeCost)
            {
                reactorUpgradeButton.interactable = true;
                reactorUpArrow.color = upArrowNormalColor;
            }
            else
            {
                reactorUpgradeButton.interactable = false;
                reactorUpArrow.color = upArrowGreyColor;
            }
        }
    }

    public float ReactorUpgradeCost { get => reactorUpgradeCost; set { reactorUpgradeCost = value; reactorUpgradeCostValue.text = (int)reactorUpgradeCost + " T"; } }

    public float ReactorLevel { get => reactorLevel; set { reactorLevel = value; reactorLevelValue.text = "lv. " + reactorLevel; } }

    public int CrisisLevel { get => crisisLevel; set { crisisLevel = value; crisisLevelValue.text = "" + crisisLevel; } }

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
        if ((int)PowerOutput > WeaponPower + ShieldPower)
        {
            WeaponPower += (int)PowerOutput - (WeaponPower + ShieldPower);
        }
        else if ((int)PowerOutput < WeaponPower + ShieldPower)
        {

            int powerDebt = (WeaponPower + ShieldPower) - (int)PowerOutput;
            ShieldPower -= powerDebt;
            if (ShieldPower < 0)
            {
                WeaponPower += ShieldPower;
                ShieldPower = 0;
            }
        }
    }

    private void ChangeFireRate()
    {
        FireRate = WeaponPower * 0.3f;
        float fireInterval = 1 / FireRate;
        turret1.FireRate = fireInterval;
        turret2.FireRate = fireInterval;
        turret3.FireRate = fireInterval;
        turret4.FireRate = fireInterval;
        turret1.shoot();
        turret2.shoot();
        turret3.shoot();
        turret4.shoot();
    }

    private void RegenShield()
    {
        Shield += shieldPower * UITickRate;
    }

    private void ChangeShieldSprite()
    {
        shieldSprite.SetActive(Shield > 0.5);
        shieldSprite.GetComponent<SpriteRenderer>().color = Color.Lerp(shieldSpriteDangerColor, Color.white, Shield / 100);
    }

    private void ChangePlanetSprite()
    {
        planetSprite.color = Color.Lerp(Color.white, shieldSpriteDangerColor, Devestation / 100);
    }

    // Start is called before the first frame update
    void Start()
    {
        CoreTemperture = 300;
        equilibriumTemperature = CoreTemperture;
        PowerOutput = basePower;
        controlRodInsertion = 100;

        Devestation = 0;
        Shield = 100;
        FireRate = 1;
        WeaponPower = 1;
        ShieldPower = 1;

        ReactorUpgradeCost = 100f;
        ReactorLevel = 0;
        reactorUpgradeButton.interactable = false;

        CrisisLevel = 0;
        crisisLevelUpCountDown = crisisLevelUpTime;
        RecalculateSpawnRate();

        InvokeRepeating(nameof(ChangeTemperature), 0f, UITickRate);
        InvokeRepeating(nameof(ChangeStructuralDamage), 0f, UITickRate);
        InvokeRepeating(nameof(ChangePowerOutput), 0f, UITickRate);
        InvokeRepeating(nameof(ChangeShieldSprite), 0f, UITickRate);
        InvokeRepeating(nameof(ChangePlanetSprite), 0f, UITickRate);
        InvokeRepeating(nameof(RegenShield), 0f, UITickRate);
        InvokeRepeating(nameof(ChangeTimerText), 0f, UITickRate);
    }

    private void RecalculateSpawnRate()
    {
        AsteroidSpawner.SpawnRate = 1 / (baseSpawnRate * Mathf.Pow(crisisMultiplier, crisisLevel));
        AsteroidSpawnerSpawnRate = AsteroidSpawner.SpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            IncreaseWeaponPower();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            IncreaseShieldPower();
        }

        timeLeft -= Time.deltaTime;
        if (!gameWon && timeLeft < 0)
        {
            gameWon = true;
            victoryPanel.SetActive(true);
            Invoke(nameof(TurnOffVictoryPanel), 5f);
        }

        crisisLevelUpCountDown -= Time.deltaTime;
        if (crisisLevelUpCountDown < 0)
        {
            crisisLevelUpCountDown = crisisLevelUpTime;
            CrisisLevel++;
            RecalculateSpawnRate();
        }
    }

    public void IncreaseWeaponPower()
    {
        if (shieldPower > 0)
        {
            WeaponPower++;
            ShieldPower--;
        }
    }

    public void IncreaseShieldPower()
    {
        if (WeaponPower > 0)
        {
            WeaponPower--;
            ShieldPower++;
        }
    }

    public void UpgradeReactor()
    {
        ReactorLevel++;
        Mineral -= ReactorUpgradeCost;
        ReactorUpgradeCost *= 1.3f;
        basePower *= 1.2f;
        maxPower *= 1.2f;
        ChangePowerOutput();
    }

    public void ChangeTimerText()
    {
        timerText.text = Mathf.Round(timeLeft) + "s";
    }

    public void TurnOffVictoryPanel()
    {
        victoryPanel.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            int damage = collision.gameObject.GetComponent<Asteroid>().damage;
            if (Shield > 0)
            {
                Shield -= damage;
                if (shield < 0)
                {
                    Devestation -= Shield;
                    Shield = 0;
                }
            }
            else
            {
                Devestation += damage;
            }
        }
    }

}
