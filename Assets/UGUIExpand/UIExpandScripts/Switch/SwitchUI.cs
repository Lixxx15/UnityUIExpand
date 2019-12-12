using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
    [DisallowMultipleComponent]
    [AddComponentMenu("UIExpand/SwitchUI")]
    public class SwitchUI : UIBehaviour, IPointerClickHandler, ICanvasElement
    {
        public Image _BG;
        public RectTransform _Cell;
        public Color OpenColor;
        public Color CloseColor;
        public bool _SwitchStatue
        {
            get { return m_SwitchStatue; }
            set { Set(value); }
        }
        public Vector2 Interval_1;//Background 与 SwitchUI 的间隔
        public Vector2 Interval_2;//Cell 与 Background 的间隔
        [Serializable] public class SwitchUIEvent : UnityEvent<bool> { }
        public SwitchUIEvent OnValueChange = new SwitchUIEvent();

        private Vector2 SwitchSize;
        private Vector2 CellSize;
        private Vector2 BGSize;

        private Vector3 OpenPoint;
        private Vector3 ClosePoint;
        
        [SerializeField]
        private bool m_SwitchStatue;

        protected override void Start()
        {
            Init();
        }

        private void Init()
        {
            SwitchSize = (transform as RectTransform).sizeDelta;
            _BG.color = _SwitchStatue ? OpenColor : CloseColor;
            SetSize();
            SetCellPoint();
        }
        private void SetCellPoint()
        {
            _Cell.localPosition = _SwitchStatue ? OpenPoint : ClosePoint;
        }
        private void SetSize()
        {
            float BGsizeY = SwitchSize.y - 2 * Interval_1.y;
            float BGsizeX = SwitchSize.x - 2 * Interval_1.x;
            BGSize = new Vector2(BGsizeX, BGsizeY);
            _BG.rectTransform.sizeDelta = BGSize;

            float CellsizeY = BGsizeY - 2 * Interval_2.y;
            float CellsizeX = (BGsizeX - 4 * Interval_2.x) / 2;
            CellSize = new Vector2(CellsizeX, CellsizeY);
            _Cell.sizeDelta = CellSize;

            OpenPoint = new Vector3(Interval_2.x + CellsizeX / 2, 0, 0);
            ClosePoint = new Vector3(-(Interval_2.x + CellsizeX / 2), 0, 0);
        }
        private void Set(bool value)
        {
            m_SwitchStatue = value;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _SwitchStatue = !_SwitchStatue;
            _BG.color = _SwitchStatue ? OpenColor : CloseColor;
            if (OnValueChange != null)
            {
                OnValueChange.Invoke(_SwitchStatue);
            }
            SetCellPoint();
        }

        public void Rebuild(CanvasUpdate executing)
        {
#if UNITY_EDITOR
            if (executing == CanvasUpdate.Prelayout)
            {
                OnValueChange.Invoke(_SwitchStatue);
            }
            Debug.Log(m_SwitchStatue);
#endif
        }

        public void LayoutComplete()
        {
            
        }

        public void GraphicUpdateComplete()
        {
            
        }


        
    }
}