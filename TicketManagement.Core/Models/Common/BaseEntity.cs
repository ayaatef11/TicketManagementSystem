using System.ComponentModel.DataAnnotations; 

namespace TicketManagement.DAL.Models.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
