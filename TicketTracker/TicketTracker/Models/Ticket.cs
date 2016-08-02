using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TicketTracker.Models
{
    /// <summary>
    /// A ticket object to be stored or viewed in the database
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Ticket object ID
        /// </summary>
        public int TicketID { get; set; }
       
        /// <summary>
        /// Title of the ticket object concern
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Full descpription of the ticket object concern
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Ticket status active/resolved
        /// </summary>
        [Required]
        [EnumDataType(typeof(TicketTypes))]
        public TicketTypes Status { get; set; }

        /// <summary>
        /// Ticket creators email for inquiries
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string CreatorEmail { get; set; }

        /// <summary>
        /// Ticket creators email for inquiries
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string ResolverEmail { get; set; }
    }

    /// <summary>
    /// Ticket status
    /// </summary>
    public enum TicketTypes { active, resolved }
}