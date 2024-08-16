using Core.ExceptionHandler.Enums;
using Core.ExceptionHandler.Extension;
using System.Net;

namespace Core.ExceptionHandler.ExceptionHandler
{
    public static class DataBaseExceptionHandler
    {
        public static CustomExceptionHandler RaiseEntityAlreadyExistsException(string entityName)
        {
            string message = "{0} already exists.";
            throw new CustomExceptionHandler(message, entityName)
            {
                ErrorCode = DatabaseErrorCode.EntityAlreadyExists.ToDescription(),
                HttpStatusCode = HttpStatusCode.Conflict
            };
        }

        public static CustomExceptionHandler RaiseNoDataFoundException(string entityName)
        {
            string message = "No data found for {0}.";
            throw new CustomExceptionHandler(message, entityName)
            {
                ErrorCode = DatabaseErrorCode.NoDataFound.ToDescription(),
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        public static CustomExceptionHandler RaiseInvalidDataException()
        {
            string message = "Invalid data.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static CustomExceptionHandler RaiseInvalidDataException(string entityName)
        {
            string message = "Invalid {0}.";
            throw new CustomExceptionHandler(message, entityName)
            {
                ErrorCode = DatabaseErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static CustomExceptionHandler RaiseCanNotDeleteException()
        {
            string message = "Can not delete.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.CanNotDelete.ToDescription(),
                HttpStatusCode = HttpStatusCode.Forbidden
            };
        }

        public static CustomExceptionHandler RaiseDuplicateDataException()
        {
            string message = "Duplicate data.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.DuplicateData.ToDescription(),
                HttpStatusCode = HttpStatusCode.Forbidden
            };
        }


        public static CustomExceptionHandler RaiseInvalidDateException()
        {
            string message = "Start date should not less than end date.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static CustomExceptionHandler RaiseCanNotUpdateException()
        {
            string message = "Can not update.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.CanNotDelete.ToDescription(),
                HttpStatusCode = HttpStatusCode.Forbidden
            };
        }

        public static CustomExceptionHandler RaiseNoDataFoundExceptionForEntity(string entityName)
        {
            string message = "No data found for {0}.";
            throw new CustomExceptionHandler(message, entityName)
            {
                ErrorCode = DatabaseErrorCode.NoDataFound.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static CustomExceptionHandler RaiseNoContentException()
        {
            string message = "No content.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = BusinessErrorCode.NoContent.ToDescription(),
                HttpStatusCode = HttpStatusCode.NoContent
            };
        }
    }
}
