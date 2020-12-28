using System.ComponentModel;

#nullable enable

namespace Database
{
    public enum TownType {
        [Description("港町")]
        Port,
        [Description("内陸")]
        Inland,
    }
}