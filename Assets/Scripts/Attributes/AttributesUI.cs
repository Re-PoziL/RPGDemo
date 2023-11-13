using RPG.Attributes;
using RPG.Combat;
using RPG.Control;
using RPG.Stats;
using TMPro;
using UnityEngine;

public class AttributesUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI playerHealthPercentText;
    [SerializeField] private TextMeshProUGUI EnemyHealthPercentText;
    [SerializeField] private TextMeshProUGUI playerEXText;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    private GameObject player;
    private Experience playerExperience;
    private Health playerHealth;
    private BaseStats baseStats;
    private void Awake()
    {
        player = GameObject.Find("Player");
        playerExperience = player.GetComponent<Experience>();
        playerHealth = player.GetComponent<Health>();
        baseStats = player.GetComponent<BaseStats>();
    }

    void Start()
    {
        playerEXText.text = playerExperience.GetExperience().ToString();
        playerLevelText.text = baseStats.GetLevel().ToString();
    }

    private void OnEnable()
    {
        playerExperience.OnGainExperience += Experience_OnGainExperience;
    }

    private void OnDisable()
    {
        playerExperience.OnGainExperience -= Experience_OnGainExperience;
    }

    private void Experience_OnGainExperience()
    {
        playerEXText.text = playerExperience.GetExperience().ToString();
        playerLevelText.text = baseStats.GetLevel().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthPercentText.text = string.Format("{0}/{1}", playerHealth.GetCurrentHealth(), playerHealth.GetMaxHealth());
    }

    private void LateUpdate()
    {

        EnemyHealthPercentText.text = player.GetComponent<Fighter>().GetTarget()?.GetComponent<Health>().GetHealthPercent().ToString("0") + "%";
    }
}
