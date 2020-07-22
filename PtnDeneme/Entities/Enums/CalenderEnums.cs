using System.ComponentModel;

namespace Entities.Enums
{
    public enum RecordType
    {
        [Description("Günlük")]
        Day,
        [Description("Haftalık")]
        Week,
        [Description("Aylık")]
        Month,
    }

}