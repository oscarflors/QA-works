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

        [Test]
        public async Task Login_WithLockedOutUser_ShouldShowError()
        {
            var loginPage = new LoginPage(Page);
            
            await loginPage.GotoAsync();
            await loginPage.LoginAsync("locked_out_user", "secret_sauce");

            // Validamos usando el elemento guardado en nuestra página
            await Expect(loginPage.ErrorMessage).ToHaveTextAsync("Epic sadface: Sorry, this user has been locked out.");
        }
    }
}