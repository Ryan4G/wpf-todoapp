﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ToDoListApp.Converters
{
    public class DisplayMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values != null)
            {
                if (values.Length == 2)
                {
                    bool display = false;
                    bool.TryParse(values[0].ToString(), out display);

                    var title = values[1].ToString();

                    if(title != "我的一天" && !display)
                    {
                        return string.Empty;
                    }

                    string time = string.Format("{0:M}", DateTime.Now);

                    string[] weeks = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                    var week = weeks[System.Convert.ToInt32(DateTime.Now.DayOfWeek)];

                    time = $"{time},{week}";

                    return time;
                }
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
