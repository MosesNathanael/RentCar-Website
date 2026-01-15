using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RentCar_MVC.Models;
using System.Net.Http.Headers;
using RentCar_MVC.Services;

namespace RentCar_MVC.Controllers
{
    public class TransactionHistoryController : Controller
    {
        private readonly ITransactionHistoryService _historyService;
    
       
        public TransactionHistoryController(ITransactionHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<ActionResult> ViewHistory()
        {
            var user = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login", "Account");
            }

            var history = await _historyService.GetHistoryAsync();

            if(history == null || !history.Any())
            {
                return View("~/Views/Transaction_history/ViewHistory.cshtml");
            }

            return View("~/Views/Transaction_history/ViewHistory.cshtml", history);
        }
    }
}