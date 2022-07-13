using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FaithAG.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    //Logging into the system
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }



    //User Registration
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Username")]
        public string UserName
        {
            get;
            set;
        }


        [Required]
        [Display(Name = "First Name")]
        public string Firstname
        {
            get;
            set;
        }


        [Display(Name = "Middle Name")]
        public string Middlename
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Last Name")]
        public string Lastname
        {
            get;
            set;
        }

        [Required]
        [Phone]
        [Display(Name = "Phone#")]
        public string PhoneN
        {
            get;
            set;
        }


        [EmailAddress]
        [Display(Name = "Email")]
        public string Email
        {
            get;
            set;
        }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        [Display(Name = "Full Name")]
        public string Fullname
        {
            get
            {
                return ( Firstname  + " " + Middlename + " " + Lastname).Trim();
            }
        }
              

        //[Required]
        //[Display(Name = "UserRoles")]
        //public string UserRoles
        //{
        //    get;
        //    set;
        //}



    }

    //Reset Password
    public class ResetAllPasswords
    {
        [Required]
        [Display(Name = "UserId")]
        public string UserID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }


    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
