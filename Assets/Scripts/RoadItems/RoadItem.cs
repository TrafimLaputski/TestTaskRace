using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoadItem : MonoBehaviour
{
    public Action<RoadItem> ReturnToPool; 
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    
    private ItemData _itemData = null;

    public void Construct(ItemData itemData)
    {
        _itemData = itemData;
        int rund = UnityEngine.Random.Range(0, itemData.itemSprites.Count);
        _spriteRenderer.sprite = itemData.itemSprites[rund];
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        ItemPickUp(collision);
    }

    private void ItemPickUp(Collider2D collision)
    {
        PlayerMagnet playerMagnet = collision.GetComponent<PlayerMagnet>();

        if (playerMagnet != null)
        {
            switch (_itemData.itemType)
            {
            case ItemType.Heart:
                playerMagnet.AddGravityObject(this);
                break;
            case ItemType.Nutro:
                playerMagnet.AddGravityObject(this);
                break;
            case ItemType.Coin:
                playerMagnet.AddGravityObject(this);
                break;
            case ItemType.Shield:
                playerMagnet.AddGravityObject(this);
                break;
            case ItemType.Magnet:
                playerMagnet.AddGravityObject(this);
                break;
            default:
                break;
            }

            return; 
        }

        PlayerCar playerCar = collision.GetComponent<PlayerCar>();

        if (playerCar != null)
        { 
            switch (_itemData.itemType)
            {
                case ItemType.Oil:
                    playerCar.MoveSpeed = _itemData.itemValue;
                    playerCar.BonusController.SetSpeedBonus(_itemData);
                    break;
                case ItemType.Block:
                    playerCar.TakeDamage(_itemData.itemValue);
                    break;
                case ItemType.Heart:
                    playerCar.TakeDamage(-_itemData.itemValue);
                    ItemReturn();
                    break;
                case ItemType.Nutro:
                    playerCar.MoveSpeed = 1;
                    playerCar.BonusController.SetSpeedBonus(_itemData);
                    ItemReturn();
                    break;
                case ItemType.Crack:
                    playerCar.MoveSpeed = _itemData.itemValue;
                    playerCar.TakeDamage(_itemData.itemValue);
                    break;
                case ItemType.Coin:
                    playerCar.TakeCoins(_itemData.itemValue);
                    ItemReturn();
                    break;
                case ItemType.Shield:
                    playerCar.BonusController.SetShield(_itemData);
                    ItemReturn();
                    break;
                case ItemType.Magnet:
                    playerCar.BonusController.SetMagnet(_itemData);
                    ItemReturn();
                    break;
                default:
                    break;
            }
        }
    }

    public void ItemReturn()
    {
        ReturnToPool?.Invoke(this);
    }
}