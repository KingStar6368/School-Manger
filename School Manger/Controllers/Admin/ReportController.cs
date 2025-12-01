using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Net.Mail;
using System.Net;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IConfiguration configuration, ILogger<ReportController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(10 * 1024 * 1024)] // 10MB
        public async Task<IActionResult> SubmitReport()
        {
            try
            {
                // دریافت داده‌ها از FormData
                var form = await Request.ReadFormAsync();

                // ساخت DTO از داده‌های فرم
                var reportDto = new BugReportDto
                {
                    Title = form["Title"].ToString(),
                    Priority = form["Priority"].ToString(),
                    IssueType = form["IssueType"].ToString(),
                    Page = form["Page"].ToString(),
                    Description = form["Description"].ToString(),
                    ReproductionSteps = form["ReproductionSteps"].ToString(),
                    ContactEmail = form["ContactEmail"].ToString()
                };

                // دریافت فایل اگر وجود دارد
                var screenshotFile = form.Files["Screenshot"];
                if (screenshotFile != null && screenshotFile.Length > 0)
                {
                    reportDto.Screenshot = screenshotFile;
                }

                // اعتبارسنجی
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new
                    {
                        success = false,
                        message = "لطفاً اطلاعات را به درستی وارد کنید.",
                        errors = errors
                    });
                }

                // ایجاد کد پیگیری
                var trackingCode = $"REP-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                // ارسال ایمیل
                var emailSent = await SendReportToSupport(reportDto, trackingCode);

                if (!string.IsNullOrEmpty(reportDto.ContactEmail))
                {
                    await SendConfirmationToUser(reportDto, trackingCode);
                }

                return Json(new
                {
                    success = true,
                    message = "گزارش شما با موفقیت ثبت شد.",
                    trackingCode = trackingCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting bug report");
                return StatusCode(500, new
                {
                    success = false,
                    message = "خطایی در ثبت گزارش رخ داده است."
                });
            }
        }

        [HttpPost]
        public IActionResult SaveDraft([FromBody] BugReportDto reportDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "داده‌های نامعتبر"
                    });
                }

                var draftJson = JsonSerializer.Serialize(reportDto);
                HttpContext.Session.SetString("BugReportDraft", draftJson);

                return Json(new
                {
                    success = true,
                    message = "پیش‌نویس ذخیره شد."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving draft");
                return StatusCode(500, new
                {
                    success = false,
                    message = "خطا در ذخیره"
                });
            }
        }

        [HttpGet]
        public IActionResult GetDraft()
        {
            try
            {
                var draftJson = HttpContext.Session.GetString("BugReportDraft");

                if (!string.IsNullOrEmpty(draftJson))
                {
                    var draft = JsonSerializer.Deserialize<BugReportDto>(draftJson);
                    return Json(new
                    {
                        success = true,
                        draft = draft
                    });
                }

                return Json(new
                {
                    success = false,
                    message = "پیش‌نویس یافت نشد"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting draft");
                return StatusCode(500, new
                {
                    success = false,
                    message = "خطا در دریافت"
                });
            }
        }

        [HttpPost]
        public IActionResult ClearDraft()
        {
            try
            {
                HttpContext.Session.Remove("BugReportDraft");
                return Json(new
                {
                    success = true,
                    message = "پیش‌نویس پاک شد"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing draft");
                return StatusCode(500, new
                {
                    success = false,
                    message = "خطا در پاک کردن"
                });
            }
        }
        private async Task<bool> SendReportToSupport(BugReportDto reportDto, string trackingCode)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var supportEmail = _configuration["SupportEmail"] ?? "support@schoolmanager.ir";

                var smtpServer = smtpSettings["Server"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(smtpSettings["Port"] ?? "587");
                var smtpUsername = smtpSettings["Username"];
                var smtpPassword = smtpSettings["Password"];
                var enableSsl = bool.Parse(smtpSettings["EnableSsl"] ?? "true");

                using var client = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = enableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername, "سیستم گزارش مشکل"),
                    Subject = $"گزارش جدید باگ - {trackingCode} - {reportDto.Title}",
                    Body = GenerateSupportEmailBody(reportDto, trackingCode),
                    IsBodyHtml = true,
                    Priority = GetMailPriority(reportDto.Priority)
                };

                mailMessage.To.Add(supportEmail);

                // ارسال CC به ایمیل‌های اضافی اگر در تنظیمات وجود دارند
                var ccEmails = _configuration["AdditionalSupportEmails"];
                if (!string.IsNullOrEmpty(ccEmails))
                {
                    foreach (var email in ccEmails.Split(','))
                    {
                        if (!string.IsNullOrEmpty(email.Trim()))
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                }

                await client.SendMailAsync(mailMessage);

                _logger.LogInformation("Support email sent for tracking code: {TrackingCode}", trackingCode);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to support");
                return false;
            }
        }

        private async Task<bool> SendConfirmationToUser(BugReportDto reportDto, string trackingCode)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");

                var smtpServer = smtpSettings["Server"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(smtpSettings["Port"] ?? "587");
                var smtpUsername = smtpSettings["Username"];
                var smtpPassword = smtpSettings["Password"];
                var enableSsl = bool.Parse(smtpSettings["EnableSsl"] ?? "true");

                using var client = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = enableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername, "پشتیبانی سیستم مدرسه"),
                    Subject = $"تأیید دریافت گزارش مشکل - کد پیگیری: {trackingCode}",
                    Body = GenerateUserConfirmationEmailBody(reportDto, trackingCode),
                    IsBodyHtml = true
                };

                mailMessage.To.Add(reportDto.ContactEmail);

                await client.SendMailAsync(mailMessage);

                _logger.LogInformation("Confirmation email sent to user: {Email}", reportDto.ContactEmail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending confirmation email to user");
                return false;
            }
        }

        private string GenerateSupportEmailBody(BugReportDto reportDto, string trackingCode)
        {
            var priorityText = reportDto.Priority switch
            {
                "low" => "کم",
                "medium" => "متوسط",
                "high" => "بالا",
                "critical" => "بحرانی",
                _ => "متوسط"
            };

            var issueTypeText = reportDto.IssueType switch
            {
                "bug" => "باگ نرم‌افزاری",
                "ui" => "مشکل رابط کاربری",
                "performance" => "مشکل عملکردی",
                "security" => "مشکل امنیتی",
                "database" => "مشکل پایگاه داده",
                "payment" => "مشکل پرداخت",
                "other" => "سایر",
                _ => "سایر"
            };

            return $@"
<!DOCTYPE html>
<html dir='rtl' lang='fa'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{ font-family: 'Tahoma', 'Arial', sans-serif; line-height: 1.6; color: #333; background-color: #f4f4f4; padding: 20px; }}
        .container {{ max-width: 800px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.1); }}
        .header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 20px; border-radius: 8px; text-align: center; margin-bottom: 30px; }}
        .info-box {{ background-color: #f8f9fa; border-right: 4px solid #3498db; padding: 15px; margin-bottom: 20px; border-radius: 5px; }}
        .priority-high {{ border-color: #e74c3c; }}
        .priority-medium {{ border-color: #f39c12; }}
        .priority-low {{ border-color: #2ecc71; }}
        .priority-critical {{ border-color: #9b59b6; }}
        .field-label {{ font-weight: bold; color: #2c3e50; margin-bottom: 5px; }}
        .field-value {{ margin-bottom: 15px; padding: 10px; background-color: white; border: 1px solid #ddd; border-radius: 5px; }}
        .footer {{ margin-top: 40px; padding-top: 20px; border-top: 2px solid #eee; color: #7f8c8d; font-size: 0.9em; text-align: center; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>📋 گزارش جدید باگ در سیستم</h1>
            <p>کد پیگیری: <strong>{trackingCode}</strong></p>
        </div>
        
        <div class='info-box priority-{reportDto.Priority}'>
            <h3>📊 اطلاعات کلی گزارش</h3>
            <p><strong>اولویت:</strong> {priorityText}</p>
            <p><strong>نوع مشکل:</strong> {issueTypeText}</p>
            <p><strong>زمان ثبت:</strong> {DateTime.Now:yyyy/MM/dd HH:mm}</p>
        </div>
        
        <div class='field-label'>عنوان مشکل:</div>
        <div class='field-value'>{reportDto.Title}</div>
        
        <div class='field-label'>صفحه یا بخش مربوطه:</div>
        <div class='field-value'>{reportDto.Page ?? "مشخص نشده"}</div>
        
        <div class='field-label'>توضیحات کامل:</div>
        <div class='field-value' style='white-space: pre-wrap;'>{reportDto.Description}</div>
        
        {(!string.IsNullOrEmpty(reportDto.ReproductionSteps) ? $@"
        <div class='field-label'>مراحل بازسازی مشکل:</div>
        <div class='field-value' style='white-space: pre-wrap;'>{reportDto.ReproductionSteps}</div>
        " : "")}
        
        <div class='field-label'>اطلاعات تماس گزارش‌دهنده:</div>
        <div class='field-value'>
            <p><strong>ایمیل:</strong> {reportDto.ContactEmail ?? "مشخص نشده"}</p>
            <p><strong>آدرس IP:</strong> {HttpContext.Connection.RemoteIpAddress}</p>
            <p><strong>مرورگر:</strong> {Request.Headers["User-Agent"]}</p>
        </div>
        
        <div class='footer'>
            <p>📞 پشتیبانی: 021-12345678 | 📧 ایمیل: support@schoolmanager.ir</p>
            <p>⏰ ساعات کاری: شنبه تا چهارشنبه ۸ صبح تا ۵ عصر</p>
            <p>این ایمیل به صورت خودکار از سیستم گزارش مشکل ارسال شده است.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GenerateUserConfirmationEmailBody(BugReportDto reportDto, string trackingCode)
        {
            return $@"
<!DOCTYPE html>
<html dir='rtl' lang='fa'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{ font-family: 'Tahoma', 'Arial', sans-serif; line-height: 1.6; color: #333; background-color: #f4f4f4; padding: 20px; }}
        .container {{ max-width: 600px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.1); }}
        .header {{ background: linear-gradient(135deg, #2ecc71 0%, #27ae60 100%); color: white; padding: 20px; border-radius: 8px; text-align: center; margin-bottom: 30px; }}
        .success-icon {{ font-size: 48px; margin-bottom: 20px; }}
        .tracking-code {{ background-color: #f8f9fa; padding: 15px; border-radius: 5px; text-align: center; font-family: monospace; font-size: 1.2em; margin: 20px 0; border: 2px dashed #3498db; }}
        .info-box {{ background-color: #e8f4fc; padding: 15px; border-radius: 5px; margin-bottom: 20px; border-right: 4px solid #3498db; }}
        .footer {{ margin-top: 40px; padding-top: 20px; border-top: 2px solid #eee; color: #7f8c8d; font-size: 0.9em; text-align: center; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='success-icon'>✅</div>
            <h2>گزارش شما با موفقیت ثبت شد</h2>
            <p>از همکاری شما برای بهبود سیستم سپاسگزاریم</p>
        </div>
        
        <div class='info-box'>
            <h3>📋 اطلاعات گزارش</h3>
            <p><strong>عنوان:</strong> {reportDto.Title}</p>
            <p><strong>زمان ثبت:</strong> {DateTime.Now:yyyy/MM/dd ساعت HH:mm}</p>
        </div>
        
        <div class='tracking-code'>
            <strong>کد پیگیری:</strong><br>
            <span style='font-size: 1.5em; color: #2c3e50;'>{trackingCode}</span>
        </div>
        
        <div class='info-box'>
            <h3>📞 مراحل بعدی</h3>
            <p>تیم پشتیبانی در اسرع وقت گزارش شما را بررسی خواهد کرد.</p>
            <p>با استفاده از کد پیگیری بالا می‌توانید از وضعیت گزارش خود مطلع شوید.</p>
            <p>مدت زمان بررسی بسته به اولویت گزارش ممکن است متفاوت باشد:</p>
            <ul>
                <li>مشکلات <strong>بحرانی</strong>: حداکثر ۲۴ ساعت</li>
                <li>مشکلات <strong>بالا</strong>: ۲-۳ روز کاری</li>
                <li>مشکلات <strong>متوسط</strong>: ۳-۵ روز کاری</li>
                <li>مشکلات <strong>کم</strong>: ۵-۷ روز کاری</li>
            </ul>
        </div>
        
        <div class='info-box'>
            <h3>ℹ️ راه‌های پیگیری</h3>
            <p>می‌توانید از طریق راه‌های زیر از وضعیت گزارش خود مطلع شوید:</p>
            <p>📧 ایمیل: support@schoolmanager.ir</p>
            <p>📞 تلفن: ۰۲۱-۱۲۳۴۵۶۷۸</p>
            <p>⏰ ساعات کاری: شنبه تا چهارشنبه ۸ صبح تا ۵ عصر</p>
        </div>
        
        <div class='footer'>
            <p>با تشکر از همراهی شما</p>
            <p><strong>تیم پشتیبانی سیستم مدیریت مدرسه</strong></p>
            <p>این ایمیل به صورت خودکار ارسال شده است. لطفاً به آن پاسخ ندهید.</p>
        </div>
    </div>
</body>
</html>";
        }

        private MailPriority GetMailPriority(string priority)
        {
            return priority switch
            {
                "critical" => MailPriority.High,
                "high" => MailPriority.High,
                "medium" => MailPriority.Normal,
                "low" => MailPriority.Low,
                _ => MailPriority.Normal
            };
        }
    }

    // DTO برای دریافت داده‌های گزارش
    public class BugReportDto
    {
        [Required(ErrorMessage = "عنوان مشکل الزامی است")]
        [StringLength(200, ErrorMessage = "عنوان مشکل نمی‌تواند بیشتر از ۲۰۰ کاراکتر باشد")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "اولویت مشکل الزامی است")]
        [RegularExpression("^(low|medium|high|critical)$", ErrorMessage = "اولویت نامعتبر است")]
        public string Priority { get; set; } = "medium";

        [Required(ErrorMessage = "نوع مشکل الزامی است")]
        [RegularExpression("^(bug|ui|performance|security|database|payment|other)$", ErrorMessage = "نوع مشکل نامعتبر است")]
        public string IssueType { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "نام صفحه نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
        public string? Page { get; set; }

        [Required(ErrorMessage = "توضیحات مشکل الزامی است")]
        [MinLength(20, ErrorMessage = "توضیحات مشکل باید حداقل ۲۰ کاراکتر باشد")]
        [MaxLength(5000, ErrorMessage = "توضیحات مشکل نمی‌تواند بیشتر از ۵۰۰۰ کاراکتر باشد")]
        public string Description { get; set; } = string.Empty;

        [MaxLength(2000, ErrorMessage = "مراحل بازسازی نمی‌تواند بیشتر از ۲۰۰۰ کاراکتر باشد")]
        public string? ReproductionSteps { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        [StringLength(100, ErrorMessage = "ایمیل نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
        public string? ContactEmail { get; set; }

        public IFormFile? Screenshot { get; set; }
    }
}