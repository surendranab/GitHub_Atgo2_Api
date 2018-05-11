using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Atgo2.Api.Entity
{
    public class ApplicationUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        public string StateId { get; set; }
        public int Zip { get; set; }
        [Required]
        [StringLength(100)]
        public string UniqueUserId { get; set; }
        public string ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int DataCount { get; set; }
        public bool? IsEulaAccepted { get; set; }
        public int OriginGroupId { get; set; }
        public int OriginLocationId { get; set; }
        public string Salt { get; set; }
        public int PageSize { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneName { get; set; }
        public bool IsAutoLockEnable { get; set; }
        public bool IsAutomationUser { get; set; }
    }
}
