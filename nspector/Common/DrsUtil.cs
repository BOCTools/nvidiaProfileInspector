﻿using nspector.Common.Meta;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace nspector.Common
{
    public static class DrsUtil
    {
        public static string StringValueRaw = "Text";
        
        public static string GetDwordString(uint dword)
        {
            return string.Format("0x{0:X8}", dword);
        }

        public static uint ParseDwordByInputSafe(string input)
        {
            uint result = 0;
            if (input.ToLower().StartsWith("0x"))
            {
                try
                {
                    int blankPos = input.IndexOf(' ');
                    int parseLen = blankPos > 2 ? blankPos - 2 : input.Length - 2;
                    result = uint.Parse(input.Substring(2, parseLen), NumberStyles.AllowHexSpecifier);
                }
                catch { }
            }
            else
                try { result = uint.Parse(input); }
                catch { }

            return result;
        }

        internal static uint ParseDwordSettingValue(SettingMeta meta, string text)
        {
            var valueByName = meta.DwordValues.FirstOrDefault(x => x.ValueName != null && x.ValueName.Equals(text));
            if (valueByName != null)
                return valueByName.Value;

            return ParseDwordByInputSafe(text);
        }

        internal static string GetDwordSettingValueName(SettingMeta meta, uint dwordValue)
        {
            var settingValue = meta.DwordValues
                       .FirstOrDefault(x => x.Value.Equals(dwordValue));

            return settingValue == null ? GetDwordString(dwordValue): settingValue.ValueName;
        }

        internal static string ParseStringSettingValue(SettingMeta meta, string text)
        {
            var valueByName = meta.StringValues.FirstOrDefault(x => x.ValueName != null && x.ValueName.Equals(text));
            if (valueByName != null)
                return valueByName.Value;

            return text;
        }

        internal static string GetStringSettingValueName(SettingMeta meta, string stringValue)
        {
            var settingValue = meta.StringValues
                       .FirstOrDefault(x => x.Value.Equals(stringValue));

            return settingValue == null ? stringValue : settingValue.ValueName;
        }

    }
}
