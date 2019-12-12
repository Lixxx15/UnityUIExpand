using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour
{
    public SwitchUI _SwitchUI;
    public DateDropdown _DateDropdown;
    public Text _SwitchValue;
    public Text _DateDropdownValue;
    // Start is called before the first frame update
    void Start()
    {
        _SwitchUI.OnValueChange.AddListener((arg) => { _SwitchValue.text = arg.ToString(); });
        _DateDropdown.OnDropdownChange.AddListener((year, month, day) => { _DateDropdownValue.text = year + "/" + month + "/" + day; });
    }
}
