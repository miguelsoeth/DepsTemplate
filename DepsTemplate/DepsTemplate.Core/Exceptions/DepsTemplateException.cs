using DepsTemplate.Core.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace DepsTemplate.Core.Exceptions
{
    public class DepsTemplateException : Exception
    {
        private static readonly Dictionary<InternalErrorCode, string> _errorMessages = new()
        {
            { InternalErrorCode.NotAuthorized, "O usuário precisa estar logado para efetuar essa ação." },
            { InternalErrorCode.Forbidden, "Usuário não tem as permissões necessárias para efetuar esta ação." },
            { InternalErrorCode.NotFound, "Entidade não encontrada. Por favor, verifique." }
        };

        public InternalErrorCode DetailErrorCode { get; set; }
        public ExpandoObject ExtraData { get; set; }

        public DepsTemplateException(InternalErrorCode errorCode, string message) : base(message)
        {
            DetailErrorCode = errorCode;
        }

        public DepsTemplateException(InternalErrorCode errorCode, string message, ExpandoObject extraData) : this(errorCode, message)
        {
            ExtraData = extraData;
        }

        public DepsTemplateException(string message) : this(InternalErrorCode.BadRequest, message) { }

        public DepsTemplateException(InternalErrorCode error) : this(error, _errorMessages[error]) { }
    }
}
