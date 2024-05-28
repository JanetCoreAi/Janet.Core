using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace Janet.Core.Services.Infrastructure;

    public class ErrorHandlingService
    {
        private readonly ILogger _logger;

        public ErrorHandlingService(ILogger logger)
        {
            _logger = logger;
        }

        public void HandleException(Exception ex, string message = null)
        {
            // Log the exception
            _logger.Error(ex, message ?? ex.Message);

            // Handle specific exception types
            switch (ex)
            {
                case ArgumentException argEx:
                    // Handle argument exceptions
                    HandleArgumentException(argEx);
                    break;
                case InvalidOperationException invalidOpEx:
                    // Handle invalid operation exceptions
                    HandleInvalidOperationException(invalidOpEx);
                    break;
                // Add cases for other specific exception types as needed
                default:
                    // Handle general exceptions
                    HandleGeneralException(ex);
                    break;
            }
        }

        private void HandleArgumentException(ArgumentException ex)
        {
            // Log additional details or context for argument exceptions
            _logger.Warning("An argument exception occurred: {ExceptionMessage}", ex.Message);

            // Optionally, you can throw a custom exception or perform additional actions
            throw new CustomArgumentException(ex.Message, ex);
        }

        private void HandleInvalidOperationException(InvalidOperationException ex)
        {
            // Log additional details or context for invalid operation exceptions
            _logger.Warning("An invalid operation exception occurred: {ExceptionMessage}", ex.Message);

            // Optionally, you can throw a custom exception or perform additional actions
            throw new CustomInvalidOperationException(ex.Message, ex);
        }

        private void HandleGeneralException(Exception ex)
        {
            // Log additional details or context for general exceptions
            _logger.Error("An unhandled exception occurred: {ExceptionMessage}", ex.Message);

            // Optionally, you can throw a custom exception or perform additional actions
            throw new CustomException("An unhandled exception occurred.", ex);
        }
    }

    // Custom exception classes (optional)
    public class CustomException : Exception
    {
        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class CustomArgumentException : Exception
    {
        public CustomArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class CustomInvalidOperationException : Exception
    {
        public CustomInvalidOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
