using Eventmi.Core.Constants;
using Eventmi.Core.Contracts;
using Eventmi.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eventmi.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        private readonly ILogger logger;

        public EventController(IEventService _eventService, ILogger<EventController> _logger)
        {
            eventService = _eventService;
            logger = _logger;
        }
        public async Task<IActionResult> Index()
        {
            var model = await eventService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new EventModel();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Add(EventModel model)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    await eventService.CreateAsync(model);

                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);                   
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Възникна грешка при създаване на събитие.");
                    ModelState.AddModelError(string.Empty, UserMessageConstants.UnknownError);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await eventService.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await eventService.EditAsync(model);

                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error editing event");
                    ModelState.AddModelError(string.Empty, UserMessageConstants.UnknownError);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await eventService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete failed");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await eventService.GetByIdAsync(id);

            return View(model);
        }
    }
}
