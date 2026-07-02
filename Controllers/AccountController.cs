using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mini_store.ViewModels;
using mini_store.Models;
using System.Threading.Tasks;

namespace mini_store.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

         private readonly UserManager<ApplicationUser> _userManager; // أضفنا هذا السطر

        // 2. حقن الأدوات في المُشيّد (Constructor)
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) 
        {
            _signInManager = signInManager;
            _userManager = userManager; 
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // محاولة تسجيل الدخول
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, 
                    model.Password, 
                    model.RememberMe, 
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
                
                // إذا كانت البيانات خاطئة
                ModelState.AddModelError(string.Empty, "البريد الإلكتروني أو كلمة المرور غير صحيحة.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // تنظيف جلسة الدخول وحذف ملف تعريف الارتباط (Cookie)
            await _signInManager.SignOutAsync();
            
            // توجيه المستخدم إلى الصفحة الرئيسية بعد تسجيل الخروج
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
    // 1. التحقق من أن المدخلات تطابق الشروط (مثل تطابق كلمتي المرور وصحة البريد)
        if (ModelState.IsValid)
        {
            // 2. إنشاء كائن مستخدم جديد وتعبئة بياناته
            var user = new ApplicationUser 
            { 
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email, 
                Email = model.Email 
            };
            
            // 3. حفظ المستخدم في قاعدة البيانات وتشفير كلمة المرور تلقائياً
            var result = await _userManager.CreateAsync(user, model.Password);

            // 4. إذا تم الحفظ بنجاح
            if (result.Succeeded)
            {
                // تسجيل دخول المستخدم الجديد تلقائياً
                await _signInManager.SignInAsync(user, isPersistent: false);
                
                // توجيهه إلى الصفحة الرئيسية للمتجر (أو أي صفحة تريدها)
                return RedirectToAction("Index", "Product");
            }

            // 5. إذا فشل الحفظ (مثلاً: الإيميل مستخدم مسبقاً، أو كلمة المرور ضعيفة)
            // نقوم بإضافة الأخطاء لعرضها للمستخدم في الصفحة
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // إذا كانت هناك أخطاء في المدخلات، نعيد عرض الصفحة مع الأخطاء
        return View(model);
        }
    }
}