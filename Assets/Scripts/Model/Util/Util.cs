using System;
using System.ComponentModel;

#nullable enable

namespace Model.Util {
    public static class Util {
        public enum StatusCode {
            Success,
            Fail
        }

        public static string GetDescription<T>(this T t)
        {
            if (t == null) throw new InvalidOperationException();
            
            var gm = t.GetType().GetMember(t.ToString());
            var attributes = gm[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
