using NUnit.Framework;
using System.Threading.Tasks;

namespace SwagLabs.UI.Tests
{
    public class DatabaseTests
    {
        [Test]
        [Ignore("Prueba de concepto End-to-End: Requiere base de datos SQL Server levantada.")]
        public async Task Registration_ShouldSaveUserInSqlServer()
        {
            // 1. Simulación: Aquí Playwright rellenaría un formulario de registro en la UI
            var testUsername = "oscar_qa_pro";
            
            // 2. Conectamos con el backend saltándonos la web
            var dbHelper = new DatabaseHelper();
            
            // 3. Extraemos la verdad absoluta de la base de datos
            var recordsFound = await dbHelper.GetUserCountAsync(testUsername);
            
            // 4. Validamos que el desarrollador no ha roto el backend (Asertamos contra SQL)
            Assert.That(recordsFound, Is.EqualTo(1), "Fallo crítico: La web dice OK, pero el usuario no está en la base de datos.");
        }
    }
}