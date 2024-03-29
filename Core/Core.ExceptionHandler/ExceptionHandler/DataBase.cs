﻿using Core.ExceptionHandler.Enums;
using Core.ExceptionHandler.Extension;
using System.Net;

namespace Core.ExceptionHandler.ExceptionHandler
{
    public static class DataBase
    {
        public static void RaiseEntityAlreadyExistsException(string entityName)
        {
            string message = "{0} already exists.";
            throw new CustomExceptionHandler(message, entityName)
            {
                ErrorCode = DatabaseErrorCode.EntityAlreadyExists.ToDescription(),
                HttpStatusCode = HttpStatusCode.Conflict
            };
        }

        public static void RaiseNoDataFoundException()
        {
            string message = "No data found.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.NoDataFound.ToDescription(),
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        public static void RaiseInvalidDataException()
        {
            string message = "Invalid data.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static void RaiseInvalidDataException(string entityName)
        {
            string message = "Invalid {0}.";
            throw new CustomExceptionHandler(message, entityName)
            {
                ErrorCode = DatabaseErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static void RaiseCanNotDeleteException()
        {
            string message = "Can not delete.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.CanNotDelete.ToDescription(),
                HttpStatusCode = HttpStatusCode.Forbidden
            };
        }

        public static void RaiseDuplicateDataException()
        {
            string message = "Duplicate data.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.DuplicateData.ToDescription(),
                HttpStatusCode = HttpStatusCode.Forbidden
            };
        }


        public static void RaiseInvalidDateException()
        {
            string message = "Start date should not less than end date.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.InvalidData.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static void RaiseCanNotUpdateException()
        {
            string message = "Can not update.";
            throw new CustomExceptionHandler(message)
            {
                ErrorCode = DatabaseErrorCode.CanNotDelete.ToDescription(),
                HttpStatusCode = HttpStatusCode.Forbidden
            };
        }

        public static void RaiseNoDataFoundExceptionForEntity(string entityName)
        {
            string message = "No data found for {0}.";
            throw new CustomExceptionHandler(message, entityName)
            {
                ErrorCode = DatabaseErrorCode.NoDataFound.ToDescription(),
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static void RaiseNoContentException()
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
