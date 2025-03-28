﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookiesKitchen.Data;
using CookiesKitchen.Models;
using CookiesKitchen.Models.ViewModels;

namespace CookiesKitchen.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Order
            .Include(o => o.Customer)
            .Include(o => o.Meal)
            .ToListAsync();

            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var viewModel = new OrderViewModel
            {
                Customer = order.Customer,
                Meals = new List<Meal> { order.Meal }
            };

            return View(viewModel);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            var viewModel = new OrderViewModel
            {
                Meals = _context.Meal.ToList() // Load available meals
            };

            return View(viewModel);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Meals = _context.Meal.ToList();
                return View(viewModel);
            }

            // 1. Check if customer exists by email
            var customer = await _context.Customer
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Email == viewModel.Customer.Email);

            // 2. If not found, create and save the new customer
            if (customer == null)
            {
                customer = viewModel.Customer; // comes from the form
                _context.Customer.Add(customer);
                await _context.SaveChangesAsync(); // saves and gives the customer an Id
            }

            // 3. Create the order
            var order = new Order
            {
                CustomerId = customer.Id,
                MealId = viewModel.SelectedMealId,
                OrderDate = DateTime.UtcNow
            };
            
            // 4. Add order through navigation property (optional)
            customer.Orders.Add(order);

            // 5. Save the order
            _context.Order.Add(order); // also fine to do this instead of adding to Orders list
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
