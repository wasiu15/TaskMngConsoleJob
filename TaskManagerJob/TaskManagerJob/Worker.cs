using Microsoft.Extensions.Configuration;
using TaskManagerJob.Dtos;
using TaskManagerJob.Entities;
using TaskManagerJob.Logger;
using TaskManagerJob.Repositories.Interfaces;
using TaskManagerJob.Utilities;

namespace TaskManagerJob
{
    public class Worker
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientWrapper _httpClient;

        public Worker(IConfiguration configuration, IHttpClientWrapper httpClient, IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task ExecuteProcessAsync()
        {
            _logger.LogInformation(" ");
            _logger.LogInformation("---------STARTING EXECUTION PROCESS---------");
            _logger.LogInformation("---------STARTING EXECUTION PROCESS---------");

            var getAnyUnCompletedTaskToDueInTwoDays = await _repository.TaskRepository.GetAnyUnCompletedTaskToDueInTwoDays(false);
            
            _logger.LogInformation("Getting uncompeted tasks that will be due in range of 48 hours");
            _logger.LogInformation("Total due tasks found is : ");
            _logger.LogInformation(getAnyUnCompletedTaskToDueInTwoDays == null ? "0" : getAnyUnCompletedTaskToDueInTwoDays.Count().ToString());


            if (getAnyUnCompletedTaskToDueInTwoDays != null)
            {
                foreach (var task in getAnyUnCompletedTaskToDueInTwoDays)
                {
                    var checkIfUserAlreadyRecievedNotification = await _repository.NotificationRepository.GetByNotificationIdAndUserId(task.Id, task.UserId, true);
                    _logger.LogInformation("-----------------------------------------------");
                    _logger.LogInformation("Checking if users already recieved notification");
                    _logger.LogInformation("USER WITH THE ID: " + task.Id);
                    if (checkIfUserAlreadyRecievedNotification == null)
                    {
                        //  NOW WE CAN SEND MAIL AND UPDATE DB
                        //  GET CURRENT USER EMAIL AND NAME FROM THE HTTPCONTEXT CLASS
                        var getUserFromDb = await _repository.UserRepository.GetByUserId(task.UserId, false);
                        if (getUserFromDb != null)
                        {
                            var sendRequest = new EmailSenderRequestDto
                            {
                                email = getUserFromDb.Email,
                                subject = "Due Date Reminder",
                                message = "Hello " + getUserFromDb.Name + ", Your task with the Title: " + task.Title + " now has less than 48 hours left to be complete as at Time: " + DateTime.UtcNow.ToString()
                            };

                            //  GET THE MAILER URL... WHERE WE WOULD BE SENDING OUR POST REQUEST TO
                            var mailerUrl = $"{_configuration.GetSection("ExternalAPIs")["MailerUrl"]}";

                            //  THIS LINE SENDS THE REQUEST TO THE EMAIL SERVER
                            // var sendEmailResponse = _httpClient.SendPostEmailAsync<string>(mailerUrl, sendRequest);

                           // var sendEmailResponse = _httpClient.SendPostEmailAsync<string>(mailerUrl, sendRequest);

                            var req = new PostRequest<EmailSenderRequestDto>
                            {
                                Url = mailerUrl,
                                Data = sendRequest
                            };
                            _logger.LogInformation("----------------------------------");
                            _logger.LogInformation("----------------------------------");
                            _logger.LogInformation("Request sent to the mailing server");

                            var sendEmailResponse = await _httpClient.SendPostEmailRequest<string, EmailSenderRequestDto>(req);
                            _logger.LogInformation("--------------------------------");
                            _logger.LogInformation("--------------------------------");
                            _logger.LogInformation("Response from the mailing server");

                            //  IT WILL ONLY UPDATE THIS RECORD IS THE EMAIL WAS SENT SUCCESSFULLY ELSE THE TASK WILL BE RETURNED BACK FOR THE NEXT BATCH PROCESSING
                            if (sendEmailResponse == "1")
                            {
                                var createNotificationRequest = new Notification
                                {
                                    NotificationId = Guid.NewGuid().ToString(),
                                    TaskId = task.Id.ToString(),
                                    RecievedUserId = task.UserId,
                                    Type = NotificationType.Due_date.ToString(),
                                    Message = sendRequest.message,
                                    ReadStatus = NotificationStatus.Unread.ToString(),
                                    Time = DateTime.UtcNow,
                                };
                                _repository.NotificationRepository.CreateNotification(createNotificationRequest);
                            }
                        }
                    }
                }
                await _repository.SaveAsync();
            }

            _logger.LogInformation("---------ENDING EXECUTION PROCESS---------");
            _logger.LogInformation("---------ENDING EXECUTION PROCESS---------");
            _logger.LogInformation(" ");

        }
    }
}