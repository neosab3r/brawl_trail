using System.Collections.Generic;
using UnityEngine;

[ExcelAsset (AssetPath = "Resources/SO")]
public class Locales : ScriptableObject
{
    public List<LocalizationExcelData> AllLocales;
}