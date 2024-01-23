using DepsTemplate.Core.Enums;
using DepsTemplate.Core.Exceptions;
using DepsTemplate.SharedKernel.Extensions;
using System;
using System.Linq;
using System.Security.Claims;

namespace DepsTemplate.Web.Util
{
    public static class UserExtension
    {
        public static Guid GetClienteId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated) throw new DepsTemplateException(InternalErrorCode.NotAuthorized);

            return Guid.Parse(user.Claims.First(c => c.Type == "clienteid").Value);
        }

        public static Guid GetUsuarioId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated) throw new DepsTemplateException(InternalErrorCode.NotAuthorized);

            return Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public static PerfilUsuario GetPerfilUsuario(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated) throw new DepsTemplateException(InternalErrorCode.NotAuthorized);

            return EnumExtensions.GetValueFromDescription<PerfilUsuario>(user.Claims.First(x => x.Type == ClaimTypes.Role).Value);
        }
    }
}
