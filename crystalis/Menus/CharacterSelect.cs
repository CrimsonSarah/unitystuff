using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour {
    public string menuScene, gameScene;
    private string[, ] skillInfo = new string[3, 5];
    [SerializeField]
    private Button SirenaButton, DefaultButton, OffensiveCastleButton;
    private Button start;
    private int characterIndex;
    private GameObject director, CharactersInfo, CastlesInfo, CharactersRoster, CastlesRoster;
    private UnlockablesManager unlockableManager;
    private characters characters;
    private spawn spawn;
    private Text skillText, castleText, difficultyText;
    private Text[] charAttribute = new Text[3];
    private Image[] skillImage = new Image[5];
    [SerializeField]
    private Sprite[] hogSprites = new Sprite[5];
    [SerializeField]
    private Sprite[] defaultSprites = new Sprite[5];
    [SerializeField]
    private Sprite[] sirenaSprites = new Sprite[5];

    void Awake () {
        characterIndex = -1;
        director = GameObject.Find ("Director");
        unlockableManager = director.GetComponent<UnlockablesManager>();
        characters = director.GetComponent<characters> ();
        CharactersInfo = GameObject.Find("CharactersInfo");
        CharactersRoster = GameObject.Find("Characters");
        CastlesInfo = GameObject.Find("CastlesInfo");
        CastlesRoster = GameObject.Find("Castles");
        start = GameObject.Find ("StartButton").GetComponent<Button> ();
        DontDestroyOnLoad (director);

        start.interactable = false;

        skillImage[0] = GameObject.Find ("QInfo").GetComponent<Image> ();
        skillImage[1] = GameObject.Find ("WInfo").GetComponent<Image> ();
        skillImage[2] = GameObject.Find ("EInfo").GetComponent<Image> ();
        skillImage[3] = GameObject.Find ("RInfo").GetComponent<Image> ();
        skillImage[4] = GameObject.Find ("PInfo").GetComponent<Image> ();

        charAttribute[0] = GameObject.Find ("CharStrength").GetComponent<Text> ();
        charAttribute[1] = GameObject.Find ("CharDexterity").GetComponent<Text> ();
        charAttribute[2] = GameObject.Find ("CharIntelligence").GetComponent<Text> ();

        skillText = GameObject.Find ("SkillText").GetComponent<Text> ();
        difficultyText = GameObject.Find ("DifficultyText").GetComponent<Text> ();
        castleText = GameObject.Find ("CastleText").GetComponent<Text> ();

        skillInfo[0, 0] = "Causa dano na área alvo.";
        skillInfo[0, 1] = "Cura um aliado.";
        skillInfo[0, 2] = "Causa dano aos inimigos em volta.";
        skillInfo[0, 3] = "Atira um projétil de alcance global que explode ao entrar em contato com um inimigo, causando dano aos inimigos no raio da explosão.";
        skillInfo[0, 4] = "Permite que Default ataque sem esperar seu intervalo de ataque de tempos em tempos.";
        skillInfo[1, 0] = "Caso Hog esteja com seu martelo, ele irá jogá-lo na área alvo causando dano e atordoamente aos inimigos pegos. O martelo irá ficar no chão até ser recolhido.";
        skillInfo[1, 1] = "Hog ganha armadura de acordo com sua vida perdida.";
        skillInfo[1, 2] = "Hog solta um grito de guerra. Caso ele esteja com seu martelo, Hog e todos aliados no raio do grito recebem um bônus de dano. Caso ele esteja sem martelo, Hog e todos aliados no raio do grito recebem regeneração de vida adicional.";
        skillInfo[1, 3] = "Caso Hog esteja com seu martelo, ele irá dar uma fortíssima pancada no chão, causando dano aos oponentes no raio do golpe, e deixando seu martelo preso no chão por um curto período de tempo. Caso Hog esteja sem seu martelo, ele irá entrar em estado de fúria, ganhando dano e roubo de vida.";
        skillInfo[1, 4] = "Caso Hog esteja com seu martelo, seus ataques básicos causaram dano diminuido e em área. Caso ele esteja sem seu martelo, seus ataques básicos causaram mais dano e sua velocidade de ataque será maior.";
        skillInfo[2, 0] = "Sirena cria uma esfera no local alvo, causando dano aos inimigos na área. Sirena pode manter diversas esferas em campo ao mesmo tempo. A esfera mais perto de Sirena é considerada ativa. Criar uma esfera além do limite fará com que a primeira desapareça.";
        skillInfo[2, 1] = "Sirena usa sua esfera ativa para puxar inimigos e causar dano a eles.";
        skillInfo[2, 2] = "Sirena usa sua esfera ativa para repelir inimigos e curar aliados na área.";
        skillInfo[2, 3] = "Sirena sobrecarrega todas esferas em campo, fazendo com que elas puxem inimigos e causem dano a eles, e repele os mesmos logo após. Aliados na área são curados.";
        skillInfo[2, 4] = "Sirena solta raios em seus ataques básicos, os raios desencadeiam várias vezes entre seus inimigos causando metade do dano de ataque a cada golpe. Os raios podem desencadear mais de uma vez no mesmo inimigo.";

        CastlesRoster.SetActive(false);
        CharactersInfo.SetActive(false);
        CastlesInfo.SetActive(false);
    }

    void Start () {
        unlockableManager.LoadJson();
        if (unlockableManager.unlockableMatrix.SirenaUnlocked) SirenaButton.interactable = true;
        if (unlockableManager.unlockableMatrix.OffensiveCastleUnlocked) OffensiveCastleButton.interactable = true;
        if (unlockableManager.unlockableMatrix.DefaultUnlocked) DefaultButton.interactable = true;
    }

    public void BackButton () {
        SceneManager.LoadScene (menuScene);
    }

    public void StartButton () {
        SceneManager.LoadScene (gameScene);
    }

    public void Heroes () {
        if (CastlesRoster.activeSelf) CastlesRoster.SetActive(false);
        if (!CharactersRoster.activeSelf) CharactersRoster.SetActive(true);
        if (CastlesInfo.activeSelf) CastlesInfo.SetActive(false);
        if (!CharactersInfo.activeSelf && characterIndex != -1) CharactersInfo.SetActive(true);
    }

    public void Castles () {
        if (CharactersRoster.activeSelf) CharactersRoster.SetActive(false);
        if (!CastlesRoster.activeSelf) CastlesRoster.SetActive(true);
        if (CharactersInfo.activeSelf) CharactersInfo.SetActive(false);
        if (!CastlesInfo.activeSelf) CastlesInfo.SetActive(true);
    }

    public void DefaultCastle () {
        if (CharactersInfo.activeSelf) CharactersInfo.SetActive(false);
        if (!CastlesInfo.activeSelf) CastlesInfo.SetActive(true);
        characters.selectedCastle = 0;
        castleText.text = "O Crystalis comum. Possui armadura, regeneração de vida e vida máxima medíocres, mas todos estes atributos podem ser aprimorados." + "\n" + "\n" + "Transformação 1: triplica a vida máxima do Crystalis e cura toda vida perdida." + "\n" + "\n" + "Transformação 2: ganha 25 de armadura." + "\n" + "\n" + "Transformação 3: dobra a regeneração de vida e ganha uma aura de cura.";
    }

    public void OffensiveCastle () {
        if (CharactersInfo.activeSelf) CharactersInfo.SetActive(false);
        if (!CastlesInfo.activeSelf) CastlesInfo.SetActive(true);
        characters.selectedCastle = 1;
        castleText.text = "O Crystalis ofensivo. Possui armadura e vida máxima médias, e regeneração de vida nula. Entretanto, ele se defende sozinho atacando inimigos por perto e se cura sugando a vida dos mesmos." + "\n" + "\n" + "Transformação 1: o limite de alvos aumenta em 3." + "\n" + "\n" + "Transformação 2: ataca duas vezes cada alvo." + "\n" + "\n" + "Transformação 3: recebe 100% e uma aura de roubo de vida.";
    }

    public void Default () {
        if (CastlesInfo.activeSelf) CastlesInfo.SetActive(false);
        if (!CharactersInfo.activeSelf) CharactersInfo.SetActive(true);
        characterIndex = 0;
        characters.charList[0].IsOn = true;
        characters.charList[1].IsOn = false;
        characters.charList[2].IsOn = false;
        start.interactable = true;
        skillText.text = "";
        for (int i = 0; i < skillImage.Length; i++) skillImage[i].sprite = defaultSprites[i];
        for (int i = 0; i < charAttribute.Length; i++) charAttribute[i].text = characters.charList[0].CharAttributePerLvl[i].ToString ("N0");
    }

    public void Hog () {
        if (CastlesInfo.activeSelf) CastlesInfo.SetActive(false);
        if (!CharactersInfo.activeSelf) CharactersInfo.SetActive(true);
        characterIndex = 1;
        characters.charList[0].IsOn = false;
        characters.charList[1].IsOn = true;
        characters.charList[2].IsOn = false;
        start.interactable = true;
        skillText.text = "";
        for (int i = 0; i < skillImage.Length; i++) skillImage[i].sprite = hogSprites[i];
        for (int i = 0; i < charAttribute.Length; i++) charAttribute[i].text = characters.charList[1].CharAttributePerLvl[i].ToString ("N0");
    }

    public void Sirena () {
        if (CastlesInfo.activeSelf) CastlesInfo.SetActive(false);
        if (!CharactersInfo.activeSelf) CharactersInfo.SetActive(true);
        characterIndex = 2;
        characters.charList[0].IsOn = false;
        characters.charList[1].IsOn = false;
        characters.charList[2].IsOn = true;
        start.interactable = true;
        skillText.text = "";
        for (int i = 0; i < skillImage.Length; i++) skillImage[i].sprite = sirenaSprites[i];
        for (int i = 0; i < charAttribute.Length; i++) charAttribute[i].text = characters.charList[2].CharAttributePerLvl[i].ToString ("N0");
    }

    public void SelectQ () {
        if (characterIndex != -1) skillText.text = skillInfo[characterIndex, 0];
        else skillText.text = "";
    }

    public void SelectW () {
        if (characterIndex != -1) skillText.text = skillInfo[characterIndex, 1];
        else skillText.text = "";
    }

    public void SelectE () {
        if (characterIndex != -1) skillText.text = skillInfo[characterIndex, 2];
        else skillText.text = "";
    }

    public void SelectR () {
        if (characterIndex != -1) skillText.text = skillInfo[characterIndex, 3];
        else skillText.text = "";
    }

    public void SelectP () {
        if (characterIndex != -1) skillText.text = skillInfo[characterIndex, 4];
        else skillText.text = "";
    }

    public void Easy () {
        director.GetComponent<Manager> ().difficulty = 1;
        difficultyText.text = "Easy";
    }

    public void Normal () {
        director.GetComponent<Manager> ().difficulty = 2;
        difficultyText.text = "Normal";
    }

    public void Hard () {
        director.GetComponent<Manager> ().difficulty = 3;
        difficultyText.text = "Hard";
    }
}