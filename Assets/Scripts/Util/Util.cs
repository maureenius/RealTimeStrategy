using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Util {
    public static class Util {
        public enum StatusCode {
            SUCCESS,
            FAIL
        }

        public static string GetDescription<T>(this T t) {
            var gm = t.GetType().GetMember(t.ToString());
            var attributes = gm[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
