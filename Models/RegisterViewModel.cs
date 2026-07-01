using System.ComponentModel.DataAnnotations;

namespace mini_store.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="الإسم الأول مطلوب")]
        [StringLength(50,ErrorMessage ="عفواً بيانات الحقل طويلة جداً  الطول لا يتجاوز 50 حرف")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="الإسم الثاني مطلوب")]
        [StringLength(50,ErrorMessage ="عفواً بيانات الحقل طويلة جداً  الطول لا يتجاوز 50 حرف")]
        public string LastName { get; set; }


        [Required(ErrorMessage =" البريد الإليكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "يرجى أدخال بريد اليكتروني صحيح ")]
        public string Email { get; set; }


        [Required(ErrorMessage ="كلمة المرور مطلوبة ")]
        [StringLength(100,ErrorMessage ="يجب ان تكون على الأقل بطول 6 رموز", MinimumLength =6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="تاكيد كلمة المرور مطلوبة ")]
        [StringLength(100,ErrorMessage ="يجب ان تكون على الأقل بطول 6 رموز", MinimumLength =6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="كلمة السر غير متطابقة")]
        public string ConfirmPaasword { get; set; }
    }
}