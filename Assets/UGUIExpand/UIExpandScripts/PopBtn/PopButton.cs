using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace UnityEngine.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class PopButton : MonoBehaviour, IPointerClickHandler
    {
        public float Speed = 0.5f;
        [Serializable]
        public class OptionData
        {
            [SerializeField]
            private string m_Text;
            [SerializeField]
            private Sprite m_Image;

            public string text { get { return m_Text; } set { m_Text = value; } }

            public Sprite image { get { return m_Image; } set { m_Image = value; } }

            public OptionData()
            {
            }

            public OptionData(string text)
            {
                this.text = text;
            }

            public OptionData(Sprite image)
            {
                this.image = image;
            }
            public OptionData(string text, Sprite image)
            {
                this.text = text;
                this.image = image;
            }
        }

        public List<OptionData> _OptionList;
        public Button ItemButton;//按钮模板
        public Transform _Content;//按钮父物体
        public float WaitSec;//按钮展开间隔
        public string OpenAnimaName;//按钮打开动画名称
        public Animation btnRect;
        [Serializable] public class OptionsClick : UnityEvent<int> { }
        public OptionsClick OnOptionClick;
        private List<GameObject> ClickList = new List<GameObject>();
        private List<Animation> ItemAnimations = new List<Animation>();
        private bool isOpen = false;
        public bool AnimaSwitch;
        private Action CloseAction;
        private Action OpenAction;
        private void Start()
        {
            LoadButtons();
            _Content.gameObject.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {

            if (isOpen)
            {
                ClosePopBtn();
            }
            else
            {
                OpenPopBtn();
            }
        }
        /// <summary>
        /// 加载按钮
        /// </summary>
        private void LoadButtons()
        {
            for (int i = 0; i < _OptionList.Count; i++)
            {
                int j = i;
                GameObject item = GameObject.Instantiate(ItemButton.gameObject);
                SetBtnItem(j, item);
                ClickList.Add(item);
            }
        }
        /// <summary>
        /// 设置按钮
        /// </summary>
        /// <param name="j"></param>
        /// <param name="item"></param>
        private void SetBtnItem(int j, GameObject item)
        {
            item.transform.SetParent(_Content);
            Button ItemBtn = item.GetComponent<Button>();
            Animation ItemAni = item.GetComponent<Animation>();
            ItemAnimations.Add(ItemAni);
            ItemBtn.image.sprite = _OptionList[j].image;
            Text ItemText = item.GetComponentInChildren<Text>();
            if (ItemText != null)
            {
                ItemText.text = _OptionList[j].text;
            }
            ItemBtn.onClick.AddListener(() => { OnOptionClick.Invoke(j); });
        }
        /// <summary>
        /// 展开按钮
        /// </summary>
        private void OpenPopBtn()
        {
            if (AnimaSwitch)
            {
                btnRect["RAnima"].time = 0;
                btnRect["RAnima"].speed = 1.5f;
                btnRect.Play("RAnima");
            }
            isOpen = true;
            for (int i = 0; i < ClickList.Count; i++)
            {
                StartCoroutine(Open(i));
            }
        }
        /// <summary>
        /// 收回按钮
        /// </summary>
        private void ClosePopBtn()
        {
            if (AnimaSwitch)
            {
                btnRect["RAnima"].time = btnRect["RAnima"].clip.length;
                btnRect["RAnima"].speed = -1.5f;
                btnRect.Play("RAnima");
            }
            isOpen = false;
            for (int i = ClickList.Count - 1; i >= 0; i--)
            {
                StartCoroutine(Close(i));
            }
        }
        /// <summary>
        /// 直接关闭按钮，不执行动画
        /// </summary>
        public void SuperClose()
        {
            if (isOpen)
            {
                isOpen = false;
                for (int i = 0; i < ClickList.Count; i++)
                {
                    ClickList[i].transform.localScale = Vector3.zero;
                }
            }
        }

        private IEnumerator Open(int i)
        {
            yield return new WaitForSeconds(WaitSec * i);
            if (!ClickList[i].activeSelf)
            {
                ClickList[i].SetActive(true);
            }
            ItemAnimations[i][OpenAnimaName].time = 0;
            ItemAnimations[i][OpenAnimaName].speed = 1;
            ItemAnimations[i].Play(OpenAnimaName);
            StopCoroutine("Open");
            if (i == _OptionList.Count - 1)
            {
                OpenAction?.Invoke();
            }
        }

        private IEnumerator Close(int i)
        {
            yield return new WaitForSeconds(WaitSec * (ClickList.Count - i));
            ItemAnimations[i][OpenAnimaName].time = ItemAnimations[i][OpenAnimaName].clip.length;
            ItemAnimations[i][OpenAnimaName].speed = -1;
            ItemAnimations[i].Play(OpenAnimaName);
            StopCoroutine("Close");
            if (i == 0)
            {
                StartCoroutine(DoEvent());
            }
        }
        private IEnumerator DoEvent()
        {
            yield return new WaitForSeconds(Speed);
            CloseAction?.Invoke();
            CloseAction = null;
        }
    }
}