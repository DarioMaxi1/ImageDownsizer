using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imageDownsizer
{
    public partial class Form1 : Form
    {
        private Image originalImage;
        private CancellationTokenSource cancellationTokenSource;

        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap ConsequentialDownscale(Image original, double scaleFactor, CancellationToken token)
        {
            int originalWidth = original.Width;
            int originalHeight = original.Height;
            int newWidth = (int)(originalWidth * scaleFactor);
            int newHeight = (int)(originalHeight * scaleFactor);

            Bitmap downscaledImage = new Bitmap(newWidth, newHeight);

            BitmapData originalData = ((Bitmap)original).LockBits(new Rectangle(0, 0, originalWidth, originalHeight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData downscaledData = downscaledImage.LockBits(new Rectangle(0, 0, newWidth, newHeight), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int originalStride = originalData.Stride;
            int downscaledStride = downscaledData.Stride;

            byte[] originalBytes = new byte[originalData.Height * originalStride];
            byte[] downscaledBytes = new byte[downscaledData.Height * downscaledStride];

            Marshal.Copy(originalData.Scan0, originalBytes, 0, originalBytes.Length);
            Marshal.Copy(downscaledData.Scan0, downscaledBytes, 0, downscaledBytes.Length);

            for (int y = 0; y < newHeight; y++)
            {
                if (token.IsCancellationRequested)
                {
                    MessageBox.Show("Operation canceled!");
                    return null;
                }

                for (int x = 0; x < newWidth; x++)
                {
                    int origX = (int)(x / scaleFactor);
                    int origY = (int)(y / scaleFactor);

                    int originalIndex = origY * originalStride + origX * 3;
                    int downscaledIndex = y * downscaledStride + x * 3;

                    downscaledBytes[downscaledIndex] = originalBytes[originalIndex];
                    downscaledBytes[downscaledIndex + 1] = originalBytes[originalIndex + 1];
                    downscaledBytes[downscaledIndex + 2] = originalBytes[originalIndex + 2];
                }

                // Update progress bar for each row processed
                int progress = (int)((y / (float)newHeight) * 100);
                Invoke(new Action(() => progressBar.Value = progress));
            }

            Marshal.Copy(downscaledBytes, 0, downscaledData.Scan0, downscaledBytes.Length);

            ((Bitmap)original).UnlockBits(originalData);
            downscaledImage.UnlockBits(downscaledData);

            return downscaledImage;
        }

        private Bitmap ParallelDownscale(Image original, double scaleFactor, CancellationToken token)
        {
            int originalWidth = original.Width;
            int originalHeight = original.Height;
            int newWidth = (int)(originalWidth * scaleFactor);
            int newHeight = (int)(originalHeight * scaleFactor);

            Bitmap downscaledImage = new Bitmap(newWidth, newHeight);

            BitmapData originalData = ((Bitmap)original).LockBits(new Rectangle(0, 0, originalWidth, originalHeight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData downscaledData = downscaledImage.LockBits(new Rectangle(0, 0, newWidth, newHeight), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int originalStride = originalData.Stride;
            int downscaledStride = downscaledData.Stride;

            byte[] originalBytes = new byte[originalData.Height * originalStride];
            byte[] downscaledBytes = new byte[downscaledData.Height * downscaledStride];

            Marshal.Copy(originalData.Scan0, originalBytes, 0, originalBytes.Length);
            Marshal.Copy(downscaledData.Scan0, downscaledBytes, 0, downscaledBytes.Length);

            Parallel.For(0, newHeight, new ParallelOptions { CancellationToken = token }, y =>
            {
                for (int x = 0; x < newWidth; x++)
                {
                    int origX = (int)(x / scaleFactor);
                    int origY = (int)(y / scaleFactor);

                    int originalIndex = origY * originalStride + origX * 3;
                    int downscaledIndex = y * downscaledStride + x * 3;

                    downscaledBytes[downscaledIndex] = originalBytes[originalIndex];
                    downscaledBytes[downscaledIndex + 1] = originalBytes[originalIndex + 1];
                    downscaledBytes[downscaledIndex + 2] = originalBytes[originalIndex + 2];
                }

                int progress = (int)((y / (float)newHeight) * 100);
                Invoke(new Action(() => progressBar.Value = progress));
            });

            Marshal.Copy(downscaledBytes, 0, downscaledData.Scan0, downscaledBytes.Length);

            ((Bitmap)original).UnlockBits(originalData);
            downscaledImage.UnlockBits(downscaledData);

            return downscaledImage;
        }

        private void btnSelectImage_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                originalImage = Image.FromFile(openFileDialog.FileName);
                picBoxOriginal.Image = originalImage;
                picBoxOriginal.SizeMode = PictureBoxSizeMode.StretchImage;

                MessageBox.Show($"Original Image Dimensions: {originalImage.Width}x{originalImage.Height}");
            }
        }

        private void btnDownscale_Click(object sender, EventArgs e)
        {
            try
            {
                double scaleFactor;
                if (double.TryParse(txtScaleFactor.Text, out scaleFactor))
                {
                    if (scaleFactor <= 0)
                    {
                        MessageBox.Show("Please enter a valid positive scale factor.");
                        return;
                    }

                    scaleFactor = scaleFactor / 100.0;

                    cancellationTokenSource = new CancellationTokenSource();

                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = 0;

                    Task.Run(() =>
                    {
                        Bitmap downscaledImage = ParallelDownscale(originalImage, scaleFactor, cancellationTokenSource.Token);

                        if (downscaledImage != null)
                        {
                            Invoke(new Action(() =>
                            {
                                picBoxDownscaled.Image = downscaledImage;
                                picBoxDownscaled.SizeMode = PictureBoxSizeMode.StretchImage;

                                MessageBox.Show($"Downscaled Image Dimensions: {downscaledImage.Width}x{downscaledImage.Height}");
                                progressBar.Value = 100;
                            }));
                        }
                    }, cancellationTokenSource.Token);
                }
                else
                {
                    MessageBox.Show("Please enter a valid numeric scale factor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
