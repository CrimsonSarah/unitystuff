using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawn : MonoBehaviour {
    public GameObject[] castlePrefab = new GameObject[2];
    public GameObject[] playerPrefab = new GameObject[3];
    public GameObject upgradekeeperPrefab, shopkeeperPrefab, hudPrefab, ItemsPrefab, playerInstance, upgradekeeperInstance, shopkeeperInstance, hudInstance, castleInstance, ItemsInstance, castleBox, shopBox, upgradeBox, enemyBox, director;
    public LevelBar LevelBar;
    public CastleLifeBar CastleHealthBar, CastleBoxHealthBar;
    public castleUpgrades CastleUpgradesBox;
    public PlayerLevelDisplay CharLevel;
    public PlayerLifeBar PlayerHealthBar;
    public PlayerManaBar PlayerManaBar;
    public EnemyLifeBar EnemyHealthBar;
    public EnemyManaBar EnemyManaBar;
    public SkillCooldown SkillBox;
    public StatusValues StatusBox;
    public ExperienceBar ExpBar;
    public gold GoldValue;
    public camera MainCamera;
    public Vector3 upgradekeeperPosition, shopkeeperPosition, castlePosition;
    public Quaternion upgradekeeperRotation, shopkeeperRotation, castleRotation;
    public skills skills;
    public player player, keep;
    public ShowSkillsRange ShowSkillsRange;
    public skillButton skillButton;
    public upgradekeeper upgradekeeper;
    public shopkeeper shopkeeper;
    public Items Items;
    public castle castle;
    public pauseMenu pauseMenu;
    public int selectedCastle, selectedHero;

    void Start () {
        director = GameObject.Find ("Director");
        selectedCastle = director.GetComponent<characters>().selectedCastle;
        for (int i = 0; i < director.GetComponent<characters>().charList.Count; i++) {
            if (director.GetComponent<characters>().charList[i].IsOn) selectedHero = i;
        }

        Time.timeScale = 1f;
        playerInstance = Instantiate (playerPrefab[selectedHero], transform.position, transform.rotation);
        hudInstance = Instantiate (hudPrefab, Vector3.zero, new Quaternion (0, 0, 0, 0));
        hudInstance.name = "Hud";
        ItemsInstance = Instantiate (ItemsPrefab, Vector3.zero, new Quaternion (0, 0, 0, 0));
        ItemsInstance.name = "Items";
        upgradekeeperInstance = Instantiate (upgradekeeperPrefab, upgradekeeperPosition, upgradekeeperRotation);
        upgradekeeperInstance.name = "Upgradekeeper";
        shopkeeperInstance = Instantiate (shopkeeperPrefab, shopkeeperPosition, shopkeeperRotation);
        shopkeeperInstance.name = "Shopkeeper";
        castleInstance = Instantiate (castlePrefab[selectedCastle], castlePosition, castleRotation);
        castleInstance.name = "castle";

        castleBox = GameObject.Find ("CastleBox");
        shopBox = GameObject.Find ("ShopBox");
        upgradeBox = GameObject.Find ("UpgradeBox");
        enemyBox = GameObject.Find ("EnemyBox");
        pauseMenu = GameObject.Find ("PauseMenu").GetComponent<pauseMenu>();

        player = playerInstance.GetComponent<player> ();
        Items = ItemsInstance.GetComponent<Items> ();
        skills = playerInstance.GetComponent<skills> ();
        ShowSkillsRange = GameObject.Find ("Ranges").GetComponent<ShowSkillsRange> ();
        skillButton = GameObject.Find ("SkillBox").GetComponent<skillButton> ();

        skills.skillButton = new Image[5] { GameObject.Find ("SkillQIcon").GetComponent<Image> (), GameObject.Find ("SkillWIcon").GetComponent<Image> (), GameObject.Find ("SkillEIcon").GetComponent<Image> (), GameObject.Find ("UltIcon").GetComponent<Image> (), GameObject.Find ("SkillPIcon").GetComponent<Image> () };

        player.spawn = this;
        player.skills = skills;
        player.Items = Items;
        player.castleBox = castleBox;
        player.shopBox = shopBox;
        player.upgradeBox = upgradeBox;
        player.enemyBox = enemyBox;
        player.characters = director.GetComponent<characters> ();
        Items.Player = player;

        skillButton.rangeScript = ShowSkillsRange;
        skillButton.player = player;

        ShowSkillsRange.character = player;
        ShowSkillsRange.Items = Items;
        ShowSkillsRange.skillRanges = new RectTransform[6] { GameObject.Find ("QRange").GetComponent<RectTransform> (), GameObject.Find ("WRange").GetComponent<RectTransform> (), GameObject.Find ("ERange").GetComponent<RectTransform> (), GameObject.Find ("RRange").GetComponent<RectTransform> (), GameObject.Find ("PRange").GetComponent<RectTransform> (), new RectTransform () };
        ShowSkillsRange.skillRadiuses = new RectTransform[5] { GameObject.Find ("QRadius").GetComponent<RectTransform> (), GameObject.Find ("WRadius").GetComponent<RectTransform> (), GameObject.Find ("ERadius").GetComponent<RectTransform> (), GameObject.Find ("RRadius").GetComponent<RectTransform> (), GameObject.Find ("PRadius").GetComponent<RectTransform> () };

        upgradekeeper = GameObject.FindGameObjectWithTag ("Upgrade Core").GetComponent<upgradekeeper> ();
        upgradekeeper.upgradeCostText = new Text[5] { GameObject.Find ("QCost").GetComponent<Text> (), GameObject.Find ("WCost").GetComponent<Text> (), GameObject.Find ("ECost").GetComponent<Text> (), GameObject.Find ("RCost").GetComponent<Text> (), GameObject.Find ("PCost").GetComponent<Text> () };
        upgradekeeper.upgradeButton = new Button[5] { GameObject.Find ("QUpgradeButton").GetComponent<Button> (), GameObject.Find ("WUpgradeButton").GetComponent<Button> (), GameObject.Find ("EUpgradeButton").GetComponent<Button> (), GameObject.Find ("RUpgradeButton").GetComponent<Button> (), GameObject.Find ("PUpgradeButton").GetComponent<Button> () };
        upgradekeeper.player = player;
        upgradekeeper.characters = director.GetComponent<characters> ();

        shopkeeper = GameObject.FindGameObjectWithTag ("Shop Core").GetComponent<shopkeeper> ();
        shopkeeper.itemPrices = new Text[3] { GameObject.Find ("Item1Price").GetComponent<Text> (), GameObject.Find ("Item2Price").GetComponent<Text> (), GameObject.Find ("Item3Price").GetComponent<Text> () };
        shopkeeper.buttons = new Button[3] { GameObject.Find ("Item1Button").GetComponent<Button> (), GameObject.Find ("Item2Button").GetComponent<Button> (), GameObject.Find ("Item3Button").GetComponent<Button> () };
        shopkeeper.Player = player;
        shopkeeper.Items = Items;

        CastleUpgradesBox = GameObject.Find ("CastleUpgradesBox").GetComponent<castleUpgrades> ();
        CastleUpgradesBox.upgradeCostText = new Text[3] { GameObject.Find ("CastleUpgrade1Cost").GetComponent<Text> (), GameObject.Find ("CastleUpgrade3Cost").GetComponent<Text> (), GameObject.Find ("CastleUpgrade2Cost").GetComponent<Text> () };

        LevelBar = GameObject.Find ("LevelBar").GetComponent<LevelBar> ();
        CastleHealthBar = GameObject.Find ("CastleHealthBar").GetComponent<CastleLifeBar> ();
        CastleBoxHealthBar = GameObject.Find ("CastleBoxHealthBar").GetComponent<CastleLifeBar> ();
        CharLevel = GameObject.Find ("CharLevel").GetComponent<PlayerLevelDisplay> ();
        PlayerHealthBar = GameObject.Find ("PlayerHealthBar").GetComponent<PlayerLifeBar> ();
        PlayerManaBar = GameObject.Find ("PlayerManaBar").GetComponent<PlayerManaBar> ();
        EnemyHealthBar = GameObject.Find ("EnemyHealthBar").GetComponent<EnemyLifeBar> ();
        EnemyManaBar = GameObject.Find ("EnemyManaBar").GetComponent<EnemyManaBar> ();
        SkillBox = GameObject.Find ("SkillBox").GetComponent<SkillCooldown> ();
        StatusBox = GameObject.Find ("StatusBox").GetComponent<StatusValues> ();
        ExpBar = GameObject.Find ("ExpBar").GetComponent<ExperienceBar> ();
        GoldValue = GameObject.Find ("GoldValue").GetComponent<gold> ();
        MainCamera = GameObject.Find ("Main Camera").GetComponent<camera> ();

        castle = GameObject.FindGameObjectWithTag ("Castle Core").GetComponent<castle> ();
        castle.GameOverMenu = GameObject.Find ("GameOverMenu");
        LevelBar.castle = castle;
        CastleHealthBar.castle = castle;
        CastleBoxHealthBar.castle = castle;
        CastleUpgradesBox.castle = castle;
        CastleUpgradesBox.player = player;
        CastleUpgradesBox.castleObject = castle.gameObject;
        CastleUpgradesBox.character = playerInstance;
        CharLevel.player = player;
        PlayerHealthBar.player = player;
        PlayerManaBar.player = player;
        EnemyHealthBar.player = player;
        EnemyManaBar.player = player;
        SkillBox.player = player;
        StatusBox.player = player;
        ExpBar.player = player;
        GoldValue.player = player;
        MainCamera.player = player.transform;

        skills.player = player;
        skills.Items = Items;

        GameObject.Find ("SkillBox").GetComponent<SkillCooldown> ().items = Items;

        pauseMenu.UpgradeBox = upgradeBox;
        pauseMenu.CastleBox = castleBox;
        pauseMenu.ShopBox = shopBox;
        pauseMenu.tooltip = GameObject.Find("Director").GetComponent<Tooltip>();

        castleBox.SetActive (false);
        shopBox.SetActive (false);
        upgradeBox.SetActive (false);
        enemyBox.SetActive (false);

        
        for (int i = 0; i < director.GetComponent<wavespawner>().spawnpoints.Length; i++) {
            director.GetComponent<wavespawner>().spawnpoints[i] = GameObject.Find ("point" + (i + 1)).transform;
        }
        director.GetComponent<wavespawner>().spawnCredits = director.GetComponent<wavespawner>().maxCredits;
        director.GetComponent<wavespawner>().waveCredits = director.GetComponent<wavespawner>().maxCredits / 2f;
        director.GetComponent<wavespawner>().countdown = 0;
        director.GetComponent<wavespawner>().waveindex = -1;

        director.GetComponent<Tooltip>().tooltipGO = GameObject.Find ("Tooltip");
        director.GetComponent<Tooltip>().text[0] = GameObject.Find ("TooltipText").GetComponent<Text> ();
        director.GetComponent<Tooltip>().text[1] = GameObject.Find ("TooltipSkillManaCost").GetComponent<Text> ();
        director.GetComponent<Tooltip>().text[2] = GameObject.Find ("TooltipSkillCooldown").GetComponent<Text> ();

        director.GetComponent<Tooltip>().tooltipGO.SetActive (false);
    }

    public IEnumerator Respawn () {
        yield return new WaitForSeconds (5f);
        playerInstance = Instantiate (playerPrefab[selectedHero], transform.position, transform.rotation);
        player = playerInstance.GetComponent<player> ();
        player.janasceu = true;
        player.characters = director.GetComponent<characters> ();
        //Gambiarra
        player.selectedChar = keep.selectedChar;
        player.charInstance = keep.charInstance;
        player.characterHealth = keep.characterHealth;
        player.characterMana = keep.characterMana;
        player.characterAttributes = keep.characterAttributes;
        player.characterExp = keep.characterExp;
        player.skillCooldown = keep.skillCooldown;
        player.skillMaxCooldown = keep.skillMaxCooldown;
        player.skillPower = keep.skillPower;
        player.skillManaCost = keep.skillManaCost;
        player.skillRange = keep.skillRange;
        player.skillRadius = keep.skillRadius;
        player.delayCounters = keep.delayCounters;
        player.skillLevel = keep.skillLevel;
        player.skillEnabled = keep.skillEnabled;
        player.armor = keep.armor;
        player.basicDamage = keep.basicDamage;
        player.attackRange = keep.attackRange;
        player.attackSpeed = keep.attackSpeed;
        GameObject.Destroy (keep);
        GameObject.Destroy (skills);
        GameObject.Destroy (ShowSkillsRange);
        ShowSkillsRange = player.transform.GetChild (1).gameObject.GetComponent<ShowSkillsRange> ();
        skills = playerInstance.GetComponent<skills> ();
        skills.player = player;
        skills.Items = Items;
        skills.skillButton = new Image[5] { GameObject.Find ("SkillQIcon").GetComponent<Image> (), GameObject.Find ("SkillWIcon").GetComponent<Image> (), GameObject.Find ("SkillEIcon").GetComponent<Image> (), GameObject.Find ("UltIcon").GetComponent<Image> (), GameObject.Find ("SkillPIcon").GetComponent<Image> () };

        //Gambiarra
        player.spawn = this;
        player.skills = skills;
        player.Items = Items;
        player.castleBox = castleBox;
        player.shopBox = shopBox;
        player.upgradeBox = upgradeBox;
        player.enemyBox = enemyBox;
        upgradekeeper.characters = director.GetComponent<characters> ();
        CastleUpgradesBox.character = playerInstance;
        CastleUpgradesBox.player = player;
        CharLevel.player = player;
        PlayerHealthBar.player = player;
        PlayerManaBar.player = player;
        EnemyHealthBar.player = player;
        EnemyManaBar.player = player;
        SkillBox.player = player;
        StatusBox.player = player;
        ExpBar.player = player;
        GoldValue.player = player;
        MainCamera.player = player.transform;
        upgradekeeper.player = player;
        shopkeeper.Player = player;
        Items.Player = player;
        ShowSkillsRange.character = player;
        ShowSkillsRange.Items = Items;
        ShowSkillsRange.skillRanges = new RectTransform[6] { player.transform.GetChild (1).GetChild (0).gameObject.GetComponent<RectTransform> (), player.transform.GetChild (1).GetChild (1).gameObject.GetComponent<RectTransform> (), player.transform.GetChild (1).GetChild (2).gameObject.GetComponent<RectTransform> (), player.transform.GetChild (1).GetChild (3).gameObject.GetComponent<RectTransform> (), player.transform.GetChild (1).GetChild (4).gameObject.GetComponent<RectTransform> (), player.transform.GetChild (1).GetChild (5).gameObject.GetComponent<RectTransform> () };
        ShowSkillsRange.skillRadiuses = new RectTransform[5] { player.transform.GetChild (1).GetChild (0).GetChild (0).gameObject.GetComponent<RectTransform> (), player.transform.GetChild (1).GetChild (1).GetChild (0).gameObject.GetComponent<RectTransform> (), player.transform.GetChild (1).GetChild (2).GetChild (0).gameObject.GetComponent<RectTransform> (), player.transform.GetChild (1).GetChild (3).GetChild (0).gameObject.GetComponent<RectTransform> (), player.transform.GetChild (1).GetChild (4).GetChild (0).gameObject.GetComponent<RectTransform> () };
        skillButton.rangeScript = ShowSkillsRange;
        skillButton.player = player;
        player.characterHealth[1] = player.characterHealth[0];
        player.characterMana[1] = player.characterMana[0];
    }

    public void Die () {
        keep = player;
        StartCoroutine (Respawn ());
        GameObject.Destroy (player.gameObject);
    }
}