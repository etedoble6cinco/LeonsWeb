using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using LeonsWeb.Data;
using LeonsWeb.Models;
using Microsoft.AspNetCore.Authorization;
using LeonsWeb.Services;
using LeonsWeb.Models.ViewModel;

namespace LeonsWeb.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ServiceController : Controller

    {

        private readonly IServiceService _serviceService;
       

        public ServiceController(  IServiceService service)
        {
           
            _serviceService = service;
        }
        // GET: Service
        public async Task<IActionResult> Index()
        {

              
              List<ServiceViewModel> services = new List<ServiceViewModel>();
             services = await _serviceService.GetAllServices();

             return View(services);
              
        }
        // GET: Service/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if((await _serviceService.GetService(id)) == null){
                return NotFound();
            }
            return View(await _serviceService.GetService(id));
        }
        // GET: Service/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceName,ServiceDescription,ServicePrice")] ServiceViewModel service)
        {
   
            
            if (ModelState.IsValid)
            {
                  if(await _serviceService.CreateService(service)){
                         return RedirectToAction(nameof(Index));
                  }
               return Problem("Not Created");
            }
            return View(service);
        }

        // GET: Service/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var service = await _serviceService.GetService(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceName,ServiceDescription,ServicePrice")] ServiceViewModel service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(await _serviceService.EditService(service))
                return RedirectToAction(nameof(Index));
                else{
                    return NotFound();
                }
            }
            return View(service);
        }

        // GET: Service/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            
            if ( await _serviceService.GetService(id) == null)
            {
                return NotFound();
            }

            return View(await _serviceService.GetService(id));
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if ((await _serviceService.DeleteService(id)))
            {
                return RedirectToAction(nameof(Index));
                
            }
        
         return Problem("Entity set 'ApplicationDbContext.Services'  is null.");   
        }

       
    }
}
