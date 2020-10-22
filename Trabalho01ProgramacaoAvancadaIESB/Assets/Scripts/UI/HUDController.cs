using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI LapText;
	[SerializeField] private TextMeshProUGUI positionText;
	[SerializeField] private Image itemImage;
	[SerializeField] private Image itemTimer;
	[SerializeField] private Sprite emptyItemSprite;
	[SerializeField] private PlayerController player;

	private float timer;

	private void Update()
	{
		ItemImageUpdate();
		PositionTextUpdate();
		LapTextUpdate();
	}

	private void ItemImageUpdate()
	{
		timer -= Time.deltaTime;

		if (player.availablePowerUp == null)
		{
			itemImage.sprite = emptyItemSprite;
			itemImage.color = new Color(0.16f, 0.2f, 0.2f, 0.2f);
			itemTimer.fillAmount = 0f;
		}
		else
		{
			itemImage.sprite = player.availablePowerUp.icon;
			itemImage.color = Color.white;
			if (timer < 0.1f)
			{
				timer = player.availablePowerUp.duration;
			}
			itemTimer.fillAmount = timer / player.availablePowerUp.duration;

		}
	}

	private void PositionTextUpdate()
	{
		int atualPosition = 1;

		for (int j = 0; j < 8; j++)
		{
			if (GameManager.Instance.ranking[player.lapNumber, j] > 1)
			{
				atualPosition++;
			}
			else if (GameManager.Instance.ranking[player.lapNumber, j] == 1)
			{
				j = 8;
			}
		}
		positionText.text = atualPosition.ToString() + " º";
	}
	private void LapTextUpdate()
	{
		LapText.text = "Lap : " + (player.lapNumber + 1).ToString() + " / 3";
	}

}