using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MFarm.Inventory
{
    public class SlotUI : MonoBehaviour
    {
        [Header("�����ȡ")]
        [SerializeField] private Image slotImage;
        [SerializeField] private TextMeshProUGUI amountText;
        public Image slotHightlight;
        [SerializeField] private Button button;

        [Header("��������")]
        public SlotType slotType;
        public bool isSelected;
        public int slotIndex;

        //��Ʒ��Ϣ
        public ItemDetails itemDetails;
        public int itemAmount;

        private void Start()
        {
            isSelected = false;
            if (itemDetails.itemID == 0)
            {
                UpdateEmptySlot();
            }
        }

        /// <summary>
        /// ���¸���UI����Ϣ
        /// </summary>
        /// <param name="item">ItemDetails</param>
        /// <param name="amount">��������</param>
        public void UpdateSlot(ItemDetails item, int amount)
        {
            itemDetails = item;
            slotImage.sprite = item.itemIcon;
            itemAmount = amount;
            amountText.text = amount.ToString();
            slotImage.enabled = true;
            button.interactable = true;
        }

        /// <summary>
        /// ��Slot����Ϊ��
        /// </summary>
        public void UpdateEmptySlot()
        {
            if (isSelected)
            {
                isSelected = false;
            }
            itemDetails = null;
            slotImage.enabled = false;
            amountText.text = string.Empty;
            button.interactable = false;
        }
    }
}