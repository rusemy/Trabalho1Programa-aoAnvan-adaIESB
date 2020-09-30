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
			itemTimer.color = new Color(40, 50, 50, 50);
		}
		else
		{
			itemImage.sprite = player.availablePowerUp.icon;
			itemTimer.color = new Color(250, 250, 200, 50);
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
		for (int i = 2; i >= 0; i--)
		{
			for (int j = 0; j < 8; j++)
			{
				if (GameManager.Instance.ranking[i, j] > 1)
				{
					atualPosition++;
				}
				else if (GameManager.Instance.ranking[i, j] == 1)
				{
					i = 0;
					j = 8;
				}
			}
		}
		positionText.text = atualPosition.ToString() + " º";
	}
	private void LapTextUpdate()
	{
		LapText.text = (player.lapNumber + 1).ToString() + " / 3";
	}

}