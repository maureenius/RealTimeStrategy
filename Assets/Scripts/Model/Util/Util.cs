using System.ComponentModel;

namespace Model.Util {
    public static class Util {
        public enum StatusCode {
            Success,
            Fail
        }

        public static string GetDescription<T>(this T t) {
            var gm = t.GetType().GetMember(t.ToString());
            var attributes = gm[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
