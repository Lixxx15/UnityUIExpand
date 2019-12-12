using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UGUIExpand
{
    public class CreateModule : SingletonTemplate<CreateModule>
    {
        private CreateModule()
        {

        }

        public GameObject CreateSwitchUI()
        {
            SwitchUIClass switchUIClass = new SwitchUIClass();
            return switchUIClass._SwitchUIGO;
        }
        
    }
    public class SwitchUIClass
    {
        public SwitchUIClass()
        {
            CreateRoot();
        }
        public GameObject _SwitchUIGO { get; set; }
        private SwitchUI _SwitchUI;
        private void CreateRoot()
        {
            _SwitchUIGO = new GameObject("SwitchUI");
            _SwitchUI = _SwitchUIGO.AddComponent<SwitchUI>();
            _SwitchUI.OpenColor = new Color(0.2f,0.8f,0.2f,0.6f);
            _SwitchUI.CloseColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);
            RectTransform rootTransform = _SwitchUIGO.AddComponent<RectTransform>();
            rootTransform.sizeDelta = new Vector2(100, 50);
            Image image = _SwitchUIGO.AddComponent<Image>();
            image.color = new Color(1, 1, 1, 1);
            _SwitchUI.Interval_1 = new Vector2(4, 4);
            _SwitchUI.Interval_2 = new Vector2(2, 2);
            CreateBG();
            CreateCell();
        }
        private void CreateBG()
        {
            GameObject BG = new GameObject("Backgruond");
            Image BGImage = BG.AddComponent<Image>();
            RectTransform BGRect = BG.GetComponent<RectTransform>();
            BGRect.sizeDelta = new Vector2(92, 42);
            BGImage.sprite = null;
            _SwitchUI._BG = BGImage;
            BGImage.color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
            BG.transform.SetParent(_SwitchUIGO.transform);
        }
        private void CreateCell()
        {
            GameObject Cell = new GameObject("Cell");
            Image CellImage = Cell.AddComponent<Image>();
            CellImage.sprite = null;
            RectTransform CellRect = Cell.GetComponent<RectTransform>();
            CellRect.sizeDelta = new Vector2(42, 38);
            _SwitchUI._Cell = CellRect;
            Cell.transform.SetParent(_SwitchUIGO.transform);
        }
    }

    

}
