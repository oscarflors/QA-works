using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SwagLabs.UI.Tests
{
    public class LoginTests : PageTest
    {
        [Test]
        public async Task Login_WithStandardUser_ShouldAccessProducts()
        {
            // Instanciamos nuestra página
            var loginPage = new LoginPage(Page);
            
            // Navegamos e interactuamos usando métodos limpios
            await loginPage.GotoAsync();
            await loginPage.LoginAsync("standard_user", "secret_sauce");
            
            // Validamos
            await Expect(Page.Locator(".title")).ToHaveTextAsync("Products");
        }

        [TestCase("locked_out_user", "secret_sauce", "Epic sadface: Sorry, this user has been locked out.")]
        [TestCase("standard_user", "contraseña_falsa", "Epic sadface: Username and password do not match any user in this service")]
        [TestCase("", "secret_sauce", "Epic sadface: Username is required")]
        public async Task Login_WithInvalidCredentials_ShouldShowDynamicError(string username, string password, string expectedError)
        {
            var loginPage = new LoginPage(Page);
            
            await loginPage.GotoAsync();
            await loginPage.LoginAsync(username, password);

            // Validamos que el cartel rojo muestre exactamente el error inyectado
            await Expect(loginPage.ErrorMessage).ToHaveTextAsync(expectedError);
        }

        [Test]
        public async Task Login_WithBrokenNetwork_ShouldLoadWithoutImages()
        {
            // Secuestramos la red: antes de cargar nada, le decimos a Playwright 
            // que intercepte y destruya cualquier petición que acabe en png, jpg o jpeg.
            await Page.RouteAsync("**/*.{png,jpg,jpeg}", async route => await route.AbortAsync());
            
            var loginPage = new LoginPage(Page);
            
            await loginPage.GotoAsync();
            await loginPage.LoginAsync("standard_user", "secret_sauce");

            // Validamos que el sistema es resiliente y nos deja entrar aunque falten recursos
            await Expect(Page.Locator(".title")).ToHaveTextAsync("Products");
        }
    }
}