
using System.Runtime.Serialization;

namespace TicketManagement.DAL.Enums
{
    public enum Status
    {
        [EnumMember(Value ="Open")]
        open,
        [EnumMember(Value = "Closed")]
        closed
    }
}
