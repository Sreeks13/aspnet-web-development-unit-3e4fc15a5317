using ContosoWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ContosoWeb.Tests;

public class SeatingApiTests
{
    [Fact]
    public void Post_ValidGuest_Returns201()
    {
        var controller = new SeatingApiController();

        var result = controller.Create(
            new WeddingGuest
            {
                Name = "Test Guest",
                TableNumber = 3
            });

        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public void Post_EmptyName_Returns400()
    {
        var controller = new SeatingApiController();

        var result = controller.Create(
            new WeddingGuest
            {
                Name = "",
                TableNumber = 3
            });

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Put_ExistingGuest_Returns204()
    {
        var controller = new SeatingApiController();

        var result = controller.Update(
            1,
            new WeddingGuest
            {
                Name = "Updated Alice",
                TableNumber = 4
            });

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Put_MissingGuest_Returns404()
    {
        var controller = new SeatingApiController();

        var result = controller.Update(
            999,
            new WeddingGuest
            {
                Name = "Nobody",
                TableNumber = 5
            });

        Assert.IsType<NotFoundResult>(result);
    }
}
