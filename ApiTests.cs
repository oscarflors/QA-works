using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SwagLabs.UI.Tests
{
    public class ApiTests : PlaywrightTest
    {
        [Test]
        public async Task GetAllBookings_ShouldReturn200OK()
        {
            // 1. Preparamos el cliente de red apuntando al servidor de Restful-Booker
            var apiContext = await Playwright.APIRequest.NewContextAsync(new()
            {
                BaseURL = "https://restful-booker.herokuapp.com"
            });

            // 2. Lanzamos la petición GET invisible al endpoint de reservas
            var response = await apiContext.GetAsync("/booking");

            // 3. Validamos que el servidor está vivo y devuelve un 200 OK
            Assert.That(response.Status, Is.EqualTo(200));
            
            // 4. Validamos que nos ha devuelto información y no está vacía
            var jsonBody = await response.JsonAsync();
            Assert.That(jsonBody, Is.Not.Null);
        }
    }
}