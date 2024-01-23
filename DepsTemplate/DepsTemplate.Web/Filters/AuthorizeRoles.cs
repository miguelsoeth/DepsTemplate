using DepsTemplate.Core.Enums;
using DepsTemplate.Core.Exceptions;
using DepsTemplate.SharedKernel.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DepsTemplate.Web.Filters
{
    public class AuthorizeRoles : ActionFilterAttribute
    {
        private PerfilUsuario[] Permissoes { get; set; }

        public AuthorizeRoles(params PerfilUsuario[] permissoes)
        {
            Permissoes = permissoes;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new DepsTemplateException(InternalErrorCode.NotAuthorized);
            }

            var possuiPermissao = false;
            foreach (var item in Permissoes)
            {
                if (user.IsInRole(EnumExtensions.Description(item)))
                {
                    possuiPermissao = true;
                    break;
                }
            }

            if (!possuiPermissao)
            {
                throw new DepsTemplateException(InternalErrorCode.Forbidden);
            }

            base.OnActionExecuting(context);
        }
    }
}
