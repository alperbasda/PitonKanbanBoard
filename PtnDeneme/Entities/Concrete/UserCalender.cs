using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Core.Entities.Concrete;
using Entities.Enums;

namespace Entities.Concrete
{
    public class UserCalender : Entity
    {
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        public string Description { get; set; }

        public RecordType RecordType { get; set; }

    }
}