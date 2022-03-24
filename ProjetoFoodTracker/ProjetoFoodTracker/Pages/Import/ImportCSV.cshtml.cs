using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ProjetoFoodTracker.Data.Import
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult Import(IFormFile file)
        {

            return RedirectToAction("ImportCSV");



            //using (SqlConnection conn = new SqlConnection("DefaultConnection"))
            //{
            //    conn.Open();
            //    using (StreamReader streamReader = new StreamReader(@"\wwwroot\File\Alimentos.csv"))
            //    {
            //        while (!streamReader.EndOfStream)
            //        {
            //            var line = streamReader.ReadLine();

            //            var values = line.Split(',');

            //            var sql = "INSERT INTO teste3.dbo.Food VALUES ('" + values[0] + ")";
            //            var sql1 = "INSERT INTO teste3.dbo.Category VALUES ('" + values[1] + ")";
            //            var sql2 = "INSERT INTO teste3.dbo.Actions VALUES ('" + values[2] + ")";

            //            var cmd = new SqlCommand();
            //            cmd.CommandText = sql;
            //            cmd.CommandType = System.Data.CommandType.Text;
            //            cmd.Connection = conn;
            //            cmd.ExecuteNonQuery();

            //        }
            //    }
            //    conn.Close();
            }
        }
    
}
