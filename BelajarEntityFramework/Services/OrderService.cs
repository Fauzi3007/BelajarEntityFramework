using BelajarEntityFramework.Models;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace BelajarEntityFramework.Services
{
    public class OrderService
    {
        private readonly EFDbContext _context;
        private readonly IConfiguration _configuration;

        // Constructor injection for AppDbContext.
        public OrderService(EFDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Retrieves a specific order by ID including customer, order items (with product details), and payment.
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        // Retrieves all orders with related customer information.
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.Customer)
                .ToListAsync();
        }

        public async Task<bool> SendEmailWithAttachmentAsync(string toEmail, string subject, string body, byte[] pdfAttachment, string attachmentName, bool isBodyHtml = false)
        {
            try
            {
                // Retrieve email settings from configuration file.
                string smtpServer = _configuration.GetValue<string>("EmailSettings:SmtpServer") ?? ""; // SMTP server address.
                int smtpPort = int.Parse(_configuration.GetValue<string>("EmailSettings:SmtpPort") ?? "587"); // SMTP server port.
                string senderName = _configuration.GetValue<string>("EmailSettings:SenderName") ?? "My Estore App"; // Name displayed as sender.
                string senderEmail = _configuration.GetValue<string>("EmailSettings:SenderEmail") ?? ""; // Sender's email address.
                string password = _configuration.GetValue<string>("EmailSettings:Password") ?? ""; // Password for the sender's email.
                // Create a new MailMessage to construct the email.
                using (MailMessage message = new MailMessage())
                {
                    // Set the sender's email and display name.
                    message.From = new MailAddress(senderEmail, senderName);
                    // Add the recipient's email address.
                    message.To.Add(new MailAddress(toEmail));
                    // Set the subject of the email.
                    message.Subject = subject;
                    // Set the body content of the email.
                    message.Body = body;
                    // Specify if the email body contains HTML.
                    message.IsBodyHtml = isBodyHtml;
                    // Create a new attachment from the provided PDF byte array.
                    // The attachmentName is the file name and "application/pdf" is the MIME type.
                    message.Attachments.Add(new Attachment(new MemoryStream(pdfAttachment), attachmentName, "application/pdf"));
                    // Create an SMTP client using the SMTP server and port.
                    using (var client = new SmtpClient(smtpServer, smtpPort))
                    {
                        // Set the client's credentials using the sender's email and password.
                        client.Credentials = new NetworkCredential(senderEmail, password);
                        // Enable SSL to secure the email transmission.
                        client.EnableSsl = true;
                        // Send the email asynchronously.
                        await client.SendMailAsync(message);
                    }
                }
                // Return true if email was sent successfully.
                return true;
            }
            catch (Exception ex)
            {
                // In case of any exception, you can log the exception here.
                // For now, return false to indicate the email was not sent.
                return false;
            }
        }

        // Generates a professional PDF invoice for the specified order using iText7.
        // orderId: The ID of the order to generate PDF for.
        // returns: Byte array representing the generated PDF file.
        public async Task<byte[]> GenerateOrderPdfAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                var ownerPassword = _configuration["PDFSettings:OwnerPassword"] ?? "SomeComplexOwnerPassword";
                var userPassword = _configuration["PDFSettings:UserPassword"] ?? "SomeComplexUserPassword";
                var writerProperties = new WriterProperties()
                        .SetStandardEncryption(
                            System.Text.Encoding.UTF8.GetBytes(userPassword),   // User password bytes.
                            System.Text.Encoding.UTF8.GetBytes(ownerPassword),  // Owner password bytes.
                            EncryptionConstants.ALLOW_PRINTING,                 // Permissions: allow printing.
                            EncryptionConstants.ENCRYPTION_AES_128              // Use AES 128-bit encryption.
                        );
                PdfWriter writer = new PdfWriter(ms,writerProperties);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, PageSize.A4);
                document.SetMargins(20, 20, 20, 20);

                // Create fonts.
                PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                PdfFont regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                // --------------------------------------------------------------------
                // Invoice Header: Company Name, Company Address & Invoice Info.
                Table headerTable = new Table(2).UseAllAvailableWidth();

                // Left cell: Company Name and Address (hard coded) in smaller font.
                Cell leftCell = new Cell()
                    .Add(new Paragraph("My ECommerce App")
                        .SetFont(boldFont)
                        .SetFontSize(26))
                    .Add(new Paragraph("1234 Business Rd, Suite 100\nCityville, ST 12345")
                        .SetFont(boldFont)
                        .SetFontSize(12))
                    .SetBorder(Border.NO_BORDER);
                headerTable.AddCell(leftCell);

                // Right cell: Invoice Number and Date.
                Cell rightCell = new Cell()
                    .Add(new Paragraph($"Invoice #{order.OrderNumber}\nDate: {order.OrderDate:yyyy-MM-dd}")
                        .SetFont(boldFont)
                        .SetTextAlignment(TextAlignment.RIGHT))
                    .SetBorder(Border.NO_BORDER);
                headerTable.AddCell(rightCell);
                document.Add(headerTable);

                // Add a horizontal line after the header.
                document.Add(new LineSeparator(new SolidLine()).SetMarginTop(10).SetMarginBottom(10));

                // --------------------------------------------------------------------
                // Customer & Payment Details (2-column layout).
                Table detailsTable = new Table(2).UseAllAvailableWidth();

                // Left: Customer Details.
                Cell customerCell = new Cell()
                    .Add(new Paragraph("Bill To:")
                        .SetFont(boldFont)
                        .SetFontSize(14))
                    .Add(new Paragraph(order.Customer.Name).SetFont(regularFont))
                    .Add(new Paragraph(order.Customer.Address).SetFont(regularFont))
                    .Add(new Paragraph($"Email: {order.Customer.Email}").SetFont(regularFont))
                    .Add(new Paragraph($"Phone: {order.Customer.Phone}").SetFont(regularFont))
                    .SetBorder(Border.NO_BORDER);
                detailsTable.AddCell(customerCell);

                // Right: Payment Details.
                Cell paymentCell = new Cell()
                    .Add(new Paragraph("Payment Details:")
                        .SetFont(boldFont)
                        .SetFontSize(14))
                    .SetTextAlignment(TextAlignment.RIGHT);
                if (order.Payment != null)
                {
                    paymentCell.Add(new Paragraph($"Method: {order.Payment.PaymentMethod}").SetFont(regularFont));
                    paymentCell.Add(new Paragraph($"Status: {order.Payment.PaymentStatus}").SetFont(regularFont));
                    paymentCell.Add(new Paragraph($"Date: {order.Payment.PaymentDate:yyyy-MM-dd}").SetFont(regularFont));
                }
                else
                {
                    paymentCell.Add(new Paragraph("Payment not recorded.").SetFont(regularFont));
                }
                paymentCell.SetBorder(Border.NO_BORDER);
                detailsTable.AddCell(paymentCell);
                document.Add(detailsTable);
                //document.Add(new Paragraph("\n")); // Extra spacing

                // --------------------------------------------------------------------
                // Items Table.
                Table itemsTable = new Table(6).UseAllAvailableWidth();
                // Define header cells with DARK_GRAY background and white font.
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Description")
                    .SetFont(boldFont)
                    .SetFontColor(ColorConstants.WHITE))
                    .SetBackgroundColor(ColorConstants.DARK_GRAY));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Quantity")
                    .SetFont(boldFont)
                    .SetFontColor(ColorConstants.WHITE))
                    .SetBackgroundColor(ColorConstants.DARK_GRAY)
                    .SetTextAlignment(TextAlignment.RIGHT));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Unit Price")
                    .SetFont(boldFont)
                    .SetFontColor(ColorConstants.WHITE))
                    .SetBackgroundColor(ColorConstants.DARK_GRAY)
                    .SetTextAlignment(TextAlignment.RIGHT));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Tax %")
                    .SetFont(boldFont)
                    .SetFontColor(ColorConstants.WHITE))
                    .SetBackgroundColor(ColorConstants.DARK_GRAY)
                    .SetTextAlignment(TextAlignment.RIGHT));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Tax Amount")
                    .SetFont(boldFont)
                    .SetFontColor(ColorConstants.WHITE))
                    .SetBackgroundColor(ColorConstants.DARK_GRAY)
                    .SetTextAlignment(TextAlignment.RIGHT));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Total")
                    .SetFont(boldFont)
                    .SetFontColor(ColorConstants.WHITE))
                    .SetBackgroundColor(ColorConstants.DARK_GRAY)
                    .SetTextAlignment(TextAlignment.RIGHT));

                // Add each order item.
                foreach (var item in order.OrderItems)
                {
                    //decimal subTotal = item.UnitPrice * item.Quantity;
                    //decimal taxAmount = subTotal * (item.TaxPercent / 100);
                    //decimal total = subTotal + taxAmount;

                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Product.Name + " - " + item.Product.Description)
                        .SetFont(regularFont)));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToString())
                        .SetFont(regularFont)).SetTextAlignment(TextAlignment.RIGHT));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.UnitPrice.ToString("C"))
                        .SetFont(regularFont)).SetTextAlignment(TextAlignment.RIGHT));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.TaxPercent.ToString("F2") + "%")
                        .SetFont(regularFont)).SetTextAlignment(TextAlignment.RIGHT));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.TotalTaxAmount.ToString("C"))
                        .SetFont(regularFont)).SetTextAlignment(TextAlignment.RIGHT));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.TotalPriceWithTax.ToString("C"))
                        .SetFont(regularFont)).SetTextAlignment(TextAlignment.RIGHT));
                }
                document.Add(itemsTable.SetMarginTop(20));

                // --------------------------------------------------------------------
                // Totals Section.
                Table totalsTable = new Table(2).UseAllAvailableWidth();
                totalsTable.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
                totalsTable.AddCell(new Cell()
                    .Add(new Paragraph("Subtotal:").SetFont(boldFont))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT));
                totalsTable.AddCell(new Cell()
                    .Add(new Paragraph(order.SubTotal.ToString("C")).SetFont(regularFont))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT));
                totalsTable.AddCell(new Cell()
                    .Add(new Paragraph("Total Tax:").SetFont(boldFont))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT));
                totalsTable.AddCell(new Cell()
                    .Add(new Paragraph(order.TotalTax.ToString("C")).SetFont(regularFont))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT));
                totalsTable.AddCell(new Cell()
                    .Add(new Paragraph("Grand Total:").SetFont(boldFont))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT));
                totalsTable.AddCell(new Cell()
                    .Add(new Paragraph(order.GrandTotal.ToString("C")).SetFont(regularFont))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT));
                document.Add(totalsTable.SetMarginTop(20));

                // --------------------------------------------------------------------
                // Footer.
                document.Add(new Paragraph("\n")); // spacing
                document.Add(new LineSeparator(new SolidLine()));
                document.Add(new Paragraph("Thank you for your business!")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFont(regularFont)
                    .SetFontSize(12)
                    .SetMarginTop(10));

                document.Close();
                return ms.ToArray();
            }
        }
    }
}