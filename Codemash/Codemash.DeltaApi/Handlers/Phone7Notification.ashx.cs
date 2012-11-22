using System.Drawing;
using System.IO;
using System.Web;
using Codemash.Server.Core.Extensions;

namespace Codemash.DeltaApi.Handlers
{
    /// <summary>
    /// Summary description for Phone7Notification
    /// </summary>
    public class Phone7Notification : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string basePath = context.Server.MapPath("Images/wp7");

            int count = context.Request.QueryString["count"].AsInt();
            if (count > 0)
            {
                string filename = GetFilename(count);
                string filePath = Path.Combine(basePath, filename);
                if (!File.Exists(filePath))
                {
                    CreateNotificationTile(basePath, count);
                }

                context.Response.ContentType = "image/png";
                context.Response.WriteFile(Path.Combine(basePath, GetFilename(count)));
                context.Response.Flush();
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        private string GetFilename(int count)
        {
            return count.ToString() + ".png";
        }

        private void CreateNotificationTile(string basePath, int count)
        {
            const string template_name = "phone7_template.png";
            using (var srcTemplate = new Bitmap(Path.Combine(basePath, template_name)))
            {
                using (var graphics = Graphics.FromImage(srcTemplate))
                {
                    var font = new Font("Arial", 35f, GraphicsUnit.Point);
                    var brush = new SolidBrush(Color.White);
                    graphics.DrawString(count.ToString(), font, brush, 105, 60);
                }

                srcTemplate.Save(Path.Combine(basePath, GetFilename(count)));
            }
        }
    }
}