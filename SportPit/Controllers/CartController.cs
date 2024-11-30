using Microsoft.AspNetCore.Mvc;
using SportPit.Core;
using SportPit.Models.Dto;
using SportPit.Repositories.Interfaces;

namespace SportPit.Controllers;

public class CartController(
    IOrderRepository orderRepository, 
    ICartRepository cartRepository, 
    IProductRepository productRepository,
    IUserRepository userRepository) : Controller
{
    public IActionResult Index()
    {
        var cartDto = new CartDto
        {
            Price = orderRepository.Sum,
            Products = orderRepository.CountProductsByProductId
        };

        return View(cartDto);
    }

    public async Task<IActionResult> CreateOrder()
    {
        var products = await productRepository
            .GetProductsByListIdAsync(orderRepository.CountProductsByProductId.Keys.Select(x => x.Id).ToList());
        var user = await userRepository.GetUserByIdAsync(1);
        await cartRepository.AddAsync(products, user, DateOnly.FromDateTime(DateTime.Now), orderRepository.Sum);

        orderRepository.Clear();

        HomeController home;

        return RedirectToAction(
            nameof(home.Index), 
            ControllerName.GetName(nameof(HomeController))
            );
    }
}