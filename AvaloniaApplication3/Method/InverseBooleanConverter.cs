using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication3.Method
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue; // 取反
            }
            return value; // 默认返回输入值
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // 此处需要实现 ConvertBack 方法
            // 假设也要对 bool 值进行反向转换
            if (value is bool boolValue)
            {
                return !boolValue; // 取反
            }
            return value; // 默认返回输入值
        }
    }

}
