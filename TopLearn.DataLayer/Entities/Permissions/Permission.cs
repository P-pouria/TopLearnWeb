using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopLearn.DataLayer.Entities.Permissions
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PermissionTitle { get; set; }

        public int? ParentID { get; set; }

        #region Relations

        // Reference to parent permission (self-referencing relationship)
        [ForeignKey("ParentID")]
        public Permission ParentPermission { get; set; }

        // List of child permissions
        public List<Permission> ChildPermissions { get; set; }

        public List<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}