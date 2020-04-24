using CurrencyBank.API.Dtos;
using CurrencyBank.API.Interfaces;
using CurrencyBank.API.Utilities;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CurrencyBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PdfCreatorController : ControllerBase
    {
        private IConverter _converter;
        private readonly IAccountRepository _accountRepo;
        private readonly IUserRepository _userRepo;

        public PdfCreatorController(IConverter converter, IAccountRepository accountRepo, IUserRepository userRepository)
        {
            _converter = converter;
            _accountRepo = accountRepo;
            _userRepo = userRepository;
        }

        [HttpPost("getAll")]
        public async Task<IActionResult> CreatePDFForAllAccountsForUser(PdfParameters parameters)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var accountsForUser = await _accountRepo.GetAccountsHistoryForUser(currentUserId);
            var user = await _userRepo.GetUser(currentUserId);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
            };

            if (parameters.SaveToDefinedPath)
            {
                globalSettings.Out = string.Format(@"{0}\{1}.pdf", user.PathToPdfFolder, parameters.Filename != string.Empty ? parameters.Filename 
                        : $"Report-{DateTime.Now.Millisecond}");
                globalSettings.Out = globalSettings.Out.Replace(" ", string.Empty);
            }

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = PdfUtility.GetHTMLStringAllAccountsForUser(accountsForUser),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Center = $"Generate date: {DateTime.Now}" },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            if (parameters.SaveToDefinedPath)
            {
                _converter.Convert(pdf);
                return Ok($"File saved to {globalSettings.Out}");
            } else
            {
                var file = _converter.Convert(pdf);
                return File(file, "application/pdf");
            }
        }
    }
}