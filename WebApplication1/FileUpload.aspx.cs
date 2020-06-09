using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace WebApplication1
{
    public partial class FileUpload : System.Web.UI.Page
    {
        // Inspirace:
        // https://docs.microsoft.com/cs-cz/dotnet/api/system.web.ui.webcontrols.fileupload?view=netframework-4.8

        private readonly string _uploadFilesPath = ConfigurationManager.AppSettings["UploadFilesPath"];

        /// <summary>
        /// Constructor.
        /// </summary>
        public FileUpload()
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(_uploadFilesPath));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbUploadStatus.ForeColor = Color.Black;

            lbUploadedFilesPath.Text = _uploadFilesPath;

            ShowUploadedFiles();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string savePath = Path.Combine(_uploadFilesPath, FileUpload1.FileName);

                FileUpload1.SaveAs(savePath);

                lbUploadStatus.ForeColor = Color.Blue;
                lbUploadStatus.Text = $"Your file was saved as: {savePath}";

                ShowUploadedFiles();
            }
            else
            {
                lbUploadStatus.ForeColor = Color.Red;
                lbUploadStatus.Text = "You did not specify a file to upload.";
            }
        }

        private void ShowUploadedFiles()
        {
            lvwUploadedFiles.DataSource = Directory.GetFiles(_uploadFilesPath);
            lvwUploadedFiles.DataBind();
        }
    }
}