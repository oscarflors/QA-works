using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SwagLabs.UI.Tests
{
    public class LoginPage
    {
        private readonly IPage _page;
        private readonly ILocator _usernameInput;
        private readonly ILocator _passwordInput;
        private readonly ILocator _loginButton;
        private readonly ILocator _errorMessage;

        // El constructor "enseña" a esta clase dónde están los botones en la web
        public LoginPage(IPage page)
        {
            _page = page;
            _usernameInput = _page.Locator("[data-test='username']");
            _passwordInput = _page.Locator("[data-test='password']");
            _loginButton = _page.Locator("[data-test='login-button']");
            _errorMessage = _page.Locator("[data-test='error']");
        }

        public async Task GotoAsync()
        {
            await _page.GotoAsync("https://www.saucedemo.com/");
        }

        // Un método reutilizable para rellenar datos y hacer clic
        public async Task LoginAsync(string username, string password)
        {
            await _usernameInput.FillAsync(username);
            await _passwordInput.FillAsync(password);
            await _loginButton.ClickAsync();
        }

        public ILocator ErrorMessage => _errorMessage;
    }
}