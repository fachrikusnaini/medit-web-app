﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiniProject319.ViewModels
{
    public class VMm_user
    {
        public long Id { get; set; }
        public long? BiodataId { get; set; }
        public long? RoleId { get; set; }
        public string? Name { get; set; }
        public string? Fullname { get; set; }
        public string? ImagePath { get; set; }
        public string? MobilePhone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? LoginAttempt { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LastLogin { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public bool? IsExpired { get; set; }
        public long? UserId { get; set; }
    }
}
