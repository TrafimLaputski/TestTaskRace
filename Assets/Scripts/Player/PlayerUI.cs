using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _bonusImage = null;
    [SerializeField] private SpriteRenderer _bonusBacklight = null;
    [SerializeField] private TextMeshProUGUI _bonusText = null;
    [SerializeField] private Image _bonusBar = null;
    [SerializeField] private Image _healthBar = null;
    [SerializeField] private GameObject _bonusBarObject = null;
    [SerializeField] private TextMeshProUGUI _scoreText = null;
    [SerializeField] private GameObject _finalMenu = null;
    [SerializeField] private TextMeshProUGUI _finalScore = null;
    [SerializeField] private TextMeshProUGUI _bestScore = null;

    public bool Ready
    {
        get { return !_bonusBarObject.activeInHierarchy; }
    }

    public void ChangeHealth(float health, float currentHealth)
    {
        float value = currentHealth / health;
        _healthBar.fillAmount = value;
    }

    public void ChangeBonus(float max, float current)
    {
        float value = current / max;
        _bonusBar.fillAmount = value;
    }

    public void SetBonus(ItemData itemData)
    {
        _bonusBarObject.SetActive(true);
        _bonusText.text = itemData.itemName;
        _bonusBar.sprite = itemData.barSprite;
        _bonusBar.fillAmount = 1;
    }

    public void SetBonusVisual(ItemData data)
    {
        _bonusImage.gameObject.SetActive(true);
        _bonusBacklight.gameObject.SetActive(true);

        _bonusImage.sprite = data.itemSprites[0];
        _bonusBacklight.sprite = data.itemBacklight;
    }

    public void ChandeScore(int score)
    {
        _scoreText.text = "Score:\n" + score;
    }
    public void Clear()
    {
        _bonusText.text = "";
        _bonusBarObject.SetActive(false);

        _bonusImage.gameObject.SetActive(false);
        _bonusBacklight.gameObject.SetActive(false);
    }

    public void ShowFinalMenu(int score, int bestScore)
    {
        _finalMenu.SetActive(true);
        _finalScore.text = "Your score:\n" + score;
        _bestScore.text = "Best score:\n" + bestScore;
    }
}
