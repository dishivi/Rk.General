using Core.ExceptionHandler.Enums;
using Core.ExceptionHandler.Extension;
using FluentValidation.Results;
using System.Net;

namespace Core.ExceptionHandler.ExceptionHandler
{
    /// <summary>
    /// 
    /// </summary>
    public static class Business
    {
        public static string GlobalExceptionMessage(ValidationFailure errorData)

        {
            string message = string.Empty;

            if (errorData.ErrorCode == "NotNullValidator")
            {
                message = $"{errorData.PropertyName} can not be null.";
            }
            else if (errorData.ErrorCode == "NotEmptyValidator")
            {
                message = $"{errorData.PropertyName} can not be empty.";
            }
            else if (errorData.ErrorCode == "NotEqualValidator")
            {
                string compareValue = Convert.ToString(errorData.FormattedMessagePlaceholderValues["ComparisonValue"]);

                message = $"{errorData.PropertyName} can not be same as {compareValue}";
            }
            else if (errorData.ErrorCode == "EqualValidator")
            {
                string compareValue = Convert.ToString(errorData.FormattedMessagePlaceholderValues["ComparisonValue"]);

                message = $"{errorData.PropertyName} can be same as {compareValue}";
            }
            else if (errorData.ErrorCode == "LengthValidator" || errorData.ErrorCode == "MaxLengthValidator" ||
                errorData.ErrorCode == "MinLengthValidator" || errorData.ErrorCode == "LessThanValidator" ||
                errorData.ErrorCode == "LessThanOrEqualToValidator" || errorData.ErrorCode == "GreaterThanValidator" ||
                errorData.ErrorCode == "GreaterThanOrEqualToValidator" || errorData.ErrorCode == "RegularExpressionValidator" ||
                errorData.ErrorCode == "EmailValidator" || errorData.ErrorCode == "CreditCardValidator" ||
                errorData.ErrorCode == "EnumValidator" || errorData.ErrorCode == "EmptyValidator" ||
                errorData.ErrorCode == "NullValidator" || errorData.ErrorCode == "ExclusiveBetweenValidator" ||
                errorData.ErrorCode == "InclusiveBetweenValidator" || errorData.ErrorCode == "AsyncPredicateValidator")

            {
                message = errorData.ErrorMessage;
            }
            else
            {
                message = errorData.ErrorMessage;
            }

            return message;
        }

        /// <summary>
        /// Raises the entity null exception.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseEntityNullException(string entityName)
        {
            string message = "{0} can not be empty.";
            throw new CustomExceptionHandler(message, entityName)
            {
                ErrorCode = BusinessErrorCode.EntityCanNotBeEmpty.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Raises the invalid entity exception.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseInvalidEntityException(string entityName)
        {
            string message = "Invalid {0}.";
            throw new CustomExceptionHandler(message, entityName)
            {
                ErrorCode = BusinessErrorCode.InvalidEntity.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Raises the request null exception.
        /// </summary>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseRequestNullException()
        {
            string message = "Request can not be null.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.RequestCanNotBeNull.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Raises the operational error exception.
        /// </summary>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseOperationalErrorException()
        {
            string message = "Operational error.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.OperationalError.ToDescription(),
                HttpStatusCode = HttpStatusCode.InternalServerError
            };
        }

        /// <summary>
        /// Raises the no content exception.
        /// </summary>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseNoContentException()
        {
            string message = "No content.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.NoContent.ToDescription(),
                HttpStatusCode = HttpStatusCode.NoContent
            };
        }

        /// <summary>
        /// Raises the invalid data exception.
        /// </summary>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseInvalidDataException()
        {
            string message = "Invalid data.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Raises the no data found exception.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseNoDataFoundException(string entity)
        {
            string message = "{0} not found.";
            throw new CustomExceptionHandler(message, entity)
            {
                ErrorCode = DatabaseErrorCode.NoDataFound.ToDescription(),
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        /// <summary>
        /// Customs the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void CustomErrorMessage(string message)
        {
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Raises the invalid length exception.
        /// </summary>
        /// <param name="FType">Type of the f.</param>
        /// <param name="MaxLen">The maximum length.</param>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseInvalidLengthException(string FType,int MaxLen)
        {
            string Lmessage = "The field " + FType +" must be a string with a maximum length of " + MaxLen;
            throw new CustomExceptionHandler(Lmessage)
            {
                ErrorCode = BusinessErrorCode.InvalidLength.ToDescription(),
                HttpStatusCode = HttpStatusCode.LengthRequired
            };
        }
    }
}
