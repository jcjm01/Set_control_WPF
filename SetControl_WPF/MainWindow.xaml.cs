using QRCoder;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SetControl_WPF
{
    public partial class MainWindow : Window
    {
        private int contador = 0;
        private WriteableBitmap qrPreviewImage;
        private Random random = new Random();
        private string logFilePath = "loginHistory.txt"; // Ruta del archivo de historial de logins

        public MainWindow()
        {
            InitializeComponent();
            EnsureRunAsAdmin(); // Verificar ejecución como administrador
            SetInitialStatus();
            ClearPreview(); // Limpiar la vista previa al inicio
            LogLogin(); // Registrar login al iniciar
        }

        private void EnsureRunAsAdmin()
        {
            // Verifica si la aplicación ya se está ejecutando con privilegios de administrador
            if (!IsRunAsAdmin())
            {
                // Si no es así, relanza la aplicación con privilegios de administrador
                try
                {
                    var processInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        FileName = System.Reflection.Assembly.GetExecutingAssembly().Location,
                        Verb = "runas" // Esto asegura que se ejecute como administrador
                    };

                    Process.Start(processInfo);
                    Application.Current.Shutdown(); // Cierra la instancia actual
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al intentar ejecutar como administrador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("La aplicación se está ejecutando como administrador.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsRunAsAdmin()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void LogLogin()
        {
            try
            {
                string userType = IsRunAsAdmin() ? "admin" : "user";
                string julianDate = DateTime.Now.DayOfYear.ToString();
                string time = DateTime.Now.ToString("HH:mm:ss");
                string logEntry = $"{userType}; {julianDate} {time}";

                // Guardar en un archivo
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el login: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowLoginHistory()
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    string[] logEntries = File.ReadAllLines(logFilePath);
                    string logHistory = string.Join(Environment.NewLine, logEntries);

                    // Mostrar en un MessageBox
                    MessageBox.Show(logHistory, "Historial de Logins (Fecha Juliana)", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No hay historial de logins disponible.", "Historial de Logins", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar el historial de logins: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetInitialStatus()
        {
            try
            {
                pbStatusLed.Source = new BitmapImage(new Uri("Images/led_off.png", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la imagen inicial: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine($"Error al cargar la imagen inicial: {ex.Message}");
            }

            lblEstado.Content = "Estado: Detenido";
            progressBarMarcado.Value = 0;
            btnIniciarMarcado.IsEnabled = false;  // Deshabilita el botón de iniciar marcado hasta previsualizar
        }

        private void ClearPreview()
        {
            textPreviewProducto.Text = string.Empty;
            textPreviewLote.Text = string.Empty;
            textPreviewElab.Text = string.Empty;
            textPreviewCad.Text = string.Empty;
            pictureBoxQRCode.Source = null;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Redirigir a la pantalla de inicio de sesión
            var loginWindow = new LoginWindow(); // Asegúrate de que tienes una ventana de inicio de sesión llamada LoginWindow
            loginWindow.Show();
            this.Close(); // Cierra la ventana actual
        }

        private void btnCambiarIP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Nueva ruta absoluta al ejecutable de Change_IP
                string changeIPPath = @"C:\Users\juan_\OneDrive\Desktop\Change_IP\Change_IP\bin\x64\Release\Change_IP.exe";

                if (File.Exists(changeIPPath))
                {
                    Process.Start(changeIPPath);
                }
                else
                {
                    MessageBox.Show("El archivo Change_IP.exe no se encontró en la ruta especificada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar ejecutar Change_IP.exe: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPrevisualizar_Click(object sender, RoutedEventArgs e)
        {
            ClearPreview();

            qrPreviewImage = GenerarQRCode();
            if (qrPreviewImage != null)
            {
                pictureBoxQRCode.Source = qrPreviewImage;

                string tipoProducto = textBoxTipoProducto.Text;
                string lote = textBoxLote.Text;
                string elaboracion = dateTimePickerElaboracion.SelectedDate?.ToString("ddMMyyyy") ?? string.Empty;
                string caducidad = dateTimePickerCaducidad.SelectedDate?.ToString("ddMMyyyy") ?? string.Empty;

                textPreviewProducto.Text = tipoProducto;
                textPreviewLote.Text = $"LOTE: {lote}";
                textPreviewElab.Text = $"ELAB: {elaboracion}";
                textPreviewCad.Text = $"CAD: {caducidad}";

                btnIniciarMarcado.IsEnabled = true; // Habilitar el botón de iniciar marcado
            }
            else
            {
                MessageBox.Show("Error al generar el código QR. Por favor, revise los datos ingresados.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnIniciarMarcado_Click(object sender, RoutedEventArgs e)
        {
            if (qrPreviewImage == null)
            {
                MessageBox.Show("Por favor, previsualiza el QR antes de iniciar el marcado.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            pictureBoxQRCode.Source = qrPreviewImage;

            MessageBoxResult result = MessageBox.Show("¿Confirma que los datos en el QR son correctos?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                await SimularEstados();
            }
            else
            {
                MessageBox.Show("Por favor, corrija los datos antes de continuar.", "Corrección Necesaria", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private WriteableBitmap GenerarQRCode()
        {
            try
            {
                string tipoProducto = textBoxTipoProducto.Text;
                string lote = textBoxLote.Text;
                string elaboracion = dateTimePickerElaboracion.SelectedDate?.ToString("ddMMyyyy") ?? string.Empty;
                string caducidad = dateTimePickerCaducidad.SelectedDate?.ToString("ddMMyyyy") ?? string.Empty;

                string qrData = $"{tipoProducto}\nLOTE {lote}\nELAB {elaboracion}\nCAD {caducidad}";

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);

                return ConvertQRCodeToBitmapImage(qrCodeData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el código QR: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine($"Error al generar el código QR: {ex.Message}");
                return null;
            }
        }

        private WriteableBitmap ConvertQRCodeToBitmapImage(QRCodeData qrCodeData)
        {
            try
            {
                int width = qrCodeData.ModuleMatrix.Count;
                int height = qrCodeData.ModuleMatrix.Count;

                var bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Gray8, null);

                bitmap.Lock();

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte color = qrCodeData.ModuleMatrix[y][x] ? (byte)0 : (byte)255;
                        bitmap.WritePixels(new Int32Rect(x, y, 1, 1), new byte[] { color }, 1, 0);
                    }
                }

                bitmap.Unlock();

                return bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al convertir el código QR a imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine($"Error al convertir el código QR a imagen: {ex.Message}");
                return null;
            }
        }

        private async Task SimularEstados()
        {
            bool errorOccurred = false;

            for (contador = 1; contador <= 15; contador++)
            {
                if (random.Next(0, 10) < 2)
                {
                    errorOccurred = true;
                    lblEstado.Content = "Estado: Error de Marcado";
                    pbStatusLed.Source = new BitmapImage(new Uri("Images/led_off.png", UriKind.Relative));
                    MessageBox.Show("¡Error durante el proceso de marcado! El marcado se ha detenido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }

                txtContador.Text = contador.ToString();
                pbStatusLed.Source = new BitmapImage(new Uri("Images/led_on.png", UriKind.Relative));
                lblEstado.Content = $"Estado: Marcando - Contador: {contador}";
                progressBarMarcado.Value = (int)((contador / 15.0) * 100);
                await Task.Delay(1000);
            }

            if (!errorOccurred)
            {
                lblEstado.Content = "Estado: Lote Terminado";
                pbStatusLed.Source = new BitmapImage(new Uri("pack://application:,,,/Images/led_off.png"));
                MessageBox.Show("Lote Terminado. Contador alcanzó 15", "Marcado Completo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnShowHistory_Click(object sender, RoutedEventArgs e)
        {
            ShowLoginHistory();
        }
    }
}
