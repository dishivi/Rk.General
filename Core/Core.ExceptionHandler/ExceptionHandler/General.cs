using Core.ExceptionHandler.Enums;
using Core.ExceptionHandler.Extension;
using Newtonsoft.Json;
using System.Net;

namespace Core.ExceptionHandler.ExceptionHandler
{
    /// <summary>
    /// 
    /// </summary>
    public static class General
    {
        /// <summary>
        /// Raises the HTTP version not supported exception.
        /// </summary>
        /// <param name="apiVersion">The API version.</param>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseHttpVersionNotSupportedException(string apiVersion)
        {
            string message = "Api version {0} does not exist. Please verify and try again.";
            throw new CustomExceptionHandler(message, apiVersion)
            {
                ErrorCode = GeneralErrorCode.HttpVersionNotSupported.ToDescription(),
                HttpStatusCode = HttpStatusCode.HttpVersionNotSupported
            };
        }

        /// <summary>
        /// Raises the request timeout exception.
        /// </summary>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseRequestTimeoutException()
        {
            string message = "Request timeout.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = GeneralErrorCode.RequestTimeout.ToDescription(),
                HttpStatusCode = HttpStatusCode.RequestTimeout
            };
        }

        /// <summary>
        /// Raises the bad request exception.
        /// </summary>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseBadRequestException()
        {
            string message = "Bad request.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = GeneralErrorCode.BadRequest.ToDescription(),
                HttpStatusCode = HttpStatusCode.BadRequest
            };
        }

        /// <summary>
        /// Returns the custom error response.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static string ReturnCustomErrorResponse(CustomExceptionHandler ex)
        {
            return JsonConvert.SerializeObject(new CustomErrorResponse
            {
                Code = ex.ErrorCode.ToString(),
                Message = ex.Message
            });
        }

        /// <summary>
        /// Returns the system error response.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static string ReturnSystemErrorResponse(Exception ex)
        {
            string message = "Operational error.";
            return JsonConvert.SerializeObject(new CustomErrorResponse
            {
                Code = BusinessErrorCode.OperationalError.ToDescription(),
                Message = message
            });
        }

        /// <summary>
        /// Returns the image resize error response.
        /// </summary>
        /// <param name="imageVesions">The image vesions.</param>
        /// <returns></returns>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static string ReturnImageResizeErrorResponse(string imageVesions)
        {
            string message = "Uploaded image is not valid for size {0}";
            throw new CustomExceptionHandler(message, imageVesions)
            {
                ErrorCode = BusinessErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Returns the imagesize error response.
        /// </summary>
        /// <param name="imageVesions">The image vesions.</param>
        /// <returns></returns>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static string ReturnImagesizeErrorResponse(string imageVesions)
        {
            string message = "Uploaded image size is not valid.";
            throw new CustomExceptionHandler(message, imageVesions)
            {
                ErrorCode = BusinessErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Returns the image format not valid.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns></returns>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static string ReturnImageFormatNotValid(string extension)
        {
            string message = "Uploaded image extension {0} is not valid.";
            throw new CustomExceptionHandler(message, extension)
            {
                ErrorCode = BusinessErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }



        /// <summary>
        /// Empties the date space schedule.
        /// </summary>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void EmptyDateSpaceSchedule()
        {
            string message = "Invalid date.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.EntityCanNotBeEmpty.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Missings the field exception.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static string MissingFieldException()
        {
            string message = "Required column missing.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }


        /// <summary>
        /// Customs the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <returns></returns>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static string CustomErrorMessage(string message, HttpStatusCode statusCode)
        {
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = GeneralErrorCode.BadRequest.ToDescription(),
                HttpStatusCode = statusCode
            };
        }

        /// <summary>
        /// Raises the configuration missing exception.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static string RaiseConfigurationMissingException(string entity)
        {
            string message = "Required Configuration Missing For {0}.";
            throw new CustomExceptionHandler(message, entity)
            {
                ErrorCode = GeneralErrorCode.ConfigurationMissing.ToDescription(),
                HttpStatusCode = HttpStatusCode.InternalServerError
            };
        }

        /// <summary>
        /// Raises the third party service not implemented.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseThirdPartyServiceNotImplemented(string entity)
        {
            string message = $"Third party {entity} service not implemented in system.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = GeneralErrorCode.ThirdPartyServiceNotImplemented.ToDescription(),
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        /// <summary>
        /// Raises the third party operation fail error.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseThirdPartyOperationFailError(string entity)
        {
            string message = $"Operation Failed for {entity}.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = GeneralErrorCode.ThirdPartyOperationFailError.ToDescription(),
                HttpStatusCode = HttpStatusCode.InternalServerError
            };
        }


        /// <summary>
        /// Returns the size not valid for orignal image error response.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static string ReturnSizeNotValidForOrignalImageErrorResponse()
        {
            string message = "Image size should not be multiple while uploading original image.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Raises the custom error message.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static CustomExceptionHandler RaiseCustomErrorMessage(int statusCode, string message)
        {
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.OperationalError.ToDescription(),
                HttpStatusCode = (HttpStatusCode)statusCode
            };
        }

        /// <summary>
        /// Raises the third party service token null.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseThirdPartyServiceTokenNull(string entity)
        {
            string message = $"Third party {entity} service token is null.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = GeneralErrorCode.ThirdPartyServiceTokenNull.ToDescription(),
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        public static void RaiseCannotEditArchiveDocumentException()
        {
            string message = "You can not update archived document";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.EntityCanNotBeEmpty.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static void RaiseCannotEditVersionOfArchiveDocumentException()
        {
            string message = "You can not update archived document version";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.EntityCanNotBeEmpty.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static string ReturnCanNotDeleteDefaultDocumentVersion()
        {
            string message = "You can not delete default document version.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Raises the third party operation fail error.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="CustomExceptionHandler"></exception>
        public static void RaiseThirdPartyOperationFailError(string message, HttpStatusCode httpStatusCode)
        {
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = GeneralErrorCode.ThirdPartyOperationFailError.ToDescription(),
                HttpStatusCode = httpStatusCode
            };
        }
    }
}
