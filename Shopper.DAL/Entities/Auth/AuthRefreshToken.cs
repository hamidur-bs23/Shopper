using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shopper.DAL.Entities.Auth
{
    public class AuthRefreshToken
    {
        public AuthRefreshToken()
        {
            IsUsed = false;
            IsRevoked = false;
        }

        public int Id { get; set; }
        public string UserId { get; set; } // FK
        public string RefreshToken { get; set; }
        public string JwtTokenId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }


        // Relationship
        [ForeignKey(nameof(UserId))]
        public AuthUser User { get; set; }
    }
}
