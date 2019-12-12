using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEngine.UI
{
    [DisallowMultipleComponent]
    [AddComponentMenu("UIExpand/DateDropdown")]
    public class DateDropdown : MonoBehaviour
    {
        public Dropdown _Year;//设置年份的下拉菜单
        public Dropdown _Month;//设置月份的下拉菜单
        public Dropdown _Day;//设置天数的下拉菜单
        public int YearMax = 20;//从现在开始，往前可设置多少年
        [Serializable] public class DropdownChange : UnityEvent<int,int,int> { }
        public DropdownChange OnDropdownChange;

        private int SelectYear;//选择的年
        private int SelectMonth;//选择的月
        private int SelectDay;//选择的天

        private void Start()
        {
            
            _Year.onValueChanged.AddListener(YearChange);
            _Month.onValueChanged.AddListener(MonthChange);
            _Day.onValueChanged.AddListener(DayChange);
            _Year.AddOptions(LoadYearOption());
            YearChange(0);
        }

        private void YearChange(int index)
        {
            SelectYear = int.Parse(_Year.options[index].text);
            SelectMonth = 1;
            SelectDay = 1;
            SetDayNum(_Month, 12, true);
            LoadDayOption("1");
            OnDropdownChange.Invoke(SelectYear, SelectMonth, SelectDay);

        }
        private void MonthChange(int index)
        {
            SelectMonth = int.Parse(_Month.options[index].text);
            LoadDayOption(_Month.options[index].text);
            OnDropdownChange.Invoke(SelectYear, SelectMonth, SelectDay);
        }
        private void DayChange(int index)
        {
            SelectDay = int.Parse(_Day.options[index].text);
            //Debug.Log(Tools.GetConstellation.Constellation(SelectMonth, SelectDay));
            OnDropdownChange.Invoke(SelectYear, SelectMonth, SelectDay);

        }
        private List<string> LoadYearOption()
        {
            List<string> vs = new List<string>();
            for (int i = 0; i < YearMax; i++)
            {
                string a = (DateTime.Now.Year - i).ToString();
                vs.Add(a);
            }
            return vs;
        }
        private void LoadDayOption(string month)
        {
            switch (month)
            {
                case "1":
                    SetDayNum(_Day, 31);
                    break;
                case "2":
                    if ((SelectYear % 4 == 0 && SelectYear % 100 != 0) || SelectYear % 400 == 0)
                    {
                        SetDayNum(_Day, 29);
                    }
                    else
                    {
                        SetDayNum(_Day, 28);
                    }

                    break;
                case "3":
                    SetDayNum(_Day, 31);
                    break;
                case "4":
                    SetDayNum(_Day, 30);
                    break;
                case "5":
                    SetDayNum(_Day, 31);
                    break;
                case "6":
                    SetDayNum(_Day, 30);
                    break;
                case "7":
                    SetDayNum(_Day, 31);
                    break;
                case "8":
                    SetDayNum(_Day, 31);
                    break;
                case "9":
                    SetDayNum(_Day, 30);
                    break;
                case "10":
                    SetDayNum(_Day, 31);
                    break;
                case "11":
                    SetDayNum(_Day, 30);
                    break;
                case "12":
                    SetDayNum(_Day, 31);
                    break;
                default:
                    break;
            }
        }
        private void SetDayNum(Dropdown dd, int num, bool isMonth = false)
        {
            dd.ClearOptions();
            if (SelectYear == DateTime.Now.Year)
            {
                num = MaxDate(num, isMonth);
            }
            List<string> vs = new List<string>();
            for (int i = 1; i < num + 1; i++)
            {
                vs.Add(i.ToString());
            }
            dd.AddOptions(vs);
        }
        private int MaxDate(int num, bool isMonth)
        {
            if (isMonth)
            {
                return DateTime.Now.Month;
            }
            else
            {
                if (SelectMonth == DateTime.Now.Month)
                {
                    return DateTime.Now.Day;
                }
            }
            return num;
        }
    }
}
