using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;
using razorWebApplication1.Models;
using System.Configuration;

namespace razorWebApplication1.Pages
{
    public class QueueTestModel(IConfiguration configuration) : PageModel
    {
        // Insert into QUEUETEST table
        private readonly string _connectionString = configuration.GetConnectionString("OracleConnection")
                ?? throw new InvalidOperationException("OracleConnection ÇÃê⁄ë±ï∂éöóÒÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");

        // Insert into Azure Queue Storage
        private const string connectionString = "DefaultEndpointsProtocol=";
        private const string queueName = "";

        [BindProperty]
        public string jsonData { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {

            // Insert into Azure Queue Storage
            QueueClient queueClient = new QueueClient(connectionString, queueName);
            await queueClient.SendMessageAsync(jsonData);

            // Retrieve message from Azure Queue Storage
            QueueMessage[] messages = await queueClient.ReceiveMessagesAsync(maxMessages: 1);
            
            // Insert into QUEUETEST table
            using var conn = new OracleConnection(_connectionString);
            conn.Open();

            foreach (QueueMessage message in messages)
            {
                //var cmd = new OracleCommand("INSERT INTO QUEUETEST (jsonData) VALUES (:jsonData)", conn);
                //cmd.Parameters.Add("jsonData", OracleDbType.Varchar2).Value = message.MessageText ?? string.Empty;

                //cmd.ExecuteNonQuery();
                // Delete the message after processing
                //queueClient.DeleteMessage(message.MessageId, message.PopReceipt);
            }

            conn.Close();
            return RedirectToPage("QueueTest");
        }
    }

}
