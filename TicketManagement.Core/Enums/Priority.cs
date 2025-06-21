using System.Runtime.Serialization;

namespace TicketManagement.DAL.Enums
{
    public enum Priority
    {
        [EnumMember(Value ="low")]
        Low ,
        [EnumMember(Value ="medium")]
        Medium ,
        [EnumMember(Value ="high")]
        High
    }
}
