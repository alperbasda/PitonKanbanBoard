using Entities.Enums;

namespace Entities.Dtos.Get.UserCalender
{
    public class GetUserCalenderModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public RecordType RecordType { get; set; }

        public int RecordTypeInt => (int) RecordType;

    }
}